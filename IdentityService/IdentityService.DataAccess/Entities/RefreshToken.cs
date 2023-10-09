namespace IdentityService.DataAccess.Entities
{
    public class RefreshToken
    {
        public int UserId { get; set; }
        public string Token { get; set; } = null!;
        public bool IsUsed { get; set; }
        public bool isRevoked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public User User { get; set; } = null!;
    }
}
