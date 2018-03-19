using System;

namespace Itn.OMS.Services.Models
{
    public class PurchaseOrderLine
    {
        public string ItemNbr { get; set; }
        public int QtyOrdered { get; set; }
        public int QtyFulfilled { get; set; }

        public static PurchaseOrderLine Create()
        {
             return new PurchaseOrderLine();
        }

        public static PurchaseOrderLine Create(string lineItemNbr, int lineQty,int lineQtyfulfilled)
        {
            return new PurchaseOrderLine()
            {
                ItemNbr = lineItemNbr,
                QtyOrdered = lineQty,
                QtyFulfilled = lineQtyfulfilled
            };
        }
    }
}
