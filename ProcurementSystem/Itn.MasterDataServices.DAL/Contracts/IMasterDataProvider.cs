using Itn.MasterDataServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itn.MasterDataServices.DAL.Contracts
{
    public interface IMasterDataProvider
    {
        List<Item> FetchItems();
        List<Customer> FetchCustomers();
    }
}
