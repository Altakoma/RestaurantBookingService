namespace IdentityService.DataAccess.Entities
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<User>? Users { get; set; }
    }
}