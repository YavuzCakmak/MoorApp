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
using Moor.Model.Models.MoorModels.DriverModel;

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

            string base64Data = agencyModel.PersonnelModel.MediaPath.IsNotNullOrEmpty() ? agencyModel.PersonnelModel.MediaPath : string.Empty; // Base64 kodu
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


            #region AgencyPersonnel
            var hashedPassword = HashingHelper.CreatePasswordHash(agencyModel.PersonnelModel.Password);
            var personnelEntity = _personnelService.AddAsync(new PersonnelEntity
            {
                Email = agencyModel.PersonnelModel.Email,
                FirstName = agencyModel.PersonnelModel.FirstName,
                Password = hashedPassword,
                MediaPath = filePath,
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

            string agencyBase64Data = agencyModel.AgencyMediaPath.IsNotNullOrEmpty() ? agencyModel.AgencyMediaPath : string.Empty; // Base64 kodu
            string agencyfileName = $"{Guid.NewGuid()}.png"; 
            string agencydirectoryPath = @"C:\Users\Dosyalar"; 

            byte[] agencybytes = Convert.FromBase64String(agencyBase64Data);

            if (!Directory.Exists(agencydirectoryPath))
            {
                Directory.CreateDirectory(agencydirectoryPath);
            }

            using (FileStream stream = new FileStream(Path.Combine(agencydirectoryPath, agencyfileName), FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            string agencyfilePath = Path.Combine(directoryPath, agencyfileName);


            agency.PersonnelId = personnelEntity.Result.Id;
            agency.Status = ((int)Status.AKTIF);
            agency.Details = agencyModel.AgencyDetails;
            agency.CityId = agencyModel.CityId;
            agency.CountyId = agencyModel.CountyId;
            agency.CreatedDate = DateTime.Now;
            agency.Email = agencyModel.AgencyEmail;
            agency.Name = agencyModel.AgencyName;
            agency.MediaPath = agencyfilePath;
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
