

using System.Collections.Generic;
using Itn.Shared.Transaction;

namespace ItnOMS.Web.Models
{
    public class POHeaderVM
    {
        public string CustomerNbr { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<POLineVM> Lines { get; set; }

        public string PONbr { get; set; }
        public string DeliveryDate { get; set; }

        

        private POHeaderVM()
        {
            Lines = new List<POLineVM>();
        }

        public static POHeaderVM Create()
        {
            return  new POHeaderVM();
        }

        public static DiPurchaseOrder ToDiPurchaseOrderModel(POHeaderVM poHeaderVm)
        {
            var diPurchaseOrder = DiPurchaseOrder.Create();
            diPurchaseOrder.CustomerNbr = poHeaderVm.CustomerNbr;
            diPurchaseOrder.CreatedBy = "rv";
            diPurchaseOrder.PONbr = poHeaderVm.PONbr;
            diPurchaseOrder.DeliveryDate = poHeaderVm.DeliveryDate;
            diPurchaseOrder.CustomerEmail = poHeaderVm.CustomerEmail;
            foreach (var lineVM in poHeaderVm.Lines)
            {
                var diPoLine = DiPurchaseOrderLine.Create(lineVM.ItemNbr, lineVM.Qty, "0");
                diPurchaseOrder.Lines.Add(diPoLine);
            }
                
            return diPurchaseOrder;
        }
    }
}