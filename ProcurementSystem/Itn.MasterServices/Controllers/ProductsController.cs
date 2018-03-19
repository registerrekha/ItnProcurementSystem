using Itn.MasterDataServices.Manager;
using Itn.Shared.MasterData;
using Itn.Utilities;
using System.Web.Http;
using System.Web.Http.Description;

namespace Itn.MasterServices.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        [Route("items", Name = "GetItems")]
        [ResponseType(typeof(ServiceDataResult<DiItem>))]
        public IHttpActionResult GetItems()
        { 
            var result = MasterDataManager.Create().GetItems();
            return Ok(ServiceDataResult<DiItem>.Create(result, "Ok"));
        }
    }
}
