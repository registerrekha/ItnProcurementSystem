using Itn.OMS.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itn.Shared.Transaction;

namespace Itn.OMS.Services.DAL.Contracts
{
    public interface ITransactionProvider
    {
        List<SimplePurchaseOrder> FetchPurchaseOrdersList(PurchaseOrderFilter filter);

         
        void SavePurchaseOrder(PurchaseOrder purchaseOrderModel);
        void UpdateInventory(PurchaseOrder purchaseOrderModel);
        PurchaseOrder FetchPO(string filterPoNbr);
        void UpdatePODetail(PurchaseOrder purchaseOrder);
        void UpdatePoStatus(string poNbr, POStatusType status);
    }
}
