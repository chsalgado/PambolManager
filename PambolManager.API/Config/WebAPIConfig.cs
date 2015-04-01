using PambolManager.API.Model.RequestCommands;
using System.Web.Http;

namespace PambolManager.API.Config
{
    public class WebAPIConfig
    {
        public static void Configure (HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            
            // Register ParameterBinding rule for IRequestCommand
            config.ParameterBindingRules.Insert(0,
                descriptor => typeof(IRequestCommand)
                .IsAssignableFrom(descriptor.ParameterType) ? new FromUriAttribute().GetBinding(descriptor) : null);
        }
    }
}
