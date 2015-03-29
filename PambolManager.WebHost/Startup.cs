using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PambolManager.API.Config;

[assembly: OwinStartup (typeof(PambolManager.WebHost.Startup))]
namespace PambolManager.WebHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var config = GlobalConfiguration.Configuration;
            HttpConfiguration config = new HttpConfiguration();

            RouteConfig.RegisterRoutes(config);
            WebAPIConfig.Configure(config);
            AutofacWebAPI.Initialize(config);

            app.UseWebApi(config);
        }
    }
}