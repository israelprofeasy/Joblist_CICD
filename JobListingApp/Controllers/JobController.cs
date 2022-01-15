using JobListingApp.AppCommons;
using JobListingApp.AppCores.Interfaces;
using JobListingApp.AppModels.DTOs;
using JobListingApp.AppModels.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobServices _jobService;
        private readonly ICategoryService _categoryService;
        private readonly IIndustryService _industryService;

        public JobController(IJobServices jobService, ICategoryService categoryService, IIndustryService industryService)
        {
            _jobService = jobService;
            _categoryService = categoryService;
            _industryService = industryService;
        }
       // [Authorize(Roles = "Admin")]
        [HttpPost("Add-Job")]
        public async Task<IActionResult> AddJob(JobDetailDto model)
        {
            var categoryCheck = await _categoryService.GetCategoryById(model.CategoryId);
            var industryCheck = await _industryService.GetIndustryById(model.IndustryId);
            if (categoryCheck == null || industryCheck == null)
            {
                ModelState.AddModelError("Invalid", "Invalid Category or industry Id");
                return BadRequest(Utilities.BuildResponse<object>(false, "Enter a valid category and industry Id", ModelState, null));
            }
            var res = await _jobService.AddJob(model);
            if (res != null)
            {
                return Ok(Utilities.BuildResponse<JobPreviewDto>(true, "Job successfully added!", null, res));
            }
            ModelState.AddModelError("Invalid", "Job Listing already exist");
            return BadRequest(Utilities.BuildResponse<object>(false, "Add a new Job", ModelState, null));

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Job")]
        public async Task<IActionResult> DeleteJob(string id)
        {

            var res = await _jobService.RemoveJob(id);
            if (res)
            {
                return Ok(Utilities.BuildResponse<string>(true, "Job successfully deleted!", null, ""));
            }
            ModelState.AddModelError("Invalid", "Job Id does not exist!");
            return BadRequest(Utilities.BuildResponse<object>(false, "Enter a valid job Id", ModelState, null));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Edit-Job")]
        public async Task<IActionResult> EditJob(string id, JobDetailDto model)
        {

            var res = await _jobService.UpdateJob(id, model);
            if (res)
            {
                return Ok(Utilities.BuildResponse<string>(true, "Update Successful", null, ""));
            }
            ModelState.AddModelError("Invalid", "Id does not exit");
            return BadRequest(Utilities.BuildResponse(false, "Enter a valid Job id", ModelState, ""));
        }
        [HttpGet("Get-Job-by-name/{name}")]
        public async Task<IActionResult> GetJobsByName(string name, int page, int perPage)
        {

            var jobs = await _jobService.GetJobsByName(name);
            if (jobs.Count > 0)
            {
                var paginatedList = PageList<JobPreviewDto>.Paginate(jobs, page, perPage);
                var res = new PaginatedListDto<JobPreviewDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of Jobs", null, res));

            }
            else
            {
                ModelState.AddModelError("Notfound", $"There was no record found with the name '{name}'!");
                var res = Utilities.BuildResponse<List<JobPreviewDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("Get-Job/{id}")]
        public async Task<IActionResult> GetJobsById(string id)
        {

            var job = await _jobService.GetJobById(id);
            if (job != null)
            {
                return Ok(Utilities.BuildResponse(true, "Job Details", null, job));

            }
            else
            {
                ModelState.AddModelError("Notfound", "Id does not exist!");
                var res = Utilities.BuildResponse<object>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }

        [HttpGet("Get-Jobs")]
        public async Task<IActionResult> GetJobs(int page, int perPage)
        {
            var jobs = await _jobService.GetAllJobs();

            if (jobs != null)
            {
                var paginatedList = PageList<JobPreviewDto>.Paginate(jobs, page, perPage);
                var res = new PaginatedListDto<JobPreviewDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of Jobs", null, res));

            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record found!");
                var res = Utilities.BuildResponse<object>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }

        }

        [HttpGet("GetJob/Category/{name}")]
        public async Task<IActionResult> GetJobByCategory(string name, int page, int perPage)
        {
            var category = await _categoryService.GetCategoryByName(name);
            if (category == null)
            {
                ModelState.AddModelError("Notfound", "Category name not found!");
                var res = Utilities.BuildResponse<object>(false, "Category does not exist!", ModelState, null);
                return NotFound(res);
            }

            var jobs = await _jobService.GetJobsByCategory(category.Id);
            if (jobs != null)
            {
                var paginatedList = PageList<JobPreviewDto>.Paginate(jobs, page, perPage);
                var res = new PaginatedListDto<JobPreviewDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of Jobs", null, res));

            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record found!");
                var res = Utilities.BuildResponse<object>(false, $"No results found under {name} category!", ModelState, null);
                return NotFound(res);
            }
        }

        [HttpGet("Get-Job/Industry/{name}")]
        public async Task<IActionResult> GetJobByIndustry(string name, int page, int perPage)
        {

            var industry = await _industryService.GetIndustryByName(name);
            if (industry == null)
            {
                ModelState.AddModelError("Notfound", "Industry name not found!");
                var res = Utilities.BuildResponse<object>(false, "Industry does not exist!", ModelState, null);
                return NotFound(res);
            }

            var jobs = await _jobService.GetJobsByIndustry(industry.Id);
            if (jobs != null)
            {
                var paginatedList = PageList<JobPreviewDto>.Paginate(jobs, page, perPage);
                var res = new PaginatedListDto<JobPreviewDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of Jobs", null, res));

            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record found!");
                var res = Utilities.BuildResponse<object>(false, $"No results found under {name} industry!", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("Get-Job/Location/{name}")]
        public async Task<IActionResult> GetJobByLocation(string name, int page, int perPage)
        {
            var check = Enum.IsDefined(typeof(Locations), name);
            if (!check)
            {
                ModelState.AddModelError("Notfound", "Location name not found!");
                var res = Utilities.BuildResponse<object>(false, "Location does not exist!", ModelState, null);
                return NotFound(res);
            }
            Enum.TryParse(name, out Locations location);
            var jobs = await _jobService.GetJobsByLocation(location);
            if (jobs != null)
            {
                var paginatedList = PageList<JobPreviewDto>.Paginate(jobs, page, perPage);
                var res = new PaginatedListDto<JobPreviewDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of Jobs", null, res));

            }
            else
            {
                ModelState.AddModelError("Notfound", $"No Jobs found with \'{name}\'!");
                var res = Utilities.BuildResponse<object>(false, "Location has Job!", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("Get-Job/SalaryRange/{name}")]
        public async Task<IActionResult> GetJobBySalaryRange(int minimum, int maximum, int page, int perPage)
        {
            var jobs = await _jobService.GetJobsBySalaryRange(minimum, maximum);

            if (jobs != null)
            {
                var paginatedList = PageList<JobPreviewDto>.Paginate(jobs, page, perPage);
                var res = new PaginatedListDto<JobPreviewDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of Jobs", null, res));

            }

            ModelState.AddModelError("Notfound", "No Jobs found within salary range");
            var result = Utilities.BuildResponse<object>(false, "No Jobs Found!", ModelState, null);
            return NotFound(result);
        }

        [HttpGet("Get-Job/Nature/{name}")]
        public async Task<IActionResult> GetJobByNature(string name, int page, int perPage)
        {

            var check = Enum.IsDefined(typeof(JobNature), name);
            if (!check)
            {
                ModelState.AddModelError("Notfound", "Job nature not found!");
                var res = Utilities.BuildResponse<object>(false, "Job nature does not exist!", ModelState, null);
                return NotFound(res);
            }
            Enum.TryParse(name, out JobNature nature);
            var jobs = await _jobService.GetJobsByNature(nature);
            if (jobs != null)
            {
                var paginatedList = PageList<JobPreviewDto>.Paginate(jobs, page, perPage);
                var res = new PaginatedListDto<JobPreviewDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of Jobs", null, res));

            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record found!");
                var res = Utilities.BuildResponse<object>(false, $"No results found under {name} type!", ModelState, null);
                return NotFound(res);
            }
        }




    }
}
