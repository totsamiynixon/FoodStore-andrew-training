using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shop.Data.Models;
using System.Security.Claims;
using System;


namespace Shop.Web.Services
{
   
    public class ShowcaseService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly bool IsShowcase;

        public ShowcaseService(IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServiceProvider _services,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            IsShowcase = _configuration.GetValue<bool>("Showcase");
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Value
        {
            get
            {
                var user = _httpContextAccessor.HttpContext.User;
                if ( (user.IsInRole("Admin") & IsShowcase == true) || (_signInManager.IsSignedIn(user) & IsShowcase == false) )
                    {
                  return true;
                }
                return false;
            }
        }
    }
}
