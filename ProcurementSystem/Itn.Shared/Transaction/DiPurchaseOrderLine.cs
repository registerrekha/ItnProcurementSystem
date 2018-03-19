namespace Itn.Shared.Transaction
{
    public class DiPurchaseOrderLine
    {
        public int LineNbr { get; set; }
        public string ItemNbr { get; set; }
        public string Qty { get; set; }
        public string QtyFulfilled { get; set; }

        public static DiPurchaseOrderLine Create(string itemNbr, string qty,string qtyFulfilled)
        {
            return new DiPurchaseOrderLine
            {
                ItemNbr = itemNbr,
                Qty = qty,
                QtyFulfilled =  qtyFulfilled
            };
        }
    }
}
