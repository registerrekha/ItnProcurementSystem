
using Itn.MasterDataServices.DAL.Implementations;
using Itn.Utilities.Exceptions;
using NUnit.Framework;

namespace Itn.Tests.MasterData
{
    [TestFixture]
    public class MasterDataDALTest
    {

        [Test]
        public void SystemShouldReturnListOfCustomer()
        {
            var customerList = new MasterDataSqlDataProvider().FetchCustomers();
            Assert.IsTrue(customerList.Count > 0);
        }

        [Test]
        public void SystemShouldReturnFalseCustomerByCustomerNbr()
        {
            var customerNbr = "test1";
            var customer = new MasterDataSqlDataProvider().FetchCustomer(customerNbr);
            Assert.IsFalse(customer.CustomerNbr == "test1");
        }

        [Test]
        public void SystemShouldReturnCustomerByCustomerNbr()
        {
            var customerNbr = "test";
            var customer = new MasterDataSqlDataProvider().FetchCustomer(customerNbr);
            Assert.IsTrue(customer.CustomerNbr == "test");
        }

        [Test]
        public void SystemShouldReturnListOfItems()
        {
            var itemsList = new MasterDataSqlDataProvider().FetchItems();
            Assert.IsTrue(itemsList.Count > 0);
        }

        [Test]
        [ExpectedException(typeof(RecordNotFoundException))]
        public void SystemShouldReturnRecordNotFoundException()
        { 
            var itemNbr = "test1";
            var item = new MasterDataSqlDataProvider().FetchItem(itemNbr);
        }

        [Test]
        public void SystemShouldReturnItemByItemNbr()
        {
            var itemNbr = "102";
            var item = new MasterDataSqlDataProvider().FetchItem(itemNbr);
            Assert.IsTrue(item.ItemNbr== itemNbr);
        }
    }
}
