using Microsoft.EntityFrameworkCore;

namespace IdentityServiceDataAccess.DatabaseContext
{
    public partial class IdentityDbContext : DbContext
    {
        public async Task<bool> SaveChangesToDbAsync()
        {
            int saved = await base.SaveChangesAsync();
            return saved > 0;
        }
    }
}
