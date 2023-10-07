namespace IdentityService.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }

        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public UserRole UserRole { get; set; } = null!;
    }
}
