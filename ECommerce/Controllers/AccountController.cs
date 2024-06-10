using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sample.Bussiness.Interfaces;
using Sample.Models.Requests.Accounts;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(IAccountService accountService, UserManager<IdentityUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            var result = await _accountService.SignUp(request);
            return new OkObjectResult(result);
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return new OkObjectResult(result);
                }
                else
                {
                    return new BadRequestObjectResult("Email confirmation failed.");
                }
            }
            else
            {
                return new BadRequestObjectResult("User not found.");
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetRequest request)
        {
            var result = await _accountService.ForgetPassword(request);
            return new OkObjectResult(result);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _accountService.ResetPassword(request);
            return new OkObjectResult(result);
        }
    }
}
