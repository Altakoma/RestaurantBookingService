namespace CatalogService.Infrastructure.Data.ApplicationDbContext
{
    public partial class CatalogServiceDbContext
    {
        public async Task<bool> SaveChangesToDbAsync()
        {
            int saved = await base.SaveChangesAsync();
            return saved > 0;
        }
    }
}
