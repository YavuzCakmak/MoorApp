using AutoMapper;
using Moor.Core.Entities.Base;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Model.Dtos.MoorDto.CarDto;
using Moor.Model.Dtos.MoorDto.CarParameterDto;
using Moor.Model.Model.Authorize;
using Moor.Model.Models.Base;
using Moor.Model.Models.MoorModels.CarModel;
using Moor.Model.Models.MoorModels.CarParameterModel;

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
            CreateMap<RoleEntity, RoleModel>().ReverseMap().IncludeBase<BaseAuthorizeModel, BaseAuthorizeEntity>();
            CreateMap<PrivilegeEntity, PrivilegeModel>().ReverseMap().IncludeBase<BaseAuthorizeModel, BaseAuthorizeEntity>();

            CreateMap<RolePrivilegeModel, RolePrivilegeEntity>()
                .ForMember(x => x.RoleId, source => source.MapFrom(src => src.Role.Id))
                .ForMember(x => x.PrivilegeId, source => source.MapFrom(src => src.Privilege.Id))
                .IncludeBase<BaseAuthorizeModel, BaseAuthorizeEntity>()
                .ReverseMap();
            #endregion*


            #region Moor

            #region CarParameter
            CreateMap<CarParameterEntity, CarParameterModel>().ReverseMap().IncludeBase<BaseModel, BaseEntity>();
            CreateMap<CarParameterEntity, CarParameterDto>().ReverseMap();
            #endregion

            #region Car
            CreateMap<CarModel, CarEntity>()
                .ForMember(x=> x.CarParameterId, source => source.MapFrom(src => src.CarParameter.Id))
                .IncludeBase<BaseModel, BaseEntity>()
                .ReverseMap();

            #region CarModel-CarDto
            CreateMap<CarModel, CarDto>().ReverseMap();
            #endregion

            #region CarEntity-CarDto
            CreateMap<CarEntity, CarDto>().ReverseMap();
            #endregion

            #endregion

            #endregion


        }
    }
}
