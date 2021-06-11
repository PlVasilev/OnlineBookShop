namespace OnLineBookStore.Server.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class OnLineBookStoreDbContext : IdentityDbContext
    {
        public OnLineBookStoreDbContext(DbContextOptions<OnLineBookStoreDbContext> options)
            : base(options)
        {
        }
    }
}
