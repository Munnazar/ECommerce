using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Bussiness.Interfaces;
using Sample.Models.Requests.Accounts;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _adminService.GetRoles();
            return Ok(result);
        }

        [HttpPost("Assignrole")]
        public async Task<IActionResult> AssignRolesToUser(string userName, string roles)
        {
            var result = await _adminService.AssignRolesToUser(userName, roles);
            return Ok(result);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(RoleRequest request)
        {
            var result = await _adminService.CreateRole(request);
            return Ok(result);
        }

    }
}
