namespace OnLineBookStore.Server.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class OnLineBookStoreDbContext : IdentityDbContext<User>
    {
        public OnLineBookStoreDbContext(DbContextOptions<OnLineBookStoreDbContext> options)
            : base(options)
        {
        }

        private DbSet<Book> books;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Book>()
                .HasKey(b => b.Id);
            
            base.OnModelCreating(builder);
        }
    }
}
