namespace Moor.Model.Model.Authorize
{
    public class RolePrivilegeModel /*: BaseModel*/
    {
        public RoleModel Role { get; set; }
        public PrivilegeModel Privilege { get; set; }
    }
}
