namespace Itn.Utilities
{
    public class ServicePostResult : BaseServiceResult
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public static ServicePostResult Create(string id, string location, ServiceResult serviceResult)
        {
            return new ServicePostResult { Id = id, Location = location, ServiceResultInfo = serviceResult };
        }
    }
}
