using AutoMapper;
using Moor.Core.Entities.Base;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Core.Extension.String;
using Moor.Model.Dtos.MoorDto;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Dtos.MoorDto.CarParameterDto;
using Moor.Model.Dtos.MoorDto.CityDto;
using Moor.Model.Dtos.MoorDto.CountyDto;
using Moor.Model.Dtos.MoorDto.DistrictDto;
using Moor.Model.Dtos.MoorDto.PriceDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferPostDto;
using Moor.Model.Dtos.MoorDto.TransferDto.TransferViewDto;
using Moor.Model.Model.Authorize;
using Moor.Model.Models.Base;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Model.Models.MoorModels.CarParameterModel;
using Moor.Model.Models.MoorModels.CityModel;
using Moor.Model.Models.MoorModels.CountryModel;
using Moor.Model.Models.MoorModels.CountyModel;
using Moor.Model.Models.MoorModels.DistrictModel;

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
            CreateMap<CarParameterEntity, CarParameterDto>().ReverseMap();
            #endregion

            #region Car
            CreateMap<CarModel, CarEntity>()
                .ForMember(x => x.CarParameterId, source => source.MapFrom(src => src.CarParameter.Id))
                .IncludeBase<BaseModel, BaseEntity>()
                .ReverseMap();

            #region CarModel-CarDto
            CreateMap<CarModel, CarDto>().ReverseMap();
            #endregion

            #region CarEntity-CarDto
            CreateMap<CarEntity, CarDto>().ReverseMap();
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
            CreateMap<TransferViewDto, TransferEntity>()
                .ForMember(x => x.City.Name, source => source.MapFrom(src => src.CityName))
                .ForMember(x => x.District.Name, source => source.MapFrom(src => src.DistrictName))
                .ForMember(x => x.County.Name, source => source.MapFrom(src => src.CountyName))
                .ForMember(x => x.County.Name, source => source.MapFrom(src => src.CountyName))
                .ForMember(x => x.Location, source => source.MapFrom(src => src.Location))
                .ForMember(x => x.FlightCode, source => source.MapFrom(src => src.FlightCode))
                .ForMember(x => x.CarParameter.Brand, source => source.MapFrom(src => src.CarParameterBrand))
                .ForMember(x => x.CarParameter.Model, source => source.MapFrom(src => src.CarParameterModel))
                .ForMember(x => x.Explanation, source => source.MapFrom(src => src.Explanation))
                .ForMember(x => x.ReceptionType, source => source.MapFrom(src => src.ReceptionType))
                .ForMember(x => x.DirectionType, source => source.MapFrom(src => src.DirectionType))
                .ForMember(x => x.DepartureDate, source => source.MapFrom(src => src.DepartureDate))
                .ForMember(x => x.ReturnDate, source => source.MapFrom(src => src.ReturnDate))
                .ForMember(x => x.Amount, source => source.MapFrom(src => src.Price))
                .ReverseMap()
                .ForPath(x => x.CityName, source => source.MapFrom(x => x.City.Name))
                .ForPath(x => x.DistrictName, source => source.MapFrom(x => x.District.Name));
            #endregion

            #region Traveller
            CreateMap<TravellerDto, TravellerEntity>().ReverseMap();
            #endregion

            #endregion

            #endregion


        }
    }
}
