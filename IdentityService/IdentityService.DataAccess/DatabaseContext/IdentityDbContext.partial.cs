namespace IdentityService.DataAccess.DatabaseContext
{
    public partial class IdentityDbContext
    {
        public async Task<bool> SaveChangesToDbAsync()
        {
            int saved = await base.SaveChangesAsync();
            return saved > 0;
        }
    }
}
