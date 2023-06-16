using Microsoft.EntityFrameworkCore;

namespace PMS_API.Data
{
    public class Api_PMSContext: DbContext
    {
        public Api_PMSContext(DbContextOptions<Api_PMSContext> context) : base(context) { }

        public DbSet<Models.Users> Users { get; set; } = default!;
    }
}
