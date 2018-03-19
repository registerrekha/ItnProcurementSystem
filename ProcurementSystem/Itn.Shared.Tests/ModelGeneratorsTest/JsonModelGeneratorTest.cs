using System;
using Itn.Shared.Transaction;
using NUnit.Framework;
using System.Collections.Generic;
using Itn.Shared.Notification;

namespace Itn.Shared.Tests.ModelGeneratorsTest
{
    [TestFixture]
    public class JsonModelGeneratorTest : BaseModelGenerator
    {
        [Test]
        public void SystemShouldGenerateJsonModelForPO()
        {
            var model = DiPurchaseOrder.Create();
            model.PONbr = "PO1";
            model.CreatedBy = "rv";
            model.CustomerNbr = "11001";
            model.DeliveryDate = DateTime.Now.AddDays(4).ToShortDateString();
            model.Status = "Open";

            model.Lines = new List<DiPurchaseOrderLine>
            {
                DiPurchaseOrderLine.Create("101","34","0"),
                DiPurchaseOrderLine.Create("102","35","0")
            };
            Write("DiPurchaseOrder.json", model);
        }

        [Test]
        public void SystemShouldGenerateJsonModelForEmail()
        {
            var model = DiEmailMessage.Create("registerrekha@gmail.com", "Test Email", "Test", "vaddirekha@gmail.com");
        
            Write("DiEmailMessage.json", model);
        }

    }
}
