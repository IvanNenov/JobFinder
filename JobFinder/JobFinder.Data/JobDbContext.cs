using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data
{
    public class JobDbContext : IdentityDbContext<User>
    {
        public JobDbContext(DbContextOptions options)
          : base(options)
        {
            
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Cv> Cvs { get; set; }

        public DbSet<FormEntry> FormEntries { get; set; }

        public DbSet<JobAdd> JobAdds { get; set; }

        public DbSet<UserJobAdds> UserJobAdds { get; set; }

        public DbSet<FavoriteUserJobAds> FavoriteUserJobAds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<User>(entity =>
            {
               entity.HasOne(x => x.Cv).WithOne(x => x.User)
                   .HasForeignKey<Cv>();
            });
           
            builder.Entity<User>(entity =>
            {
                entity.HasMany(x => x.UserJobAdds).WithOne(x => x.User);
            });

            builder.Entity<JobAdd>(entity =>
            {
                entity.HasMany(x => x.CandidatesForPosition).WithOne(x => x.JobAdd);
            });

            builder.Entity<UserJobAdds>(entity =>
            {
                entity.HasKey(x => new {x.JobAddId, x.UserId}); 
            });

            builder.Entity<User>(entity =>
            {
                entity.HasMany(x => x.FavoriteJobs).WithOne(x => x.User);
            });

            builder.Entity<JobAdd>(entity =>
            {
                entity.HasMany(x => x.FavoriteUserJob).WithOne(x => x.JobAdd);
            });


            builder.Entity<FavoriteUserJobAds>(entity =>
            {
                entity.HasKey(x => new { x.JobAddId, x.UserId });
            });

            base.OnModelCreating(builder);
        }
    }
}
