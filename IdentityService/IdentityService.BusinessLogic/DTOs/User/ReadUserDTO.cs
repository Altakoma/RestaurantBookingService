namespace IdentityService.BusinessLogic.DTOs.User
{
    public class ReadUserDTO
    {
        public int Id { get; set; }

        public string UserRoleName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
