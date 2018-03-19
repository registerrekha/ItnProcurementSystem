using Itn.MasterDataServices.DAL.Contracts;
using Itn.Shared.MasterData;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itn.MasterDataServices.Manager
{
    public class MasterDataManager
    {
        public IMasterDataProvider DataProvider { get; set; }

        public MasterDataManager() : this(ObjectFactory.GetInstance<IMasterDataProvider>())
        {
        }

        public MasterDataManager(IMasterDataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        public List<DiCustomer> GetCustomers()
        {
            var customers = DataProvider.FetchCustomers();
            var diCustomers = new List<DiCustomer>();
            foreach (var customer in customers)
            {
                diCustomers.Add(DiCustomer.Create(customer.CustomerNbr, customer.Name,customer.Email));
            }
            return diCustomers;
        }

        public List<DiItem> GetItems()
        {
            var items = DataProvider.FetchItems();
            var diItems = new List<DiItem>();
            foreach(var item in items)
            {
                diItems.Add(DiItem.Create(item.ItemNbr, item.Description));
            }
            return diItems ;
        }

        public static MasterDataManager Create()
        {
            return new MasterDataManager();
        }
        public static MasterDataManager Create(IMasterDataProvider dataProvider)
        {
            return new MasterDataManager(dataProvider);
        }

    }
}
