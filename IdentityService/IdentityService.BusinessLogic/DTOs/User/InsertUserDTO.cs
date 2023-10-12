namespace IdentityService.BusinessLogic.DTOs.User
{
    public class InsertUserDTO
    {
        public int UserRoleId { get; set; }

        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
