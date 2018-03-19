
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Itn.OMS.Services.Manager;
using Itn.Shared.Transaction;
using Itn.Utilities;

namespace Itn.OMS.Services.Controllers
{
    //http://localhost:50083/api/wms/po/polist
    [RoutePrefix("api/wms/po")]
    public class PurchaseOrderReadController : ApiController
    {
        [HttpGet]
        [Route("polist",Name ="GetPoList")]
        [ResponseType(typeof(ServiceDataResult<DiPurchaseOrderListItem>))]
        public IHttpActionResult GetPoList( [FromUri] PurchaseOrderFilter filter)
        {
            //var svcTransInfo = ToTransactionInfo(Request);
            //var facade = GetFacade(Request.RequestUri);
            //var result = facade.GetPoList(filter, GetFacadeQueryString(Request.RequestUri), svcTransInfo);
            //return Ok(result);

            var result = TransactionManager.Create().FetchPOList(filter);
            return Ok(ServiceDataResult<DiPurchaseOrderListItem>.Create(result, "Ok"));
         
        }

        [Route("po",Name  ="GetPO")]
        [ResponseType(typeof(DiPurchaseOrder))]
        public IHttpActionResult GetPo([FromUri] PurchaseOrderFilter filter)
        {
            //var svcTransInfo = ToTransactionInfo(Request);
            //var facade = GetFacade(Request.RequestUri);
            //var result = facade.GetPoList(filter, GetFacadeQueryString(Request.RequestUri), svcTransInfo);
            //return Ok(result);

            var result = TransactionManager.Create().FetchPO(filter);
            return Ok(result);

        }
    }
}
