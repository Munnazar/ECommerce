using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Bussiness.Interfaces;
using Sample.Models.Requests.ContactUs;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Open")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CatageryController : ControllerBase
    {
        private readonly ICatageryService _catageryService;
        public CatageryController(ICatageryService catageryService)
        {
            _catageryService = catageryService;
        }

        [HttpPost]
        [Route("AddCatagery")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCatagery(CatageryRequest request)
        {
            var result = await _catageryService.AddCatagery(request);
            return Ok(result);
        }

        
        [HttpGet]
        [Route("GetCatageries")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetCatageries()
        {
            var result = await _catageryService.GetCatageries();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCatagery/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetCatageryById(int id)
        {
            var result = await _catageryService.GetCatageryById(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateCatagery/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCatagery(int id,  CatageryRequest request)
        {
            var result = await _catageryService.UpDateCatagery(id, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteCatagery/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCatagery(int id)
        {
            var result = await _catageryService.DeleteCatagery(id);
            return Ok(result);
        }

    }
}
