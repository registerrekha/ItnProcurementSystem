using Itn.OMS.Services.Registry;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Itn.OMS.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ServiceBootstrapper.Bootstrap(new BaseServicesRegistry());

            System.Web.Http.GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("format", "json", new MediaTypeHeaderValue("application/json")));
            System.Web.Http.GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("format", "xml", new MediaTypeHeaderValue("application/xml")));
        }
    }
}
