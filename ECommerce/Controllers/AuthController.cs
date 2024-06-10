using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Bussiness.Interfaces;
using Sample.Models.Requests.Accounts;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var result = await _authService.SignIn(request);
            return Ok(result);
        }
    }
}
