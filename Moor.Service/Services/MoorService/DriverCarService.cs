using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using Moor.Model.Utilities;
using Moor.Model.Models.MoorModels.DriverCarModel;
using Moor.Core.Extension.String;
using Moor.Core.Enums;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Model.Models.MoorModels.DriverModel;
using Moor.Service.Utilities.AuthorizeHelpers;
using Moor.Model.Model.Authorize;

namespace Moor.Service.Services.MoorService
{
    public class DriverCarService : Service<DriverCarEntity>, IDriverCarService
    {
        private readonly ICarService _carService;
        private readonly IDriverService _driverService;
        private readonly IPersonnelService _personnelService;
        private readonly IPersonnelRoleService _personnelRoleService;
        private readonly IDriverCarRepository _driverCarRepository;
        private readonly IMapper _mapper;

        public DriverCarService(IGenericRepository<DriverCarEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IDriverCarRepository driverCarRepository, ICarService carService, IDriverService driverService, IPersonnelService personnelService, IPersonnelRoleService personnelRoleService) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _driverCarRepository = driverCarRepository;
            _carService = carService;
            _driverService = driverService;
            _personnelService = personnelService;
            _personnelRoleService = personnelRoleService;
        }

        public async Task<DataResult> Save(DriverModel driverModel)
        {
            #region
            DataResult dataResult = new DataResult();
            DriverEntity driverEntity = new DriverEntity();
            DriverCarEntity driverCarEntity = new DriverCarEntity();
            CarEntity carEntity = new CarEntity();
            #endregion

            #region DriverPersonnel

            string base64Data = driverModel.PersonnelModel.MediaPath.IsNotNullOrEmpty() ? driverModel.PersonnelModel.MediaPath : string.Empty ; // Base64 kodu
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

            var hashedPassword = HashingHelper.CreatePasswordHash(driverModel.PersonnelModel.Password);
            var personnelEntity = _personnelService.AddAsync(new PersonnelEntity
            {
                Email = driverModel.PersonnelModel.Email,
                FirstName = driverModel.PersonnelModel.FirstName,
                MediaPath = filePath.IsNotNullOrEmpty() ? filePath : string.Empty,
                Password = hashedPassword,
                UserName = driverModel.PersonnelModel.UserName,
                Status = ((int)Status.AKTIF),
                LastName = driverModel.PersonnelModel.LastName,
            });
            #endregion

            if (personnelEntity.Result.IsNotNull() && personnelEntity.Result.Id.IsNotNull())
            {
                PersonnelRoleEntity personnelRoleEntity = new PersonnelRoleEntity();
                personnelRoleEntity.IsDeleted = false;
                personnelRoleEntity.RoleId = 88;
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

            carEntity.CarParameterId = driverModel.CarParameterId;
            carEntity.NumberPlate = driverModel.Plate;
            carEntity.Status = ((int)Status.AKTIF);
            carEntity.CreatedDate = DateTime.Now;
            carEntity.IsDeleted = false;

            var carEntityResult = await _carService.AddAsync(carEntity);
            if (carEntityResult.Id.IsNotNull())
            {
                driverEntity.Status = ((int)Status.AKTIF);
                driverEntity.CreatedDate = DateTime.Now;
                driverEntity.IsDeleted = false;
                driverEntity.PersonnelId = personnelEntity.Result.Id;
                driverEntity.Price = driverModel.Price;
                var driverEntityResult = await _driverService.AddAsync(driverEntity);
                if (driverEntityResult.Id.IsNotNull())
                {
                    driverCarEntity.Status = ((int)Status.AKTIF);
                    driverCarEntity.CreatedDate = DateTime.Now;
                    driverCarEntity.DriverId = driverEntityResult.Id;
                    driverCarEntity.CarId = carEntityResult.Id;
                    var driverCarEntityResult = await base.AddAsync(driverCarEntity);
                    if (driverCarEntityResult.IsNotNull() && driverCarEntityResult.Id.IsNotNull())
                    {
                        return new DataResult
                        {
                            IsSuccess = true,
                            PkId = driverEntityResult.Id
                        };
                    }
                }
                else
                {
                    return new DataResult
                    {
                        IsSuccess = false,
                        ErrorMessage = "Şoför Kaydı sırasında hata oluştu."
                    };
                }
            }
            else
            {
                return new DataResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Araç oluşturulurken hata oluştu."
                };
            }
            return dataResult;
        }
    }
}
