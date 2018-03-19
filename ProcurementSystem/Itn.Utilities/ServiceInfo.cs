using System.Reflection;

namespace Itn.Utilities
{

    public class ServiceInfo
    {
        public string ServiceName { get; set; }
        public string ServiceMethod { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceVersion { get; set; }

        protected ServiceInfo()
        {
            ServiceName = string.Empty;
            ServiceVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ServiceMethod = string.Empty;
            ServiceDescription = string.Empty;

        }

        public static ServiceInfo Create()
        {
            return new ServiceInfo();
        }

        public static ServiceInfo Create(string serviceName, string serviceMethod, string serviceDescription, string serviceVersion)
        {
            return new ServiceInfo
            {
                ServiceName = serviceName,
                ServiceMethod = serviceMethod,
                ServiceDescription = serviceDescription,
                ServiceVersion = serviceVersion
            };
        }




    }
}
