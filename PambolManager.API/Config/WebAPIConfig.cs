using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PambolManager.API.Config
{
    public class WebAPIConfig
    {
        public static void Configure (HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}
