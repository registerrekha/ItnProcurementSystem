using Itn.Shared.Transaction;
using System.Web.Http;
using System.Web.Http.Description;
using Itn.OMS.Services.Manager;
using Itn.Utilities;

namespace Itn.OMS.Services.Controllers
{
    [RoutePrefix("api/wms/po")]
    public class PurchaseOrderWriteController : ApiController
    {
        [Route("pocreate",Name = "PostPoCreate")]
      [ResponseType(typeof(ServicePostResult))]
        public IHttpActionResult PostPoCreate([FromBody] DiPurchaseOrder model)
        {
            TransactionManager.Create().SavePurchaseOrder(model);
            TransactionManager.Create().NotifyCustomer(model,"create");
            return Ok();
        }

        [Route("poupdate", Name = "PostPoUpdate")]
        [ResponseType(typeof(ServicePostResult))]
        public IHttpActionResult PostPoUpdate([FromBody] DiPurchaseOrder model)
        {
            TransactionManager.Create().UpdatePOLines(model);
            return Ok();
        }
    }
}
