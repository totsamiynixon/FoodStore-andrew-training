using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Shop.Web.Filters  
{
    internal class SimpleResourceFilter : Attribute, IResourceFilter
    {
        private readonly IConfiguration _configuration;
        readonly bool isShowcase;

        public SimpleResourceFilter(IConfiguration configuration)
        {
            _configuration = configuration;
            isShowcase = _configuration.GetValue<bool>("Showcase");
        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (isShowcase == true)
                context.Result = new ContentResult { Content = "Ресурс не найден" };
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            return;
        }

    }
}
