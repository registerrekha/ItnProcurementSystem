
using System.Collections.Generic;
using Itn.Shared.Transaction;
using Itn.Utilities;

namespace OMSWeb.Provider
{
    public class TransactionRestDataProvider :  BaseRestProvider, ITransactionDataProvider
    {
        public List<DiPurchaseOrderListItem> FetchPOList(PurchaseOrderFilter poFilter)
        {
            var queryString = string.Empty;
            if (poFilter != null)
            {
                queryString = new[] {poFilter.ToNameValueCollection().ToQueryString()}.ToQueryString();
            }

            var route = string.Format("{0}{1}",
                AppConfig.GetConfigVal("Itn.Transaction.PO.Get.List.Route"), queryString);
            return Get<ServiceDataResult<DiPurchaseOrderListItem>>(route).Data;
        }

        public void PostPOModel(DiPurchaseOrder diPurchaseOrderModel)
        {
            var route = AppConfig.GetConfigVal("Itn.Transaction.PO.Post.Create.Route");
            Post(route, diPurchaseOrderModel);
        }

        public DiPurchaseOrder FetchPurchaseOrder(string ponbr)
        {
            var poQueryString = new PurchaseOrderFilter() {PoNbr = ponbr}.ToNameValueCollection().ToQueryString();
            var route = string.Format("{0}{1}",
                AppConfig.GetConfigVal("Itn.Transaction.PO.Get.Route"), poQueryString);
            return Get<DiPurchaseOrder>(route);
        }

        public void PostFulfillPO(DiPurchaseOrder diPurchaseOrderModel)
        {
            var route = AppConfig.GetConfigVal("Itn.Transaction.PO.Post.Fulfill.Route");
            Post(route, diPurchaseOrderModel);
        }


        internal override string CoreBaseUri()
        {
            return AppConfig.GetConfigVal("Provider.Itn.Services.EndPoint.Uri.Base");
        }
       
    }
}
