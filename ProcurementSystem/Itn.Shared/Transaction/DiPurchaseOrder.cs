using System.Collections.Generic;

namespace Itn.Shared.Transaction
{
    public class DiPurchaseOrder
    {
        public string PONbr { get; set; }
        public string CustomerNbr { get; set; }

       
        public string DeliveryDate { get; set; }
        public string CreatedDateTime { get; set; }

        public string CreatedBy { get; set; }

        public string Status { get; set; }
        public List<DiPurchaseOrderLine> Lines { get; set; }
        public string CustomerEmail { get; set; }

        private DiPurchaseOrder()
        {
            Lines = new List<DiPurchaseOrderLine>();
        }

        public static DiPurchaseOrder Create()
        {
            return new DiPurchaseOrder();
        }
    }
}
