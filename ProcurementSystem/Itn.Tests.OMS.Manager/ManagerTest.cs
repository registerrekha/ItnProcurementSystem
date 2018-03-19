using System;
using Itn.OMS.Services.Manager;
using Itn.OMS.Services.Models;
using Itn.Shared.Transaction;
using Itn.Tests.OMS.Manager.Registry;
using NUnit.Framework;

namespace Itn.Tests.OMS.Manager
{
    [TestFixture]
    public class ManagerTest
    {

        [Test]
        public void SystemShouldNotClosePOIfAllLinesAreNotFulfilled()
        {
            ServiceBootstrapper.Bootstrap(new BaseServicesRegistry());
            var mgr = TransactionManager.Create();

            mgr.SavePurchaseOrder(GetMockDiPO());

            var diModeltoFulfill = GetMockDiPO();
            diModeltoFulfill.Lines[0].QtyFulfilled = "50";
            mgr.UpdatePOLines(diModeltoFulfill);

            var po = mgr.FetchPO(new PurchaseOrderFilter() {PoNbr = "TestPO1"});
            Assert.IsTrue(po.Status.ToLower() == "open");
        }

        //Working but need to remove notify to diff section ..

        [Test]
        public void SystemShouldClosePOIfAllLinesAreFulfilled()
        {
            ServiceBootstrapper.Bootstrap(new BaseServicesRegistry());
            var mgr = TransactionManager.Create();
            var poModel = GetMockDiPO();
            poModel.PONbr = "Mock2";
            mgr.SavePurchaseOrder(poModel);

            var diModeltoFulfill = GetMockDiPO();
            diModeltoFulfill.PONbr = "Mock2";
            diModeltoFulfill.Lines[0].QtyFulfilled = "100";
            mgr.UpdatePOLines(diModeltoFulfill);

            var po = mgr.FetchPO(new PurchaseOrderFilter() { PoNbr = "Mock1" });
            Assert.IsTrue(po.Status.ToLower() == "closed");
        }


        private DiPurchaseOrder GetMockDiPO()
        {
            var dipurchaseOrder = DiPurchaseOrder.Create();

            dipurchaseOrder.PONbr = "TestPO1";
            dipurchaseOrder.CustomerNbr = "test";
            dipurchaseOrder.CustomerEmail = "registerrekha@gmail.com";
            dipurchaseOrder.CreatedBy = "rv";
            dipurchaseOrder.CreatedDateTime = DateTime.Now.ToShortDateString();
            dipurchaseOrder.DeliveryDate = DateTime.Now.AddDays(6).ToShortDateString();
            dipurchaseOrder.Status = POStatusType.Open.ToString();
            dipurchaseOrder.Lines.Add(DiPurchaseOrderLine.Create("102", 100.ToString(), 0.ToString()));

            return dipurchaseOrder;

        }


    }
}
