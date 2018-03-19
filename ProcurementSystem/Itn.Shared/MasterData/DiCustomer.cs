using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itn.Shared.MasterData
{
    public class DiCustomer
    {
        public string CustomerNbr { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public static DiCustomer Create(string customerNbr, string name,string email)
        {
            return new DiCustomer
            {
                CustomerNbr = customerNbr,
                Name = name,
                Email = email
            };
        }
    }
}
