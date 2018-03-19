using System;
using System.Collections.Generic;

namespace Itn.OMS.Services.Models
{
    public class PurchaseOrder
    {
        public string PONbr { get; set; }
        public string CustomerNbr { get; set; }

       
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDateTime { get; set;  }

        public string CreatedBy { get; set; }
      
        public POStatusType Status { get; set; }
        public List<PurchaseOrderLine> Lines { get; set; }
        public string CustomerEmail { get; set; }


        private PurchaseOrder()
        {
            Lines = new List<PurchaseOrderLine>();
            Status = POStatusType.None;
        }
        public static PurchaseOrder Create()
        {
            return new PurchaseOrder();
        }
    }
}
