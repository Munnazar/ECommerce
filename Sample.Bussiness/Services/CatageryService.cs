using Sample.Bussiness.Interfaces;
using Sample.Domain.Interfaces;
using Sample.Domain.Models;
using Sample.Models.Requests.ContactUs;
using Sample.Models.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Bussiness.Services
{
    public class CatageryService : ICatageryService
    {
        private readonly ICatageryRepository _catageryRepository;
        public CatageryService(ICatageryRepository catageryRepository)
        {
            _catageryRepository = catageryRepository;
        }
        public async Task<ApiResponce<bool>> AddCatagery(CatageryRequest request)
        {
            var result = await _catageryRepository.AddCatagery(request);
            if(result != null)
            {
                return new ApiResponce<bool> { Success = true, Result = result, Message = "Data Added Successfully" };
            }
            return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "No Record Added" };
        }

        public async Task<ApiResponce<bool>> DeleteCatagery(int id)
        {
            var existingCategory = await _catageryRepository.GetCatageryById(id);
            if (existingCategory.Count <= 0)
            {
                return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "Category not found." };
            }
            var result = await _catageryRepository.DeleteCatagery(id);
            if (result != null)
            {
                return new ApiResponce<bool> { Result = result, Success = true, Message = "Delete Record" };
            }
            return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "No Record Found" };
        }

        public async Task<ApiResponce<List<Catagery>>> GetCatageries()
        {
            var result = await _catageryRepository.GetCatageries();
            if (result != null)
            {
                return new ApiResponce<List<Catagery>> { Result = result, Success = true, Message = "Get Data Successfully" };
            }
            return new ApiResponce<List<Catagery>> { Result = null, Success = false, ErrorMessage = "NO Record Found" };
        }

        public async Task<ApiResponce<List<Catagery>>> GetCatageryById(int id)
        {
            var result = await _catageryRepository.GetCatageryById(id);
            if (result != null && result.Count() > 0)
            {
                return new ApiResponce<List<Catagery>> { Result = result, Success = true, Message = "Get Data Successfully" };
            }
            return new ApiResponce<List<Catagery>> { Result = null, Success = false, ErrorMessage = "NO Record Found Invalid Id" };
        }

        public async Task<ApiResponce<bool>> UpDateCatagery(int id, CatageryRequest request)
        {
            var existingCategory = await _catageryRepository.GetCatageryById(id);
            if (existingCategory.Count <= 0)
            {
                return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "Category not found." };
            }
            var result = await _catageryRepository.UpDateCatagery(id, request);
            if (result != null)
            {
                return new ApiResponce<bool> { Result = result, Success = true, Message = "Record Successfully Updated" };
            }
            return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "Invalid Record" };
        }
    }
}
