using System.Collections.Generic;
using Itn.Shared.Transaction;

namespace OMSWeb.Provider
{
    public interface  ITransactionDataProvider
    {
        List<DiPurchaseOrderListItem> FetchPOList(PurchaseOrderFilter poFilter);
        void PostPOModel(DiPurchaseOrder diPurchaseOrderModel);
        DiPurchaseOrder FetchPurchaseOrder(string ponbr);
        void PostFulfillPO(DiPurchaseOrder diPurchaseOrderModel);
    }
}
