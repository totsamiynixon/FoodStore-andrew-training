using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shop.Data.Models;
using System;

namespace Shop.Web.Services
{
    public class ShowcaseService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly bool _IsShowcase;

        public ShowcaseService(IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServiceProvider _services,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool ShowcaseModeEnabled
        {
            get
            {
                var _IsShowcase = _configuration.GetValue<bool>("Showcase");
                var user = _httpContextAccessor.HttpContext.User;

                if ( (user.IsInRole("Admin") & _IsShowcase == true) || (_signInManager.IsSignedIn(user) & _IsShowcase == false) 
                    || (!_signInManager.IsSignedIn(user) & _IsShowcase == false))
                {
                  return true;
                }
                return false;
            }
        }
    }
}
