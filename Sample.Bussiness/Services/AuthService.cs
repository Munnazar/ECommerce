using Microsoft.AspNetCore.Identity;
using Sample.Bussiness.Interfaces;
using Sample.Models.Requests.Accounts;
using Sample.Models.Responces;
using Sample.Models.Responces.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Bussiness.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManger = userManager;
            _tokenService = tokenService;
        }
        public async Task<ApiResponce<AuthResponce>> SignIn(SignInRequest request)
        {
            var userExist = await _userManger.FindByEmailAsync(request.Email);

            var authResponse = new AuthResponce();
            if (userExist == null)
            {
                authResponse.Errors = new List<string>() { "User not found" };
                return new ApiResponce<AuthResponce>()
                {
                    Result = authResponse,
                    Success = false
                };
            }


            if (!userExist.EmailConfirmed)
            {
                authResponse.Errors = new List<string>() { "Email not confirmed" };
                return new ApiResponce<AuthResponce>()
                {
                    Result = authResponse,
                    Success = false
                };
            }


            var isValid = await _userManger.CheckPasswordAsync(userExist, request.Password);

            if (!isValid)
            {
                authResponse.Errors = new List<string>() { "Invalid login request" };
                return new ApiResponce<AuthResponce>()
                {
                    Result = authResponse,
                    Success = false
                };
            }

            var jwtToken = await _tokenService.GenerateJwtToken(userExist);
            return new ApiResponce<AuthResponce>
            {
                Success = true,
                Result = jwtToken,
                Message = "Request Successful"
            };
        }
    }
}
