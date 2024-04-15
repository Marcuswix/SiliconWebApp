using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Entities;
using System.Reflection.Emit;

namespace Infrastructure.Contexts
{
    public class UserContext : IdentityDbContext<UserEntity>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public virtual DbSet<AddressEntity>? Addresses { get; set; }

        public virtual DbSet<UserCourseItemEntity>? UserCourses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEntity>()
                .HasOne(x => x.Address)
                .WithMany(a => a.Users)
                .HasForeignKey(a => a.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserCourseItemEntity>()
                .HasKey(uci => uci.Id);

            builder.Entity<UserEntity>()
                .HasMany(x => x.Courses);


            builder.Entity<UserCourseItemEntity>()
                .HasOne(x => x.UserEntity)
                .WithMany(x => x.Courses);
        }
    }
}

