using AutoMapper;
using JobListingApp.AppCores.Interfaces;
using JobListingApp.AppDataAccess.Repository.Interfaces;
using JobListingApp.AppModels.DTOs;
using JobListingApp.AppModels.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepo = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryReturnDto> AddCategory(CategoryDto category)
        {
            var check = await _categoryRepo.GetCategoryByName(category.Name);
            //var res = false;
            if (check == null)
            {

                var newCategory = _mapper.Map<Category>(category);
                try
                {
                    var addCategory = await _categoryRepo.Add(newCategory);
                    if (addCategory.Equals(true))
                    {
                        var res = _mapper.Map<CategoryReturnDto>(newCategory);
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


            }
            return null;


        }

        public async Task<List<CategoryReturnDto>> GetAllCategory()
        {
            var listofCategory = new List<CategoryReturnDto>();
            try
            {
                var res = await _categoryRepo.GetAll();
                foreach (var item in res)
                {
                    listofCategory.Add(_mapper.Map<CategoryReturnDto>(item));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listofCategory;
        }

        public async Task<List<CategoryReturnDto>> GetCategories(string name)
        {

            var listofCategory = new List<CategoryReturnDto>();
            try
            {
                var res = await _categoryRepo.GetCategories(name);
                foreach (var item in res)
                {
                    listofCategory.Add(_mapper.Map<CategoryReturnDto>(item));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listofCategory;
        }

        public async Task<CategoryReturnDto> GetCategoryById(string id)
        {
            try
            {
                var res = await _categoryRepo.GetCategoryById(id);
                if (res != null)
                {
                    return _mapper.Map<CategoryReturnDto>(res);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public async Task<CategoryReturnDto> GetCategoryByName(string name)
        {
            try
            {
                var res = await _categoryRepo.GetCategoryByName(name);
                if (res != null)
                {
                    return _mapper.Map<CategoryReturnDto>(res);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public async Task<bool> RemoveCategory(string id)
        {
            var category = await _categoryRepo.GetCategoryById(id);
            if (category != null)
            {
                try
                {
                    var res = await _categoryRepo.Delete(category);
                    if (res.Equals(true))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }

        public async Task<bool> UpdateCategory(string id, CategoryDto category)
        {
            var result = await _categoryRepo.GetCategoryById(id);
            //var result = "";
            var success = false;
            if (result != null)
            {
                //result.;

                var updatedCategory = _mapper.Map<Category>(category);
                updatedCategory.Id = id;
                try
                {
                    var res = await _categoryRepo.Update(updatedCategory);
                    if (res.Equals(true))
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return success;


            }
            return success;

        }
    }
}
