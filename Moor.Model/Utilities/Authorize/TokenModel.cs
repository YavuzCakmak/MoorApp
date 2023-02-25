using Moor.Model.Utilities.Authorize;

namespace Moor.Model.Utilities.TokenModel
{
    public class TokenModel
    {
        public long PersonnelId { get; set; }
        public string Username { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenEndDate { get; set; }
        public string ValidTo { get; set; }
        public List<Role> Roles { get; set; }
    }
}
