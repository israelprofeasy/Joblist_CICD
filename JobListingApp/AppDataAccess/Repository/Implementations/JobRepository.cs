using JobListingApp.AppDataAccess.DataContext;
using JobListingApp.AppDataAccess.Repository.Interfaces;
using JobListingApp.AppModels.Enums;
using JobListingApp.AppModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListingApp.AppDataAccess.Repository.Implementations
{
    public class JobRepository : IJobRepository
    {
        private readonly JobDbContext _ctx;

        public JobRepository(JobDbContext ctx)
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

        public async Task<Job> GetJobById(string id)
        {
            var res = await _ctx.Job.Where(x => x.Id == id).Include(x => x.Category).Include(x => x.Industry).FirstOrDefaultAsync();
            //if (res != null)
            //    _ctx.Entry(res).State = EntityState.Detached;
            return res;
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            return await _ctx.Job.ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByCategory(string categoryId)
        {
            return await _ctx.Job.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByIndustry(string industryId)
        {
            return await _ctx.Job.Where(c => c.IndustryId == industryId).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByLocation(Locations location)
        {
            return await _ctx.Job.Where(x => x.Location == location).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByName(string name)
        {
            return await _ctx.Job.Where(x => x.JobTitle.Contains(name) || x.Company.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByNature(JobNature jobNature)
        {
            return await _ctx.Job.Where(x => x.JobNature == jobNature).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsBySalaryRange(decimal minimum, decimal maximum)
        {
            return await _ctx.Job.Where(x => x.MinimumSalary >= minimum && x.MaximumSalary <= maximum).OrderBy(x => x.MinimumSalary).ToListAsync();
        }

        public async Task<bool> JobExists(string jobTitle, string company)
        {
            return await _ctx.Job.AnyAsync(x => x.JobTitle == jobTitle && x.Company == company);
        }

        public async Task<int> RowCount()
        {
            return await _ctx.Job.CountAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update<T>(T entity)
        {
            _ctx.Update(entity);
            return await SaveChanges();
        }
    }
}
