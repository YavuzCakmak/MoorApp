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
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Model.Models.MoorModels.AgencyModel;

namespace Moor.Service.Services.MoorService
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IMapper _mapper;
        private readonly IPersonnelService _personnelService;
        private readonly IDriverService _driverService;
        private readonly IAgencyService _agencyService;
        private readonly IPersonnelRoleService _personnelRoleService;
        private readonly IRolePrivilegeService _rolePrivilegeService;
        private readonly TokenHelper _tokenHelper;


        public AuthorizeService(IUnitOfWork unitOfWork, IPersonnelService personnelService, IPersonnelRoleService personnelRoleService, IRolePrivilegeService rolePrivilegeService, TokenHelper tokenHelper, IMapper mapper, IAgencyService agencyService, IDriverService driverService)
        {
            _personnelService = personnelService;
            _personnelRoleService = personnelRoleService;
            _rolePrivilegeService = rolePrivilegeService;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
            _agencyService = agencyService;
            _driverService = driverService;
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
            TokenModel tokenModel = new TokenModel();
            tokenModel.PersonnelId = personnel.Id;
            tokenModel.Roles = roleDtos.ToList();
            tokenModel.Username = personnel.UserName;

            List<long> oldMediaFile = new List<long>
            {
                549,570,571,572,573,574,575,576,577,578,579,580,584,585,586,587,588,589,590,591,592,593,594,595,596,597,600,601,602,603,604
            };


            if (personnelRoles.Any(x => x.RoleId == (long)Roles.ACENTE))
            {
                var agencyModel = _agencyService.Where(x => x.PersonnelId == personnel.Id).FirstOrDefault();
                tokenModel.AgencyId = agencyModel.Id;
                var agencyMedia = agencyModel.MediaPath.IsNotNullOrEmpty() ? agencyModel.MediaPath : string.Empty;

                if (!oldMediaFile.Contains(personnel.Id))
                {
                    using (FileStream stream = new FileStream(agencyMedia, FileMode.Open))
                    {
                        byte[] bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, bytes.Length);
                        string base64Data = Convert.ToBase64String(bytes);
                        tokenModel.AgencyMedia = base64Data;
                    }
                }

            }
            else if (personnelRoles.Any(x => x.RoleId == (long)Roles.SOFOR))
            {
                var driverModel = _driverService.Where(x => x.PersonnelId == personnel.Id).FirstOrDefault();
                tokenModel.DriverId = driverModel.Id;

                var personnelMedia = personnel.MediaPath.IsNotNullOrEmpty() ? personnel.MediaPath : string.Empty;

                if (!oldMediaFile.Contains(personnel.Id))
                {
                    using (FileStream stream = new FileStream(personnelMedia, FileMode.Open))
                    {
                        byte[] bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, bytes.Length);
                        string base64Data = Convert.ToBase64String(bytes);
                        tokenModel.DriverMedia = base64Data;
                    }
                }
            }

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


            string base64Data = personnelModel.MediaPath; // Base64 kodu
            string fileName = $"{Guid.NewGuid()}.png"; // Dosya adı
            string directoryPath = @"C:\Users\Dosyalar"; // Klasör yolu

            byte[] bytes = Convert.FromBase64String(base64Data);

            // Klasörü kontrol edin ve oluşturun
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Dosyayı kaydetmek için bir FileStream kullanın
            using (FileStream stream = new FileStream(Path.Combine(directoryPath, fileName), FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            // Dosya yoluyla birlikte geri dönün
            string filePath = Path.Combine(directoryPath, fileName);

            var hashedPassword = HashingHelper.CreatePasswordHash(personnelModel.Password);

            var dataResult = _personnelService.AddAsync(new PersonnelEntity
            {
                Email = personnelModel.Email,
                FirstName = personnelModel.FirstName,
                Password = hashedPassword,
                MediaPath = filePath,
                UserName = personnelModel.UserName,
                Status = ((int)Status.AKTIF), //Default Aktif
                LastName = personnelModel.LastName,
            });

            if (dataResult.Result.IsNotNull() && dataResult.Result.Id.IsNotNull())
            {
                PersonnelRoleEntity personnelRoleEntity = new PersonnelRoleEntity();
                personnelRoleEntity.IsDeleted = false;
                personnelRoleEntity.RoleId = (long)personnelModel.RoleId;
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
