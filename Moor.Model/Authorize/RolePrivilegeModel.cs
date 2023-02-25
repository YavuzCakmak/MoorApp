using Moor.Model.Models.Base;

namespace Moor.Model.Model.Authorize
{
    public class RolePrivilegeModel : BaseAuthorizeModel
    {
        public RoleModel Role { get; set; }
        public PrivilegeModel Privilege { get; set; }
    }
}
