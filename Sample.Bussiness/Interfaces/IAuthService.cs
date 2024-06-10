using Sample.Models.Requests.Accounts;
using Sample.Models.Responces.Accounts;
using Sample.Models.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Bussiness.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponce<AuthResponce>> SignIn(SignInRequest request);
    }
}
