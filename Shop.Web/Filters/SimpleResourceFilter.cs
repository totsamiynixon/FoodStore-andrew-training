using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Shop.Web.Filters  
{
    internal class SimpleResourceFilter : Attribute, IResourceFilter
    {
        private readonly IConfiguration _configuration;
        private readonly bool _isShowcase;

        public SimpleResourceFilter(IConfiguration configuration)
        {
            _configuration = configuration;
            _isShowcase = _configuration.GetValue<bool>("Showcase");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (_isShowcase == true)
                context.Result = new ContentResult { Content = "Ресурс не найден" };
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            return;
        }

    }
}
