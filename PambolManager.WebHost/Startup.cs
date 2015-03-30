using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PambolManager.API.Config;
using PambolManager.WebHost.Providers;
using System;
using System.Web.Http;

[assembly: OwinStartup (typeof(PambolManager.WebHost.Startup))]
namespace PambolManager.WebHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var config = GlobalConfiguration.Configuration;
            HttpConfiguration config = new HttpConfiguration();
            
            ConfigureOAuth(app);

            RouteConfig.RegisterRoutes(config);
            WebAPIConfig.Configure(config);
            AutofacWebAPI.Initialize(config);

            //Allow CORS (request coming form any origin, not only our front-end)
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            //Token generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}