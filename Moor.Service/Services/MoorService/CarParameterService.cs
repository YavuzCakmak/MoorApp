using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Repositories;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;
using Moor.Model.Dtos.MoorDto.CarParameterDto;
using Moor.Core.Extension.String;
using Moor.Core.SSH.Concretion;

namespace Moor.Service.Services.MoorService
{
    public class CarParameterService : Service<CarParameterEntity>, ICarParameterService
    {
        private readonly ICarParameterRepository _carParameterRepository;
        private readonly IMapper _mapper;
        private readonly SshHelper sshHelper;

        public CarParameterService(IGenericRepository<CarParameterEntity> repoistory, IUnitOfWork unitOfWork, IMapper mapper, ICarParameterRepository carParameterRepository, SshHelper sshHelper) : base(repoistory, unitOfWork)
        {
            _mapper = mapper;
            _carParameterRepository = carParameterRepository;
            this.sshHelper = sshHelper;
        }

        public async Task<CarParameterDto> Save(CarParameterDto carParameterDto)
        {
            if (carParameterDto.MediaPath.IsNotNullOrEmpty())
            {
                string folderPath = carParameterDto.Model;
                string fileName = carParameterDto.Brand + "." + carParameterDto.Model;
                var mediaUploadResult = sshHelper.WriteFile(fileName, folderPath, carParameterDto.MediaPath);
                if (mediaUploadResult.IsSuccess)
                {
                    carParameterDto.MediaPath = mediaUploadResult.Link;
                }
            }
            var carParameterEntity = await base.AddAsync(_mapper.Map<CarParameterEntity>(carParameterDto));
            var newCarParameterDto = _mapper.Map<CarParameterDto>(carParameterEntity);
            return newCarParameterDto;
        }
    }
}
