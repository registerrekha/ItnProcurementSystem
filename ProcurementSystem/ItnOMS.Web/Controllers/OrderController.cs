using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Itn.Shared.Transaction;
using ItnOMS.Web.Managers;
using ItnOMS.Web.Models;

namespace ItnOMS.Web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        #region Create PO 

        public ActionResult Create(FormCollection collection)
        {
            if(!collection.HasKeys()) return View();

            var poHeaderVM = POHeaderVM.Create();
            try
            {
                SetPoHeader(poHeaderVM, collection);
                var diPurchaseOrderModel = POHeaderVM.ToDiPurchaseOrderModel(poHeaderVM);
                TransactionManager.Create().PostPOModel(diPurchaseOrderModel);

            }
            catch (Exception ex)
            {

            }
            //  return RedirectToAction("Index", "Home",new{area = "" });
            return Json(new { result = "Redirect", url = Url.Action("Index", "Home") });

        }

        private void SetPoHeader(POHeaderVM poHeaderVm, FormCollection collection)
        {
            poHeaderVm.CustomerNbr = collection["customernbr"];
            poHeaderVm.CustomerEmail = collection["customeremail"];
            poHeaderVm.CustomerName = collection["customername"];
            poHeaderVm.PONbr = collection["PONbr"];
            poHeaderVm.DeliveryDate = collection["DeliveryDate"];
            //There is no naming convention for first line in the HTML name
            SetFirstLine(poHeaderVm, collection);
           

            foreach (var key in collection.Keys)
            {
              
                if (!((string)key).Contains("_")) continue;

                //We added the first line already
                var lineNbr = ((string)key).Split('_')[1];
                lineNbr = (Convert.ToInt32(lineNbr) + 1).ToString();

                POLineVM poLine;
                // Find the line
                if (poHeaderVm.Lines.Exists(u => u.LineNbr == Convert.ToInt32(lineNbr)))
                {
                    poLine = poHeaderVm.Lines.Find(u => u.LineNbr == Convert.ToInt32(lineNbr));
                }
                else
                {
                    poLine = POLineVM.Create();
                    poLine.LineNbr = poHeaderVm.Lines.Count;
                    poHeaderVm.Lines.Add(poLine);
                }


                var propName = ((string) key).Split('_')[0];
                if (propName == "item")
                {
                    poLine.ItemNbr = collection[key.ToString()];
                }
                if (propName == "qty")
                {
                    poLine.Qty = collection[key.ToString()];
                }
                if (propName == "desc")
                {
                    poLine.Desc = collection[key.ToString()];
                }
             }
        }

        private void SetFirstLine(POHeaderVM poHeaderVm, FormCollection collection)
        {
            var poFirstLine = POLineVM.Create();

            if (collection["item"] != null)
            {
                poFirstLine.ItemNbr = collection["item"];
            }
            if (collection["qty"] != null)
            {
                poFirstLine.Qty = collection["qty"];
            }
            if (collection["desc"] != null)
            {
                poFirstLine.Desc = collection["desc"];
            }
            poHeaderVm.Lines.Add(poFirstLine);
        }

        #endregion  



        #region Review
        public ActionResult Review()
        {
            var purchaseOrdersVM = TransactionManager.Create().FetchPOList();
            return View(purchaseOrdersVM);
        }

        private void SetSearchErrorViewbag(string msg)
        {
            ViewBag.Type = "Error";
            ViewBag.Message = msg;
        }

        public ActionResult Search(FormCollection collection)
        {
            var customerSearchList = new List<DiPurchaseOrderListItem>();
            try
            {
                var poFilter = SetPOFilter(collection);
                customerSearchList = TransactionManager.Create().FetchPOList(poFilter);
                return PartialView("_ResultPOList", customerSearchList);
            }
            catch (Exception ex)
            {
                var msg = string.Format("Error: [{0}] getting the search results.", ex.Message);
                SetSearchErrorViewbag(msg);
            }
            return PartialView("_ResultPOList", customerSearchList);
        }

        private PurchaseOrderFilter SetPOFilter(FormCollection collection)
        {
            var postatus = collection["status"];
            var emailAddress = collection["emailaddress"];

            if (string.IsNullOrEmpty(postatus) && string.IsNullOrEmpty(emailAddress)) return null;

            return new PurchaseOrderFilter()
            {
                Email = emailAddress,
                Status = postatus
            };
        }

        #endregion


        public ActionResult FulfillList()
        {
            
            var poFilter = new PurchaseOrderFilter  {  Status =  "Open"};
            var purchaseOrdersVM =
                TransactionManager.Create().FetchPOList(poFilter).OrderBy(u => u.DeliveryDate).ToList();
            return View(purchaseOrdersVM);
        }

        public ActionResult Fulfill(string ponbr)
        {
            var purchaseOrder = TransactionManager.Create().FetchPO(ponbr);
            AssignLineNbrs(purchaseOrder);
            Session["PurchaseOrderModel"] = purchaseOrder;
            return View("POOrderFulfill",purchaseOrder);
        }

        private void AssignLineNbrs(DiPurchaseOrder purchaseOrder)
        {
            var lineNbr = 1;
            foreach (var line in purchaseOrder.Lines)
            {
                line.LineNbr = lineNbr++;
            }
        }

        public ActionResult PoFulfill(FormCollection collection)
        {
            var purchaseOrderModel = (DiPurchaseOrder) Session["PurchaseOrderModel"];
            UpdateLineQtyFulfilled(purchaseOrderModel, collection);
            TransactionManager.Create().FulfillPO(purchaseOrderModel);
            //return RedirectToAction("Index", "Home", new { area = "" });
            return Json(new { result = "Redirect", url = Url.Action("Index", "Home") });
        }

        private void UpdateLineQtyFulfilled(DiPurchaseOrder purchaseOrderModel, FormCollection collection)
        {
            foreach (var key in collection.Keys)
            {

                if (!((string) key).Contains("_")) continue;

                //We added the first line already
                var lineNbr = ((string) key).Split('_')[1];

                DiPurchaseOrderLine poLine;

                // Find the line
                if (purchaseOrderModel.Lines.Exists(u => u.LineNbr == Convert.ToInt32(lineNbr)))
                {
                    poLine = purchaseOrderModel.Lines.Find(u => u.LineNbr == Convert.ToInt32(lineNbr));

                    var propName = ((string) key).Split('_')[0];

                    if (propName == "qtyfulfill")
                    {
                        poLine.QtyFulfilled = collection[key.ToString()];
                    }
                }
            }
        }
    }
}