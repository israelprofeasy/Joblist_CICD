using JobListingApp.AppModels.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobListingApp.AppDataAccess.DataContext
{
    public class JobDbContext : IdentityDbContext<AppUser>
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {

        }
        public DbSet<Job> Job { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Industry> Industry { get; set; }
        public DbSet<CvUpload> CvUpload { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().HasOne(x => x.CvUpload).WithOne(c => c.AppUser).HasForeignKey<AppUser>(x => x.CvUploadId);
            builder.Entity<CvUpload>().HasOne(x => x.AppUser).WithOne(x => x.CvUpload).HasForeignKey<CvUpload>(x => x.AppUserId);
            builder.Entity<JobApplication>().HasOne(x => x.Job).WithMany(c => c.AppliedJobs).HasForeignKey(d => d.JobId);
            builder.Entity<JobApplication>().HasOne(x => x.AppUser).WithMany(c => c.AppliedJobs).HasForeignKey(d => d.AppUserId);
            builder.Entity<Job>().Property(x => x.MinimumSalary).HasColumnType("decimal(18,4)");
            builder.Entity<Job>().Property(x => x.MaximumSalary).HasColumnType("decimal(18,4)");

        }
    }
}
