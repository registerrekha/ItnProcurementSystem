using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itn.MasterDataServices.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public string CustomerNbr { get; set; }
        public string SalesRep { get; set; }
       
        public string DeliveryMode { get; set; }
        public string Company { get; set; }
        public string DefaultCurrency { get; set; }
       
        public static Customer Create(int customerId, string customerNbr, string name,string email)
        {
            return new Customer
            {
                Id = customerId,
                CustomerNbr = customerNbr,
                Name = name,
                Email = email
            };
        }
    }
}
