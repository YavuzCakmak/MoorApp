using Moor.Model.Models.Base;

namespace Moor.Model.Model.Authorize
{
    public class PersonnelRoleModel : BaseAuthorizeModel
    {
        public PersonnelModel Personnel { get; set; }
        public RoleModel Role { get; set; }
    }
}
