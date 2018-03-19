using System;

namespace Itn.OMS.Services.Models
{
    public class SimplePurchaseOrder
    {
        public string PoNbr { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNbr   { get; set;  }
      
        public DateTime CreateDateTime { get; set; }
        public DateTime DeliveryDate { get; set; }

        public string CompanyId { get; set; }
        public POStatusType Status { get; set; }
       
        public string CreatedBy { get; set; }
        public static SimplePurchaseOrder Create()
        {
            return new SimplePurchaseOrder();
        }
    }
}
