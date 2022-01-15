using JobListingApp.AppCommons;
using JobListingApp.AppCores.Interfaces;
using JobListingApp.AppModels.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _category;


        public CategoryController(ICategoryService category)
        {
            _category = category;

        }
       // [Authorize(Roles = "Admin")]
        [HttpPost("add-category")]
        public async Task<IActionResult> Add(CategoryDto model)
        {
            var res = await _category.AddCategory(model);
            if (res != null)
            {
                return Ok(Utilities.BuildResponse<CategoryReturnDto>(true, "Category successfully added!", null, res));
            }
            ModelState.AddModelError("Invalid", "Name already exist");
            return BadRequest(Utilities.BuildResponse<object>(false, "Name already exist", ModelState, null));

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var res = await _category.RemoveCategory(id);
            if (res)
            {
                return Ok(Utilities.BuildResponse<string>(true, "Category successfully deleted!", null, ""));
            }
            ModelState.AddModelError("Invalid", "Category Id does not exist!");
            return BadRequest(Utilities.BuildResponse<object>(false, "Enter a valid category Id", ModelState, null));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("edit-category")]
        public async Task<IActionResult> EditCategory(string id, CategoryDto model)
        {
            var res = await _category.UpdateCategory(id, model);
            if (res)
            {
                return Ok(Utilities.BuildResponse<string>(true, "Update Successful", null, model.Name));
            }
            ModelState.AddModelError("Invalid", "Id does not exit");
            return BadRequest(Utilities.BuildResponse(false, "Enter a valid id", ModelState, ""));
        }
        [HttpGet("get-all-category")]
        public async Task<IActionResult> GetAllCategory(int page, int perPage)
        {
            var category = await _category.GetAllCategory();
            if (category != null)
            {
                var paginatedList = PageList<CategoryReturnDto>.Paginate(category, page, perPage);
                var res = new PaginatedListDto<CategoryReturnDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of category", null, res));

            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record for category found!");
                var res = Utilities.BuildResponse<List<CategoryReturnDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("get-category-by-id")]
        public async Task<IActionResult> GetCategoryId(string id)
        {
            var category = await _category.GetCategoryById(id);
            if (category != null)
            {
                return Ok(Utilities.BuildResponse(true, "Category Details", null, category));

            }
            else
            {
                ModelState.AddModelError("Notfound", "Id does not exist!");
                var res = Utilities.BuildResponse<List<CategoryReturnDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("get-category-by-name")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            var category = await _category.GetCategoryByName(name);
            if (category != null)
            {
                return Ok(Utilities.BuildResponse(true, "Category Details", null, category));

            }
            else
            {
                ModelState.AddModelError("Notfound", "Name does not exist!");
                var res = Utilities.BuildResponse<List<CategoryReturnDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("get-categories-by-name")]
        public async Task<IActionResult> GetCategories(string name, int page, int perPage)
        {
            var category = await _category.GetCategories(name);
            if (category != null)
            {
                var paginatedList = PageList<CategoryReturnDto>.Paginate(category, page, perPage);
                var res = new PaginatedListDto<CategoryReturnDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of category", null, res));


            }
            else
            {
                ModelState.AddModelError("Notfound", "Id does not exist!");
                var res = Utilities.BuildResponse<List<CategoryReturnDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
    }
}
