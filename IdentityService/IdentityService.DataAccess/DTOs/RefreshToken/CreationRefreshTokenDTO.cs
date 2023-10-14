namespace IdentityService.DataAccess.DTOs.RefreshToken
{
    public class CreationRefreshTokenDTO
    {
        public int Id { get; set; }
        public string UserRoleName { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
