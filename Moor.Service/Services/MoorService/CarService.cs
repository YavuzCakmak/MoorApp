﻿using AutoMapper;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Services.MoorService;
using Moor.Core.UnitOfWorks;
using Moor.Service.Services.BaseService;

namespace Moor.Service.Services.MoorService
{
    public class CarService : Service<CarEntity>, ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(IGenericRepository<CarEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, ICarRepository carRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }
    }
}