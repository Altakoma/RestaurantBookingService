namespace IdentityService.BusinessLogic.DTOs.UserDTOs
{
    public class UpdateUserDTO
    {
        public int UserRoleId { get; set; }
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
