using System.Web.Http;
using System.Web.Http.Description;

using Itn.MasterDataServices.Manager;
using Itn.Shared.MasterData;
using Itn.Utilities;

namespace Itn.MasterServices.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        [Route("listcustomers", Name = "GetCustomers")]
        [ResponseType(typeof(ServiceDataResult<DiCustomer>))]
        public IHttpActionResult GetItems()
        {
            var result = MasterDataManager.Create().GetCustomers();
            return Ok(ServiceDataResult<DiCustomer>.Create(result, "Ok"));
           
        }
    }
}
