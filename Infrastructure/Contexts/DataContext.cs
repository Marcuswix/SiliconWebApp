using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<FeatureEntity> Features { get; set; }

        public DbSet<FeatureItemEntity> FeatureItems { get; set; }

        public DbSet<IntegrateItemEntity> IntegrateItem { get; set; }

        public DbSet<IntegrateEntity> Integrate {  get; set; }

        public DbSet<ManageItemEntity> ManageItems { get; set; }

        public DbSet<ManageEntity> Manage { get; set; }

        public DbSet<SubscriberEntity> Subscribers { get; set; }

        public DbSet<CourseEntity> Courses { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CourseEntity>()
                .HasOne(x => x.Category)
                .WithMany(a => a.Courses)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
