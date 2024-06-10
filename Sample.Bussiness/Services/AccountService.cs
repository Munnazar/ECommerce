using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Sample.Bussiness.Interfaces;
using Sample.Communication.Interfaces;
using Sample.Models.Requests.Accounts;
using Sample.Models.Responces;
using Sample.Models.Responces.Accounts;
using Sample.Models.ServiceRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Sample.Bussiness.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;
        public AccountService(UserManager<IdentityUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<ApiResponce<bool>> ForgetPassword(ForgetRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var emailSubject = "Reset your password";

                var link = "https://localhost:7242/Account/ResetPassword?code=" + WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)) + "&email=" + request.Email;

                var mailReq = new MailRequest()
                {
                    Attachments = null,
                    Subject = emailSubject,
                    Body = link,
                    ToEmail = request.Email
                };

                var flag = await _emailService.SendEmailAsync(mailReq);
                if (flag != null)
                {
                    return new ApiResponce<bool> { Message = "Email send successfully", Success = true, Result = flag };
                }
            }
            return new ApiResponce<bool> { Message = "Email not send", Success = false };
        }

        public async Task<ApiResponce<bool>> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);


            if (user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token)), request.NewPassword);

                if (result.Succeeded)
                {
                    return new ApiResponce<bool> { Message = "Password has been reset successfully", Success = true };
                }
                return new ApiResponce<bool> { Message = "Invalid Token", Success = false };
            }
            return new ApiResponce<bool> { Message = "User not found", Success = false };
        }

        public async Task<ApiResponce<AuthResponce>> SignUp(SignUpRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                var authResponse = new AuthResponce();

                if (user != null)
                {
                    authResponse.Errors = new List<string>() { "Email address already used! Try a new email address." };
                    return new ApiResponce<AuthResponce>()
                    {
                        Result = authResponse,
                        Success = false
                    };
                }

                var newUser = new IdentityUser
                {
                    Email = request.Email,
                    UserName = request.Username,
                    EmailConfirmed = false
                };

                var isCreated = await _userManager.CreateAsync(newUser, request.Password);
                if (isCreated.Succeeded)
                {
                    // Create a role for the user
                    var result = await _userManager.AddToRoleAsync(newUser, "User");

                    if (result.Succeeded)
                    {
                        // Generate email confirmation token
                        var emailConfirmation = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                        var url = "https://localhost:5251/api/Account/ConfirmEmail";

                        // Construct email confirmation link
                        var link = $"{url}?userId={newUser.Id}&token={WebUtility.UrlEncode(emailConfirmation)}";

                        // Prepare and send email
                        var emailSubject = "Confirm your email address";
                        var mailRequest = new MailRequest()
                        {
                            Subject = emailSubject,
                            Body = link,
                            ToEmail = request.Email
                        };
                        await _emailService.SendEmailAsync(mailRequest);

                        // Return success response
                        return new ApiResponce<AuthResponce> { Result = authResponse, Success = true };
                    }
                    else
                    {
                        // If role creation failed, return failure response
                        return new ApiResponce<AuthResponce>()
                        {
                            ErrorMessage = "Failed to create role for the user.",
                            Success = false
                        };
                    }
                }
                else
                {
                    authResponse.Errors = isCreated.Errors.Select(x => x.Description).ToList();
                    return new ApiResponce<AuthResponce>()
                    {
                        Result = authResponse,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                var authResponse = new AuthResponce() { Errors = new List<string> { ex.Message } };
                return new ApiResponce<AuthResponce>
                {
                    Result = authResponse,
                    Success = false
                };
            }

        }
    }
}
