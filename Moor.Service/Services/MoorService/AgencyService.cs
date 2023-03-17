using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using Moor.Model.Utilities;
using Moor.Model.Models.MoorModels.AgencyModel;
using Moor.Core.Extension.String;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Core.Enums;
using Moor.Model.Model.Authorize;
using Moor.Service.Utilities.AuthorizeHelpers;

namespace Moor.Service.Services.MoorService
{
    public class AgencyService : Service<AgencyEntity>, IAgencyService
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly IPersonnelService _personnelService;
        private readonly IPersonnelRoleService _personnelRoleService;
        private readonly IMapper _mapper;

        public AgencyService(IGenericRepository<AgencyEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, IAgencyRepository agencyRepository, IPersonnelService personnelService, IPersonnelRoleService personnelRoleService) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _personnelService = personnelService;
            _personnelRoleService = personnelRoleService;
        }

        public async Task<DataResult> Save(AgencyModel agencyModel)
        {
            #region Object
            DataResult dataResult = new DataResult();
            AgencyEntity agency = new AgencyEntity();
            #endregion

            #region AgencyPersonnel
            var hashedPassword = HashingHelper.CreatePasswordHash(agencyModel.PersonnelModel.Password);
            var personnelEntity = _personnelService.AddAsync(new PersonnelEntity
            {
                Email = agencyModel.PersonnelModel.Email,
                FirstName = agencyModel.PersonnelModel.FirstName,
                Password = hashedPassword,
                MediaPath = agencyModel.PersonnelModel.MediaPath,
                UserName = agencyModel.PersonnelModel.UserName,
                Status = ((int)Status.AKTIF),
                LastName = agencyModel.PersonnelModel.LastName,
            });
            #endregion

            if (personnelEntity.Result.IsNotNull() && personnelEntity.Result.Id.IsNotNull())
            {
                PersonnelRoleEntity personnelRoleEntity = new PersonnelRoleEntity();
                personnelRoleEntity.IsDeleted = false;
                personnelRoleEntity.RoleId = 87;
                personnelRoleEntity.PersonnelId = personnelEntity.Result.Id;
                var personnelRole = await _personnelRoleService.AddAsync(personnelRoleEntity);
            }
            else
            {
                return new DataResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı Kaydı sırasında hata oluştu."
                };
            }
            agency.PersonnelId = personnelEntity.Result.Id;
            agency.Status = ((int)Status.AKTIF);
            agency.Details = agencyModel.AgencyDetails;
            agency.CityId = agencyModel.CityId;
            agency.CountyId = agencyModel.CountyId;
            agency.CreatedDate = DateTime.Now;
            agency.Email = agencyModel.AgencyEmail;
            agency.Name = agencyModel.AgencyName;
            agency.MediaPath = agencyModel.AgencyMediaPath;
            agency.OperationPhoneNumber = agencyModel.OperationPhoneNumber;
            agency.PhoneNumber = agencyModel.AgencyPhoneNumber;
            agency.ReceptionPrice = (decimal)agencyModel.ReceptionPrice;
            agency.TaxNumber = agencyModel.TaxNumber;
            agency.TaxOffice = agencyModel.TaxOffice;
            agency.Title = agencyModel.Title;

            var agencyResult = await base.AddAsync(agency);
            if (agencyResult.Id.IsNotNull() && agencyResult.Id > 0)
            {
                dataResult.PkId = agencyResult.Id;
                dataResult.IsSuccess = true;
                return dataResult;
            }
            else
            {
                dataResult.ErrorMessage = "Acente kayıt edilirken hata oluştu.";
                dataResult.IsSuccess = false;
                return dataResult;
            }
        }

        //public Task<DataResult> Update(AgencyModel agencyModel)
        //{
            
        //}
    }
}
