using System;
using Itn.OMS.Services.DAL.Implementations;
using Itn.OMS.Services.Models;
using Itn.Shared.Transaction;
using NUnit.Framework;

namespace Itn.Tests.ServicesData
{
    [TestFixture]
    public class TransactionSqlDataTest
    {
        [Test]
        public void SystemShouldFetchPObyStatus()
        {
            //only po140
            var purchaseFilter = new PurchaseOrderFilter {Status = "closed"};
            var dp = new TransactionSqlDataProvider();
            var list = dp.FetchPurchaseOrdersList(purchaseFilter);
            Assert.IsTrue(list.Count == 1);
        }

        [Test]
        public void SystemShouldAddPO()
        {
            var purchaseOrder = GetMockPO();

            var dp = new TransactionSqlDataProvider();
            dp.SavePurchaseOrder(purchaseOrder); 
            var po = dp.FetchPO(purchaseOrder.PONbr);

            Assert.IsTrue(po.PONbr == "TestPO");
        }


        private PurchaseOrder GetMockPO()
        {
            var purchaseOrder = PurchaseOrder.Create();

            purchaseOrder.PONbr = "TestPO";
            purchaseOrder.CustomerNbr = "test";
            purchaseOrder.CustomerEmail = "registerrekha@gmail.com";
            purchaseOrder.CreatedBy = "rv";
            purchaseOrder.CreatedDateTime = DateTime.Now;
            purchaseOrder.DeliveryDate = DateTime.Now.AddDays(6);
            purchaseOrder.Status = POStatusType.Open;
            purchaseOrder.Lines.Add(PurchaseOrderLine.Create("101", 100, 0));

            return purchaseOrder;

        }


    }

   
}
