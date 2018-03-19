using System;
using Itn.OMS.Services.DAL.Contracts;
using StructureMap;
using System.Collections.Generic;
using Itn.Shared.Transaction;
using Itn.OMS.Services.Models;
using Itn.Shared.Notification;
using Itn.Utilities;

namespace Itn.OMS.Services.Manager
{
    public class TransactionManager
    {
        public ITransactionProvider DataProvider { get; set; }
        public IMessageProvider NotficationDataProvider { get; set; }

        public TransactionManager() : this(ObjectFactory.GetInstance<ITransactionProvider>(), ObjectFactory.GetInstance<IMessageProvider>())
                                                                                              
        {
        }


        public TransactionManager(ITransactionProvider dataProvider,IMessageProvider notificationProvider)
        {
            DataProvider = dataProvider;
            NotficationDataProvider = notificationProvider;
        }

        public static TransactionManager Create()
        {
            return new TransactionManager();
        }

        
        public List<DiPurchaseOrderListItem> FetchPOList(PurchaseOrderFilter filter)
        {

            var polist = DataProvider.FetchPurchaseOrdersList(filter);
            var diPoList = new List<DiPurchaseOrderListItem>();
            foreach (var po in polist)
            {
                diPoList.Add(DiPurchaseOrderListItem.Create(po.PoNbr, po.CompanyId, po.CustomerName, po.DeliveryDate.ToShortDateString(), po.Status.ToString(), po.CustomerNbr));

            }
            return diPoList;
        }

        public void UpdatePOLines(DiPurchaseOrder model)
        {
            var purchaseOrder = DataProvider.FetchPO(model.PONbr);

            foreach (var line in purchaseOrder.Lines)
            {
                var poVMLine = model.Lines.Find(u => u.ItemNbr == line.ItemNbr);
                if (poVMLine != null)
                {
                    line.QtyFulfilled = Convert.ToInt32(poVMLine.QtyFulfilled);
                }
            }

            DataProvider.UpdatePODetail(purchaseOrder);

            SetPOStatusByQtyFulfilled(purchaseOrder, model);
            if (purchaseOrder.Status == POStatusType.Closed)
            {
                NotifyCustomer(model, "fulfill");
            }
            UpdateInventory(purchaseOrder);
        }

        private void UpdateInventory(PurchaseOrder purchaseOrderModel)
        {
            DataProvider.UpdateInventory(purchaseOrderModel);
        }

        private void SetPOStatusByQtyFulfilled(PurchaseOrder purchaseOrderModel, DiPurchaseOrder model)
        {
            var anyChange = purchaseOrderModel.Lines.TrueForAll(u => u.QtyOrdered == u.QtyFulfilled);
            if (anyChange)
            {
                purchaseOrderModel.Status = POStatusType.Closed;
                DataProvider.UpdatePoStatus(purchaseOrderModel.PONbr, purchaseOrderModel.Status);
               
            }
        }


        public void SavePurchaseOrder(DiPurchaseOrder model)
        {
            var purchaseOrderModel = PurchaseOrder.Create();
            SetPOHeader(model, purchaseOrderModel);
            SetPOLines(model , purchaseOrderModel);
            DataProvider.SavePurchaseOrder(purchaseOrderModel);

        }

        private void SetPOLines(DiPurchaseOrder model, PurchaseOrder purchaseOrderModel)
        {
            foreach (var line in model.Lines)
            {
                purchaseOrderModel.Lines.Add(PurchaseOrderLine.Create(line.ItemNbr,Convert.ToInt32(line.Qty),Convert.ToInt32(line.QtyFulfilled)));
            }

        }

        private void SetPOHeader(DiPurchaseOrder model, PurchaseOrder purchaseOrderModel)
        {
            purchaseOrderModel.PONbr = model.PONbr;
            purchaseOrderModel.CustomerNbr = model.CustomerNbr;
            purchaseOrderModel.CreatedBy = model.CreatedBy;
            purchaseOrderModel.Status = EnumHelper.ToEnum<POStatusType>(model.Status);
            purchaseOrderModel.DeliveryDate = Convert.ToDateTime(model.DeliveryDate);
            purchaseOrderModel.CustomerEmail = model.CustomerEmail;
        }

        public void NotifyCustomer(DiPurchaseOrder model,string type)
        {
            var diEmailMessage = DiEmailMessage.Create();

            if (type == "create")
            {
                diEmailMessage.Body = string.Format("PO - {0} is succsesfully created and est delivery date around {1}",
                    model.PONbr, model.DeliveryDate);
                diEmailMessage.Subject = string.Format("{0} created", model.PONbr);
            }
              
            if (type == "fulfill")
            {
                diEmailMessage.Body = string.Format("PO - {0} is succsesfully fulfilled and shipped on {1}",
                    model.PONbr,DateTime.Now.ToShortDateString());
                diEmailMessage.Subject = string.Format("{0} Fulfilled", model.PONbr);
            }
            diEmailMessage.Recipients.Add(model.CustomerEmail);
          

            NotficationDataProvider.SendEmail(diEmailMessage);
        }

        public DiPurchaseOrder FetchPO(PurchaseOrderFilter filter)
        {
            var purchaseOrder = DataProvider.FetchPO(filter.PoNbr);
            var diPurchaseOrder = DiPurchaseOrder.Create();

            diPurchaseOrder.PONbr = purchaseOrder.PONbr;
            diPurchaseOrder.CustomerNbr = purchaseOrder.CustomerNbr;
            diPurchaseOrder.CustomerEmail = purchaseOrder.CustomerEmail;

            diPurchaseOrder.DeliveryDate = purchaseOrder.DeliveryDate.ToShortDateString();
            diPurchaseOrder.CreatedDateTime = purchaseOrder.CreatedDateTime.ToShortDateString();

            diPurchaseOrder.CreatedBy = purchaseOrder.CreatedBy;

            diPurchaseOrder.Status = purchaseOrder.Status.ToString();

            foreach (var line in purchaseOrder.Lines)
            {
                diPurchaseOrder.Lines.Add(DiPurchaseOrderLine.Create(line.ItemNbr,line.QtyOrdered.ToString(),line.QtyFulfilled.ToString()));
            }


            return diPurchaseOrder;
        }
    }
}
