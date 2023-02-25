using AutoMapper;
using Moor.Core.Entities.Base;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using Moor.Model.Model.Authorize;
using Moor.Model.Models.Base;

namespace Moor.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<BaseModel, BaseEntity>().ReverseMap();
            CreateMap<BaseAuthorizeEntity, BaseAuthorizeModel>().ReverseMap();


            CreateMap<RoleEntity, RoleModel>().ReverseMap().IncludeBase<BaseAuthorizeModel, BaseAuthorizeEntity>();
            CreateMap<PrivilegeEntity, PrivilegeModel>().ReverseMap().IncludeBase<BaseAuthorizeModel, BaseAuthorizeEntity>();

            CreateMap<RolePrivilegeModel, RolePrivilegeEntity>()
                .ForMember(x => x.RoleId, source => source.MapFrom(src => src.Role.Id))
                .ForMember(x => x.PrivilegeId, source => source.MapFrom(src => src.Privilege.Id))
                .IncludeBase<BaseAuthorizeModel, BaseAuthorizeEntity>()
                .ReverseMap();

        }
    }
}
