namespace Itn.Shared.Transaction
{
    public class DiPurchaseOrderListItem
    {
        public string PoNbr { get; set; }
         public string CustomerName { get; set; }
        public string CustomerNbr   { get; set;  }
     
        public string CreateDateTime { get; set; }
        public string DeliveryDate { get; set; }

        public string CompanyId { get; set; }
        public string Status { get; set; }
       
        public string CreatedBy { get; set; }

        public static DiPurchaseOrderListItem Create(string poNbr, string companyId, string customerName,  string deliveryDate, string status, string customerNbr)
        {
            return new DiPurchaseOrderListItem
            {
                PoNbr = poNbr,
                CompanyId = companyId,
                CustomerName = customerName,
                CustomerNbr = customerNbr,
              
                DeliveryDate = deliveryDate,
                Status = status
                
            };
        }
    }
}
