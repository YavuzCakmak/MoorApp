using AutoMapper;
using Moor.Core.Entities.Base;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Core.Extension.String;
using Moor.Model.Dtos.MoorDto;
using Moor.Model.Dtos.MoorDto.AgencyDto;
using Moor.Model.Dtos.MoorDto.CarBrandDto;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Dtos.MoorDto.CarModelDto;
using Moor.Model.Dtos.MoorDto.CarParameterDto;
using Moor.Model.Dtos.MoorDto.CityDto;
using Moor.Model.Dtos.MoorDto.CountyDto;
using Moor.Model.Dtos.MoorDto.DistrictDto;
using Moor.Model.Dtos.MoorDto.DriverDto;
using Moor.Model.Dtos.MoorDto.PriceDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferPostDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using Moor.Model.Model.Authorize;
using Moor.Model.Models.Base;
using Moor.Model.Models.MoorModels.AgencyModel;
using Moor.Model.Models.MoorModels.CarBrandModel;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Model.Models.MoorModels.CityModel;
using Moor.Model.Models.MoorModels.CountryModel;
using Moor.Model.Models.MoorModels.CountyModel;
using Moor.Model.Models.MoorModels.DistrictModel;
using Moor.Model.Models.MoorModels.NotificationModel;
using Moor.Model.Models.MoorModels.TransferModel;

namespace Moor.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Model
            CreateMap<BaseModel, BaseEntity>().ReverseMap();
            CreateMap<BaseAuthorizeEntity, BaseAuthorizeModel>().ReverseMap();
            #endregion

            #region Authorize
            CreateMap<RoleEntity, RoleModel>().ReverseMap().IncludeBase<BaseModel, BaseEntity>();
            #endregion*


            #region Moor

            #region CarParameter
            CreateMap<CarParameterEntity, CarParameterModel>().ReverseMap().IncludeBase<BaseModel, BaseEntity>();
            CreateMap<CarParameterEntity, CarParameterDto>()
                .ForMember(x => x.CarBrandName, source => source.MapFrom(src => src.CarBrand.Brand))
                .ForMember(x => x.CarModelName, source => source.MapFrom(src => src.CarModel.Model))
                .ReverseMap();
            #endregion

            #region CarBrand
            CreateMap<CarBrandEntity, CarBrandModel>().ReverseMap().IncludeBase<BaseModel, BaseEntity>();
            CreateMap<CarBrandEntity, CarBrandDto>().ReverseMap();
            #endregion

            #region CarModel
            CreateMap<CarModelEntity, CarModel>().ReverseMap().IncludeBase<BaseModel, BaseEntity>();
            CreateMap<CarModelEntity, CarModelDto>().ReverseMap();
            #endregion

            #region Car
            CreateMap<CarEntity, CarDto>()
               .ForMember(x => x.CarParameterId, source => source.MapFrom(src => src.CarParameter.Id))
               .ForMember(x => x.CarBrand, source => source.MapFrom(src => src.CarParameter.CarBrand.Brand))
               .ForMember(x => x.CarModel, source => source.MapFrom(src => src.CarParameter.CarModel.Model));
            #endregion

            #region DriverCar 
            CreateMap<DriverCarEntity, DriverCarDto>()
              .ForMember(x => x.CarId, source => source.MapFrom(src => src.Car.Id))
              .ForMember(x => x.CarBrand, source => source.MapFrom(src => src.Car.CarParameter.CarBrand.Brand))
              .ForMember(x => x.Price, source => source.MapFrom(src => src.Driver.Price))
              .ForMember(x => x.CarModel, source => source.MapFrom(src => src.Car.CarParameter.CarModel.Model));
            #endregion

            #endregion

            #region District
            CreateMap<DistrictModel, DistrictEntity>()
             .IncludeBase<BaseModel, BaseEntity>()
             .ReverseMap();


            CreateMap<DisctrictDto, DistrictEntity>().ReverseMap();
            #endregion

            #region Country
            CreateMap<CountryModel, CountryEntity>()
             .IncludeBase<BaseModel, BaseEntity>()
             .ReverseMap();
            #endregion

            #region City
            CreateMap<CityModel, CityEntity>()
              .ForMember(x => x.CountryId, source => source.MapFrom(src => src.Country.Id))
             .IncludeBase<BaseModel, BaseEntity>()
             .ReverseMap();

            CreateMap<CityDto, CityEntity>().ReverseMap();
            #endregion

            #region County
            CreateMap<CountyModel, CountyEntity>()
              .ForMember(x => x.CityId, source => source.MapFrom(src => src.City.Id))
             .IncludeBase<BaseModel, BaseEntity>()
             .ReverseMap();

            #region CountyDto
            CreateMap<CountyDto, CountyEntity>().ReverseMap();
            #endregion

            #region Price
            CreateMap<PriceDto, PriceEntity>().ReverseMap();
            #endregion


            #region Transfer 
            CreateMap<TransferPostDto, TransferEntity>().ReverseMap();
            CreateMap<TransferModel, TransferEntity>().ReverseMap();
            #endregion

            #region Traveller
            CreateMap<TravellerDto, TravellerEntity>().ReverseMap();
            #endregion

            #region Driver
            CreateMap<DriverEntity, DriverDto>()
                .ForMember(x => x.PersonnelId, source => source.MapFrom(src => src.Personnel.Id))
                .ForMember(x => x.FirstName, source => source.MapFrom(src => src.Personnel.FirstName))
                .ForMember(x => x.LastName, source => source.MapFrom(src => src.Personnel.LastName))
                .ForMember(x => x.Price, source => source.MapFrom(src => src.Price))
                .ReverseMap();
            #endregion

            #region Notification 
            CreateMap<NotificationEntity, NotificationModel>()
                .ForMember(x => x.Id, source => source.MapFrom(src => src.Id))
                .ForMember(x => x.TransferId, source => source.MapFrom(src => src.TransferId))
                .ForMember(x => x.AgencyId, source => source.MapFrom(src => src.AgencyId))
                .ForMember(x => x.Explanation, source => source.MapFrom(src => src.Explanation)).IncludeBase<BaseEntity, BaseModel>().ReverseMap();
            #endregion

            #region Agency 
            CreateMap<AgencyEntity, AgencyDto>().ReverseMap();
            CreateMap<AgencyEntity, AgencyModel>().ReverseMap();
            #endregion

            #endregion

        }
    }
}
