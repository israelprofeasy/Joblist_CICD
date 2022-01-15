using JobListingApp.AppDataAccess.DataContext;
using JobListingApp.AppDataAccess.Repository.Interfaces;
using JobListingApp.AppModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListingApp.AppDataAccess.Repository.Implementations
{
    public class CvUpLoadRepository : ICVUpload
    {
        private readonly JobDbContext _ctx;

        public CvUpLoadRepository(JobDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> Add<T>(T entity)
        {
            // _ctx.CvUpload.AsNoTracking();
            //_ctx.Entry(entity).State = EntityState.Detached;
            var cv = entity as CvUpload;
            _ctx.CvUpload.Add(cv);
            return await SaveChanges();
        }

        public async Task<bool> Delete<T>(T entity)
        {
            _ctx.Remove(entity);
            return await SaveChanges();
        }

        public async Task<IEnumerable<CvUpload>> GetAllUploads()
        {
            return await _ctx.CvUpload.ToListAsync();
        }

        public async Task<CvUpload> GetUpload(string userId)
        {
            return await _ctx.CvUpload.Where(x => x.AppUserId == userId).FirstOrDefaultAsync();
        }
        public async Task<CvUpload> GetCvByPublicId(string PublicId)
        {
            return await _ctx.CvUpload.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.PublicId == PublicId);
        }
        public async Task<int> RowCount()
        {
            return await _ctx.CvUpload.CountAsync();
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
