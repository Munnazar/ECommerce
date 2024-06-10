using Microsoft.AspNetCore.Identity;
using Sample.Models.Requests.Accounts;
using Sample.Models.Responces.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Bussiness.Interfaces
{
    public interface ITokenService
    {
        Task<AuthResponce> GenerateJwtToken(IdentityUser user);
        //Task<AuthResponce> VerifyAndGenerateToken(TokenRequest tokenRequest);
    }
}
