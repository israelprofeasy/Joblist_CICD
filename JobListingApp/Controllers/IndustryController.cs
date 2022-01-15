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
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryService _industry;

        public IndustryController(IIndustryService idustry)
        {
            _industry = idustry;
        }
       // [Authorize(Roles = "Admin")]
        [HttpPost("add-industry")]
        public async Task<IActionResult> Add(IndustryDto model)
        {
            var res = await _industry.AddIndustry(model);
            if (res != null)
            {
                return Ok(Utilities.BuildResponse<IndustryReturnedDto>(true, "Industry successfully added!", null, res));
            }
            ModelState.AddModelError("Invalid", "Name already exist");
            return BadRequest(Utilities.BuildResponse<object>(false, "Name already exist", ModelState, null));

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-industry")]
        public async Task<IActionResult> DeleteIndustry(string id)
        {
            var res = await _industry.RemoveIndustry(id);
            if (res)
            {
                return Ok(Utilities.BuildResponse<string>(true, "Industry successfully deleted!", null, ""));
            }
            ModelState.AddModelError("Invalid", "Industry Id does not exist!");
            return BadRequest(Utilities.BuildResponse<object>(false, "Enter a valid Industry Id", ModelState, null));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("edit-industry")]
        public async Task<IActionResult> EditIndustry(string id, IndustryDto model)
        {
            var res = await _industry.UpdateIndustry(id, model);
            if (res)
            {
                return Ok(Utilities.BuildResponse<string>(true, "Update Successful", null, model.Name));
            }
            ModelState.AddModelError("Invalid", "Id does not exit");
            return BadRequest(Utilities.BuildResponse(false, "Enter a valid id", ModelState, ""));
        }
        [HttpGet("get-all-industry")]
        public async Task<IActionResult> GetAllIndustry(int page, int perPage)
        {
            var industry = await _industry.GetAllIndustry();
            if (industry != null)
            {
                var paginatedList = PageList<IndustryReturnedDto>.Paginate(industry, page, perPage);
                var res = new PaginatedListDto<IndustryReturnedDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of industry", null, res));

            }
            else
            {
                ModelState.AddModelError("Notfound", "There was no record for category found!");
                var res = Utilities.BuildResponse<List<IndustryReturnedDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("get-industry-by-id")]
        public async Task<IActionResult> GetIndustryId(string id)
        {
            var industry = await _industry.GetIndustryById(id);
            if (industry != null)
            {
                return Ok(Utilities.BuildResponse(true, "Industry Details", null, industry));

            }
            else
            {
                ModelState.AddModelError("Notfound", "Id does not exist!");
                var res = Utilities.BuildResponse<List<IndustryReturnedDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("get-industry-by-name")]
        public async Task<IActionResult> GetIndustryByName(string name)
        {
            var industry = await _industry.GetIndustryByName(name);
            if (industry != null)
            {
                return Ok(Utilities.BuildResponse(true, "Industry Details", null, industry));

            }
            else
            {
                ModelState.AddModelError("Notfound", "Name does not exist!");
                var res = Utilities.BuildResponse<List<IndustryReturnedDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
        [HttpGet("get-categories-by-name")]
        public async Task<IActionResult> GetIndustries(string name, int page, int perPage)
        {
            var industry = await _industry.GetIndustries(name);
            if (industry != null)
            {
                var paginatedList = PageList<IndustryReturnedDto>.Paginate(industry, page, perPage);
                var res = new PaginatedListDto<IndustryReturnedDto> { MetaData = paginatedList.MetaData, Data = paginatedList.Data };
                return Ok(Utilities.BuildResponse(true, "List of industries", null, res));


            }
            else
            {
                ModelState.AddModelError("Notfound", "Id does not exist!");
                var res = Utilities.BuildResponse<List<IndustryReturnedDto>>(false, "No results found!", ModelState, null);
                return NotFound(res);
            }
        }
    }
}
