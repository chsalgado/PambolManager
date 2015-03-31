using PambolManager.API.Routing;
using System.Web.Http;

namespace PambolManager.API.Config
{
    public class RouteConfig
    {
        public static void RegisterRoutes(HttpConfiguration config)
        {
            var routes = config.Routes;

            routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = new GuidRouteConstraint() });
        }
    }
}
