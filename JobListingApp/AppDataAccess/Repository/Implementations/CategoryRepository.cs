using JobListingApp.AppDataAccess.DataContext;
using JobListingApp.AppDataAccess.Repository.Interfaces;
using JobListingApp.AppModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobListingApp.AppDataAccess.Repository.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly JobDbContext _ctx;

        public CategoryRepository(JobDbContext ctx)
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

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _ctx.Category.ToListAsync();
        }

        public async Task<Category> GetCategoryById(string id)
        {
            var res = await _ctx.Category.Where(x => x.Id == id).FirstOrDefaultAsync();
            //if(res != null)
            // _ctx.Entry(res).State = EntityState.Detached;
            return res;
        }

        public async Task<IEnumerable<Category>> GetCategories(string name)
        {
            return
                await _ctx.Category.Where(x => x.Name.Contains(name)).ToListAsync();
        }

        public async Task<int> RowCount()
        {
            return await _ctx.Category.CountAsync();
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

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _ctx.Category.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
