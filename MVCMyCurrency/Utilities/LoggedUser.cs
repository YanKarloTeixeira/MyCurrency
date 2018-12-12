using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMyCurrency.Utilities
{
    public class LoggedUser
    {
        //// Stores UserManager
        ////private readonly UserManager<ApplicationUser> _manager;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        //// Inject UserManager using dependency injection.
        //// Works only if you choose "Individual user accounts" during project creation.
        //public LoggedUser(UserManager<IdentityUser> manager)  
        //{
        //    _userManager = manager; 
        //}

        //// You can also just take part after return and use it in async methods.
        //private async Task<IdentityUser> GetCurrentUser()  
        //{  
        //    return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);  
        //}  

        //// Generic demo method.
        //public async Task DemoMethod()  
        //{  
        //    var user = await GetCurrentUser(); 
        //    string userEmail = user.Email; // Here you gets user email 
        //    string userId = user.Id;
        //}  

        private readonly UserManager<IdentityUser> _userManager;

        public LoggedUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> GetUser(string userId)
        {
            if (userId == null )
            {
                return "";
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ($"Unable to load user with ID '{userId}'.");
            }

            

            return user.Email;
        }



    }
}
