using System.Collections.Generic;

namespace Itn.Utilities
{
    public class AcceptedServiceDataResult
    {
        public string QueueToken { get; set; }
        public string Status { get; set; }
        public string Eta { get; set; }
        public string QueueLocation { get; set; }
        public List<ResourceLink> _links { get; set; }
        public string Message { get; set; }

        public AcceptedServiceDataResult()
        {
            _links = new List<ResourceLink>();
        }

        //changed enum to string, may cause issues?
        public static AcceptedServiceDataResult Create(string queueToken, string status, string eta,
            string queueLocation, string message)
        {
            return new AcceptedServiceDataResult
            {
                QueueToken = queueToken,
                Status = status.ToString(),
                Eta = eta,
                QueueLocation = queueLocation,
                Message = message
            };
        }
        //same comment as above
        public static AcceptedServiceDataResult Create(string queueToken, string status, string eta,
            string queueLocation, string message, List<ResourceLink> links)
        {
            return new AcceptedServiceDataResult
            {
                QueueToken = queueToken,
                Status = status.ToString(),
                Eta = eta,
                QueueLocation = queueLocation,
                Message = message,
                _links = links
            };
        }
    }
}
