using back.Modules;
using Microsoft.EntityFrameworkCore;

namespace back.Context
{
    public class UserContext : DbContext
    {

        public DbSet<User> user { get; set; }
        
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("userId");
                entity.Property(e => e.UserName).HasColumnName("userName");
                entity.Property(e => e.UserType).HasColumnName("userType");
                entity.Property(e => e.UserPassword).HasColumnName("userPassword");

            });
        }  
    }
}