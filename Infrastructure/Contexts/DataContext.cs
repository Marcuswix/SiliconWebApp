using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Entities;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

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

        public DbSet<TeacherEntity> Teachers { get; set; }

        public DbSet<ProgramDetailsEntity> ProgramDetails { get; set; }

        public DbSet<ProgramDetailsItemsEntity> ProgramDetailsItems { get; set; }

        public DbSet<WhatYouLearnEntity> WhatYouLearn { get; set; }

        public DbSet<WhatYouLearnItemsEntity> WhatYouLearnItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CourseEntity>()
                .HasOne(x => x.Category)
                .WithMany(a => a.Courses)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WhatYouLearnItemsEntity>()
            .HasKey(wyli => wyli.Id);

            builder.Entity<WhatYouLearnEntity>()
                .HasMany(x => x.whatYouLearnItems);

            builder.Entity<WhatYouLearnEntity>()
                .HasKey(x => x.Id);

            builder.Entity<WhatYouLearnItemsEntity>()
                .HasOne(wyli => wyli.WhatYouLearnEntity)
                .WithMany(wyl => wyl.whatYouLearnItems)
                .HasForeignKey(wyl => wyl.WhatYouLearnId);

            builder.Entity<ProgramDetailsEntity>()
                .HasMany(p => p.ProgramDetails)
                .WithOne(pi => pi.ProgramDetailsEntity)
                .HasForeignKey(pi => pi.ProgramDetailsId);

        }
    }
}
