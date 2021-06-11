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
        public DbSet<Book> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartBook> CartBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Book>()
                .HasKey(b => b.Id);

            builder.Entity<Cart>()
                .HasKey(b => b.Id);

            builder.Entity<CartBook>()
                .HasKey(k => new { k.CartId, k.BookId });

            builder.Entity<CartBook>()
                .HasOne(rp => rp.Cart)
                .WithMany(r => r.CartBooks)
                .HasForeignKey(rp => rp.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CartBook>()
                .HasOne(rp => rp.Book)
                .WithMany(p => p.CartBooks)
                .HasForeignKey(rp => rp.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
