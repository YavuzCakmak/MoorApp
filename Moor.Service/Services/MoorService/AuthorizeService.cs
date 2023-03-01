using Moor.Core.Services.MoorService;
using Moor.Model.Model.Authorize;
using Moor.Model.Utilities.Authentication;
using Moor.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moor.Service.Utilities.AuthorizeHelpers;
using AutoMapper;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Core.Repositories.MoorRepository.AuthorizeRepository;
using Moor.Core.Repositories;
using Moor.Core.UnitOfWorks;
using Moor.Model.Utilities.TokenModel;
using Moor.Model.Utilities.Authorize;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Org.BouncyCastle.Asn1.Pkcs;
using Moor.Core.Extension.String;
using Moor.Core.Enums;

namespace Moor.Service.Services.MoorService
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelService _personnelService;
        private readonly IPersonnelRoleService _personnelRoleService;
        private readonly IRolePrivilegeService _rolePrivilegeService;
        private readonly TokenHelper _tokenHelper;


        public AuthorizeService(IUnitOfWork unitOfWork, IPersonnelService personnelService, IPersonnelRoleService personnelRoleService, IRolePrivilegeService rolePrivilegeService, TokenHelper tokenHelper, IMapper mapper)
        {
            _personnelService = personnelService;
            _personnelRoleService = personnelRoleService;
            _rolePrivilegeService = rolePrivilegeService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        public LoginResponseModel Login(LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return new LoginResponseModel
                {
                    Message = "Kullanıcı adı veya şifreniz yanlıştır. Lütfen kontrol ediniz."
                };
            }

            var personnel = _personnelService.Where(x => x.UserName == loginModel.UserName).FirstOrDefault();

            if (personnel is null)
            {
                return new LoginResponseModel
                {
                    Message = "Kullanıcı adı veya şifreniz yanlıştır. Lütfen kontrol ediniz."
                };
            }

            var hashedPassword = HashingHelper.CreatePasswordHash(loginModel.Password);

            if (!HashingHelper.VerifyPasswordHash(personnel.Password, hashedPassword))
            {
                return new LoginResponseModel
                {
                    Message = "Kullanıcı adı veya şifreniz yanlıştır. Lütfen kontrol ediniz."
                };
            }

            var personnelRoles = _personnelRoleService.Where(x => x.PersonnelId == personnel.Id && !x.IsDeleted).ToList();

            if (personnel is null)
            {
                return new LoginResponseModel
                {
                    Message = "Kullanıcıya ait herhangi bir role bulunamadı."
                };
            }
            var roleDtos = new List<Role>();

            foreach (var rolePrivilegeGroup in personnelRoles.GroupBy(x => x.Role.Id))
            {
                var role = rolePrivilegeGroup.Select(x => x.Role).First();
                roleDtos.Add(new Role
                {
                    Id = role.Id,
                    Name = role.Name,
                });
            }

            var tokenModel = new TokenModel
            {
                PersonnelId = personnel.Id,
                Roles = roleDtos.ToList(),
                Username = personnel.UserName
            };

            return new LoginResponseModel
            {
                Token = _tokenHelper.CreateToken(tokenModel),
                IsSuccess = true,
            };
        }

        public async Task<DataResult> Register(PersonnelModel personnelModel)
        {
            #region
            PersonnelRoleEntity personnelRole = new PersonnelRoleEntity();
            #endregion
            
            var personnel = _personnelService.Where(x => x.UserName == personnelModel.UserName || x.Email == personnelModel.Email).ToList();
            if (personnel is not null && personnel.Count > 0)
            {
                return new DataResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı adınız veya email adresiniz sistemde mevcuttur lütfen değiştiriniz."
                };
            }

            var hashedPassword = HashingHelper.CreatePasswordHash(personnelModel.Password);

            var dataResult = _personnelService.AddAsync(new PersonnelEntity
            {
                Email = personnelModel.Email,
                FirstName = personnelModel.FirstName,
                Password = hashedPassword,
                UserName = personnelModel.UserName,
                Status = ((int)Status.AKTIF), //Default Aktif
                LastName = personnelModel.LastName,
            });

            if (dataResult.Result.IsNotNull() && dataResult.Result.Id.IsNotNull())
            {
                PersonnelRoleEntity personnelRoleEntity = new PersonnelRoleEntity();
                personnelRoleEntity.IsDeleted = false;
                personnelRoleEntity.RoleId = personnelModel.RoleId;
                personnelRoleEntity.PersonnelId = dataResult.Result.Id;
                personnelRole = await _personnelRoleService.AddAsync(personnelRoleEntity);
            }
            else
            {
                return new DataResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı Kaydı sırasında hata oluştu."
                };
            }

            return new DataResult { IsSuccess = true, PkId = dataResult.Result.Id };
        }
    }
}
