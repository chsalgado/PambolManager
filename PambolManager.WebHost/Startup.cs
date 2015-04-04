using Microsoft.Owin;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PambolManager.API.Config;
using PambolManager.WebHost.Providers;
using System;
using System.Web.Http;

[assembly: OwinStartup (typeof(PambolManager.API.WebHost.Startup))]
namespace PambolManager.API.WebHost
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        
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
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

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

            //Configure Google External Login
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "21407591687-g6083g4toudfhv82ikr7o9fl83u955tq.apps.googleusercontent.com",
                ClientSecret = "5L6p0RJMZ0MYy1zWHmX9Cw4o",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);

        }
    }
}