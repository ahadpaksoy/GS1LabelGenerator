using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gs1BarcodeApplication.Models
{
    public class ProductLabelModel
    {
        public string productName { get; set; }
        public string GTIN { get; set; } // 01
        public string batch_lotNumber { get; set; } // 10
        public string productionDate { get; set; } // 11 
        public string bestBeforeDate { get; set; } // (15)	
        public string expirationDate { get; set; } // 17
        public string serialNumber { get; set; } // 21
        public string veriableCount { get; set; } // 30
        public string netWeight { get; set; } // kg (3102)	
        public string netVolume { get; set; } // litre 3152 
        public string width { get; set; } // feet 3252
        public string netWeightUS { get; set; } // oz 3562
        public string netVolumeUS { get; set; } // qt 3602
        public string amaountPayable { get; set; } // single currency 3902
        public string customerPONumber { get; set; } // customer refrence 400
    }
}