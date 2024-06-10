using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sample.Bussiness.Interfaces;
using Sample.Data.Context;
using Sample.Domain.Models;
using Sample.Models.ConfigModels;
using Sample.Models.Requests.Accounts;
using Sample.Models.Responces.Accounts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utility = Sample.Common.Utilities;

namespace Sample.Bussiness.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TokenService(JwtConfig jwtConfig,
            TokenValidationParameters tokenValidationParams,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager)
        {
            _jwtConfig = jwtConfig;
            _tokenValidationParams = tokenValidationParams;
            _userManager = userManager;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }
        public async Task<AuthResponce> GenerateJwtToken(IdentityUser user)

        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.FindByNameAsync(userRoles.FirstOrDefault());
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Username", user.UserName),
                    new Claim(ClaimTypes.Role, userRoles.FirstOrDefault()),
                    //new Claim(ClaimTypes.Role, role?.Name ?? ""),
                    new Claim("RoleId", role?.Id ?? ""),

                }),


                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtConfig.ExpirationTime)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };


            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(Convert.ToInt32(_jwtConfig.RefreshTokenExpiration)),
                Token = Utility.Utility.RandomString(35) + Guid.NewGuid()

            };

            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            return new AuthResponce()
            {
                Token = jwtToken,
                Email = user.Email,
                UserId = user.Id,
                UserName = user.UserName,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }


        //public async Task<AuthResponce> VerifyAndGenerateToken(TokenRequest tokenRequest)

        //{
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();

        //    try
        //    {
        //        // Validation 1 - Validation JWT token format
        //        var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out var validatedToken);

        //        // Validation 2 - Validate encryption alg
        //        if (validatedToken is JwtSecurityToken jwtSecurityToken)
        //        {
        //            var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        //            if (!result)
        //            {
        //                return null;
        //            }
        //        }

        //        // Validation 3 - validate expiry date
        //        var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
        //        var expiryDate = Utility.Utility.UnixTimeStampToDateTime(utcExpiryDate);
        //        if (expiryDate > DateTime.UtcNow)
        //        {
        //            return new AuthResponce()
        //            {
        //                Success = false,
        //                Errors = new List<string>() { "Token has not yet expired" }
        //            };
        //        }

        //        // validation 4 - validate existence of the token
        //        var storedToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

        //        if (storedToken == null)
        //        {
        //            return new AuthResponce()
        //            {
        //                Success = false,
        //                Errors = new List<string>() { "Token does not exist" }
        //            };
        //        }

        //        // Validation 5 - validate if used
        //        if (storedToken.IsUsed)
        //        {
        //            return new AuthResponce()
        //            {
        //                Success = false,
        //                Errors = new List<string>() { "Token has been used" }
        //            };
        //        }

        //        // Validation 6 - validate if revoked
        //        if (storedToken.IsRevoked)
        //        {
        //            return new AuthResponce()
        //            {
        //                Success = false,
        //                Errors = new List<string>() { "Token has been revoked" }
        //            };
        //        }

        //        // Validation 7 - validate the id
        //        var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        //        if (storedToken.JwtId != jti)
        //        {
        //            return new AuthResponce()
        //            {
        //                Success = false,
        //                Errors = new List<string>() { "Token doesn't match" }
        //            };
        //        }

        //        // update current token 
        //        storedToken.IsUsed = true;
        //        _dbContext.RefreshTokens.Update(storedToken);
        //        await _dbContext.SaveChangesAsync();

        //        // Generate a new token
        //        var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
        //        return await GenerateJwtToken(dbUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("Lifetime validation failed. The token is expired."))
        //        {
        //            return new AuthResponce()
        //            {
        //                Success = false,
        //                Errors = new List<string>() { "Token has expired please re-login" }
        //            };
        //        }
        //        else
        //        {
        //            return new AuthResponce()
        //            {
        //                Success = false,
        //                Errors = new List<string>() { "Something went wrong." }
        //            };
        //        }
        //    }
        //}
    }
}
