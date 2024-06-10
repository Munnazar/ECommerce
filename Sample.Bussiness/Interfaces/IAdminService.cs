using Microsoft.AspNetCore.Identity;
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
    public interface IAdminService
    {
        Task<ApiResponce<List<IdentityRole>>> GetRoles();
        Task<ApiResponce<bool>> AssignRolesToUser(string userName, string roles);
        Task<ApiResponce<AdminResponce>> CreateRole(RoleRequest request);
    }
}
