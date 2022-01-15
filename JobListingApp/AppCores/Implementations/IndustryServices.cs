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
    public class IndustryServices : IIndustryService
    {
        private readonly IIndustryRepository _industryRepo;
        private readonly IMapper _mapper;

        public IndustryServices(IIndustryRepository industryRepository, IMapper mapper)
        {
            _industryRepo = industryRepository;
            _mapper = mapper;
        }
        public async Task<IndustryReturnedDto> AddIndustry(IndustryDto industry)
        {
            var check = await _industryRepo.GetIndustryByName(industry.Name);
            //var res = false;
            if (check == null)
            {

                var newIndustry = _mapper.Map<Industry>(industry);
                try
                {
                    var addIndustry = await _industryRepo.Add(newIndustry);
                    if (addIndustry.Equals(true))
                    {
                        return _mapper.Map<IndustryReturnedDto>(newIndustry);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


            }
            return null;
        }

        public async Task<List<IndustryReturnedDto>> GetAllIndustry()
        {
            var listofIndustry = new List<IndustryReturnedDto>();
            try
            {
                var res = await _industryRepo.GetAll();
                foreach (var item in res)
                {
                    listofIndustry.Add(_mapper.Map<IndustryReturnedDto>(item));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listofIndustry;
        }

        public async Task<List<IndustryReturnedDto>> GetIndustries(string name)
        {
            var listofIndustry = new List<IndustryReturnedDto>();
            try
            {
                var res = await _industryRepo.GetIndustries(name);
                foreach (var item in res)
                {
                    listofIndustry.Add(_mapper.Map<IndustryReturnedDto>(item));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listofIndustry;
        }

        public async Task<IndustryReturnedDto> GetIndustryById(string id)
        {
            try
            {
                var res = await _industryRepo.GetIndustryById(id);
                if (res != null)
                {
                    return _mapper.Map<IndustryReturnedDto>(res);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public async Task<IndustryReturnedDto> GetIndustryByName(string name)
        {
            try
            {
                var res = await _industryRepo.GetIndustryByName(name);
                if (res != null)
                {
                    return _mapper.Map<IndustryReturnedDto>(res);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public async Task<bool> RemoveIndustry(string id)
        {
            var industry = await _industryRepo.GetIndustryById(id);
            if (industry != null)
            {
                try
                {
                    var res = await _industryRepo.Delete(industry);
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

        public async Task<bool> UpdateIndustry(string id, IndustryDto industry)
        {
            var result = await _industryRepo.GetIndustryById(id);
            var success = false;
            if (result != null)
            {
                var updatedindustry = _mapper.Map<Industry>(industry);
                updatedindustry.Id = id;
                try
                {
                    var res = await _industryRepo.Update(updatedindustry);
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
