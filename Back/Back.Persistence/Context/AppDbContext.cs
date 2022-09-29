using Back.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Back.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
    }
}
