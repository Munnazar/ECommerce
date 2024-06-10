using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class AdminService : IAdminService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;

        }

        public async Task<ApiResponce<bool>> AssignRolesToUser(string userName, string roles)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (userName == null)
            {
                return new ApiResponce<bool> { Message = "User not found", Success = false };
            }
            var result = await _userManager.AddToRoleAsync(user, roles);
            return new ApiResponce<bool> { Message = "Roles have been assigned to user", Success = true, Result = true };
            //: new ApiResponce<bool> { ErrorMessage = result.Errors.FirstOrDefault().Description, Success = false };
        }

        public async Task<ApiResponce<AdminResponce>> CreateRole(RoleRequest request)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = request.Role
            };
            IdentityResult result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return new ApiResponce<AdminResponce> { Message = "Role Created Successfully", Success = true };
            }
            return new ApiResponce<AdminResponce> { ErrorMessage = "Role Already Exists", Success = false };
        }

        public async Task<ApiResponce<List<IdentityRole>>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if(roles != null && roles.Count > 0)
            {
                return new ApiResponce<List<IdentityRole>> { Message = "Success", Success = true, Result = roles };
            }
            else
            {
                return new ApiResponce<List<IdentityRole>> { Message = "No role found", Success = false };
            }
        }
    }
}
