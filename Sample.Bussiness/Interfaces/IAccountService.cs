using Sample.Models.Requests.Accounts;
using Sample.Models.Responces;
using Sample.Models.Responces.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Bussiness.Interfaces
{
    public interface IAccountService
    {
        Task<ApiResponce<AuthResponce>> SignUp(SignUpRequest request);
        Task<ApiResponce<bool>> ForgetPassword(ForgetRequest request);
        Task<ApiResponce<bool>> ResetPassword(ResetPasswordRequest request);

    }
}
