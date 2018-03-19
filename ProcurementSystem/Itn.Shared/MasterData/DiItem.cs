using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itn.Shared.MasterData
{
    public class DiItem
    {
        public string ItemNbr { get; set; }
        public string Description { get; set; }
        public string ItemType { get; set; } // ItemTypeType
       
        public string Brand { get; set; }
       
        public string NetWeight { get; set; } // float
        
        public string Upc { get; set; }
        public string IsSerialized { get; set; } // bool
        
        public DiCodeDescription DefaultUom { get; set; }
        public string OverallStatus { get; set; } // StatusType
        public string LastModified { get; set; }




        public static DiItem Create(string itemNbr, string description, string itemType,
    string brand, 
    float netWeight,  string upc, bool isSerialized,
    DiCodeDescription defaultUom,string overallStatus)
        {
            return new DiItem
            {
                ItemNbr = itemNbr,
                Description = description,
                ItemType = itemType.ToString(), 
                Brand = brand,
                NetWeight = netWeight.ToString(),
              
                Upc = upc,
                IsSerialized = isSerialized.ToString(),
               
                DefaultUom = defaultUom,
                
                OverallStatus = overallStatus.ToString(),
               
            };
        }

        public static DiItem Create(string itemNbr, string description)
        {
            return new DiItem
            {
                ItemNbr = itemNbr,
                Description = description
            };
        }
    }
}
