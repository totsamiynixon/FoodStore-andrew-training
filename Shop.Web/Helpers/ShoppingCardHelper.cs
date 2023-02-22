using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data;
using Shop.Data.Models;
using System;
using System.Security.Claims;

namespace Shop.Web.Helpers
{
    public class ShoppingCardHelper
    {
        public static ShoppingCartService GetCart(IServiceProvider services)
        {
            var user = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.User;
            var context = services.GetService<ApplicationDbContext>();
            return new ShoppingCartService(context, user.FindFirstValue(ClaimTypes.NameIdentifier));

            //ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            //var context = services.GetService<ApplicationDbContext>();
            //string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            //session.SetString("CartId", cartId);
            //return new ShoppingCartService(context);
        }
    }
}
