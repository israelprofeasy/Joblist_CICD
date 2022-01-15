using JobListingApp.AppDataAccess.DataContext;
using JobListingApp.AppDataAccess.Repository.Interfaces;
using JobListingApp.AppModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListingApp.AppDataAccess.Repository.Implementations
{
    public class JobApplicationRepository : IJobApplicationRepo
    {
        private readonly JobDbContext _ctx;

        public JobApplicationRepository(JobDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> Add<T>(T entity)
        {
            await _ctx.AddAsync(entity);
            return await SaveChanges();
        }

        public async Task<bool> Delete<T>(T entity)
        {
            _ctx.Remove(entity);
            return await SaveChanges();
        }

        public async Task<IEnumerable<AppUser>> JobApplications(string jobId)
        {
            var res = await _ctx.JobApplication.Where(x => x.JobId == jobId).Include(x => x.AppUser).Select(x => x.AppUser).ToListAsync();
            return res;
        }

        public async Task<int> RowCount()
        {
            return await _ctx.JobApplication.CountAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public Task<bool> Update<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Job>> UserApplications(string userId)
        {
            var res = await _ctx.JobApplication.Where(x => x.AppUserId == userId).Include(x => x.Job).Select(x => x.Job).ToListAsync();
            return res;
        }
    }
}
