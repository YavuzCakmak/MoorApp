namespace Moor.Service.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Department { get; set; }
        public bool IsDeleted { get; set; }
    }
}
