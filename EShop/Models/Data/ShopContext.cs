using Microsoft.EntityFrameworkCore;

namespace EShop.Models.Data
{
    public class ShopContext :DbContext
    {
        public ShopContext(DbContextOptions options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            //modelBuilder.Entity<UserRole>()
            //    .HasOne<Users>(ul => ul.Users)
            //    .WithMany(ur => ur.UserRole)
            //.HasForeignKey(ul => ul.UserId);


            //modelBuilder.Entity<UserRole>()
            //    .HasOne<Roles>(ur => ur.Roles)
            //    .WithMany(s => s.UserRole)
            //    .HasForeignKey(ur => ur.RoleId);

        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
    }
}
