namespace IdentityService.BusinessLogic.DTOs.TokenDTOs
{
    public class TokensDTO
    {
        public string RefreshToken { get; set; } = null!;
        public string EncodedToken { get; set; } = null!;
    }
}
