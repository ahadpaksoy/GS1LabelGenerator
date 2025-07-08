using System.ComponentModel.DataAnnotations;

namespace gs1BarcodeApplication.Models
{
    public class ProductLabelModel
    {
        [StringLength(18, ErrorMessage = "SSCC must be 18 characters long.")]
        [RegularExpression(@"^\d{18}$", ErrorMessage ="SSCC must contain only digits.")]
        public string sscc { get; set; }                      // (00) Serial Shipping Container Code
        [Required(ErrorMessage ="GTIN is required.")]
        [StringLength(14, ErrorMessage ="GTIN must be 14 characters exact.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "GTIN must contain only digits.")]
        public string GTIN { get; set; }                      // (01) Global Trade Item Number
        [StringLength(14, ErrorMessage = "GTIN must be 14 characters exact.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "GTIN must contain only digits.")]
        public string gtinContained { get; set; }             // (02) GTIN of contained trade items
        [StringLength(20, ErrorMessage ="Batch lot number can no exceed 20 characters.")]
        public string batch_lotNumber { get; set; }           // (10) Batch or lot number
        [RegularExpression(@"^d{6}$", ErrorMessage ="Date must be in YYMMDD format.")]
        public string productionDate { get; set; }            // (11) Production date (YYMMDD)
        [RegularExpression(@"^d{6}$", ErrorMessage = "Date must be in YYMMDD format.")]
        public string dueDate { get; set; }                    // (12) Due date (YYMMDD)
        [RegularExpression(@"^d{6}$", ErrorMessage = "Date must be in YYMMDD format.")]
        public string packDate { get; set; }                   // (13) Packaging date (YYMMDD)
        [RegularExpression(@"^d{6}$", ErrorMessage = "Date must be in YYMMDD format.")]
        public string bestBeforeDate { get; set; }             // (15) Best before date (YYMMDD)
        [RegularExpression(@"^d{6}$", ErrorMessage = "Date must be in YYMMDD format.")]
        public string expirationDate { get; set; }             // (17) Expiration date (YYMMDD)
        [StringLength(2,ErrorMessage ="Maximum 2 digit",MinimumLength =1)]
        public string productVariant { get; set; }             // (20) Product variant
        [StringLength(20, ErrorMessage = "Serial number can no exceed 30 characters.")]
        public string serialNumber { get; set; }               // (21) Serial number
        [StringLength(18, ErrorMessage = "Healthcare GTIN must be 18 characters long.")]
        [RegularExpression(@"^d{18}$", ErrorMessage ="GTIN must contain only digits.")]
        public string healthCareGTIN { get; set; }             // (22) Global Trade Item Number for healthcare trade items
        [StringLength(30,ErrorMessage ="Maximum 30 character long.")]
        public string pharmaLotNumber { get; set; }            // (241) Pharma lot number
        [StringLength(30, ErrorMessage = "Maximum 30 character long.")]
        public string additionalProductID { get; set; }        // (242) Additional product identification
        [StringLength(30, ErrorMessage = "Maximum 30 character long.")]
        public string customerPartNumber { get; set; }         // (243) Customer part number
        [StringLength(6, ErrorMessage="Maximum 6 digit.")]
        [RegularExpression("^d+$", ErrorMessage ="Must contain only digits.")]
        public string madeToOrderVariation { get; set; }       // (250) Made-to-order variation number
        [StringLength(30, ErrorMessage = "Maximum 30 character long.")]
        public string secondarySerialNumber { get; set; }      // (251) Secondary serial number
        [StringLength(30, ErrorMessage = "Maximum 30 character long.")]
        [RegularExpression(@"^\d{13}[a-zA-Z0-9]{0,17}$", ErrorMessage = "GDTI must start with 13 digits followed by up to 17 alphanumeric characters for the serial component.")]
        public string gdti { get; set; }                       // (253) Global Document Type Identifier
        [StringLength(25, ErrorMessage ="Maximum 25 character long.")]
        public string gcn { get; set; }                         // (255) Global Coupon Number
        public string variableCount { get; set; }               // (30) Variable measure count
        public string countContained { get; set; }              // (37) Count of contained trade items

        // Measurements
        public string netWeightKg { get; set; }                 // (3102) Net weight (kg)
        public string lengthM { get; set; }                      // (3112) Length (m)
        public string widthM { get; set; }                       // (3122) Width (m)
        public string depthM { get; set; }                       // (3132) Depth (m)
        public string areaSqM { get; set; }                      // (3142) Area (m²)
        public string netVolumeL { get; set; }                   // (3152) Net volume (L)
        public string netVolumeM3 { get; set; }                  // (3162) Net volume (m³)
        public string netWeightLb { get; set; }                  // (3202) Net weight (lbs)
        public string lengthIn { get; set; }                     // (3212) Length (inches)
        public string widthIn { get; set; }                      // (3222) Width (inches)
        public string depthIn { get; set; }                      // (3232) Depth (inches)
        public string areaSqIn { get; set; }                     // (3242) Area (in²)
        public string netVolumeUSGal { get; set; }               // (3252) Net volume (US gallons)
        public string netVolumeCubicIn { get; set; }             // (3262) Net volume (cubic inches)
        public string grossWeightKg { get; set; }                // (3302) Gross weight (kg)
        public string lengthGrossM { get; set; }                 // (3312) Gross length (m)
        public string widthGrossM { get; set; }                  // (3322) Gross width (m)
        public string depthGrossM { get; set; }                  // (3332) Gross depth (m)
        public string areaGrossSqM { get; set; }                 // (3342) Gross area (m²)
        public string grossVolumeL { get; set; }                 // (3352) Gross volume (L)
        public string grossVolumeM3 { get; set; }                // (3362) Gross volume (m³)
        public string grossWeightLb { get; set; }                // (3402) Gross weight (lbs)
        public string lengthGrossIn { get; set; }                // (3412) Gross length (inches)
        public string widthGrossIn { get; set; }                 // (3422) Gross width (inches)
        public string depthGrossIn { get; set; }                 // (3432) Gross depth (inches)
        public string areaGrossSqIn { get; set; }                // (3442) Gross area (in²)
        public string grossVolumeUSGal { get; set; }             // (3452) Gross volume (US gallons)
        public string grossVolumeCubicIn { get; set; }           // (3462) Gross volume (cubic inches)
        public string areaSqFt { get; set; }                      // (3472) Area (ft²)
        public string lengthFt { get; set; }                       // (3482) Length (ft)
        public string widthFt { get; set; }                        // (3492) Width (ft)
        public string depthFt { get; set; }                        // (3502) Depth (ft)
        public string grossWeightMetricTon { get; set; }          // (3512) Gross weight (metric tons)
        public string netWeightMetricTon { get; set; }            // (3522) Net weight (metric tons)
        public string netVolumeCubicFt { get; set; }              // (3532) Net volume (cubic feet)
        public string netVolumeQt { get; set; }                    // (3542) Net volume (quarts)
        public string netWeightOz { get; set; }                    // (3552) Net weight (oz)
        public string netVolumeUSFlOz { get; set; }                // (3562) Net volume (US fluid oz)
        public string netVolumeImpGal { get; set; }                // (3572) Net volume (imperial gal)
        public string netVolumeImpFlOz { get; set; }               // (3582) Net volume (imperial fluid oz)
        public string netVolumeCubicYd { get; set; }               // (3592) Net volume (cubic yards)
        public string netVolumeUSPint { get; set; }                // (3602) Net volume (US pints)
        public string netVolumeUSQt { get; set; }                  // (3612) Net volume (US quarts)
        public string netVolumeImpPint { get; set; }               // (3622) Net volume (imperial pints)
        public string netVolumeImpQt { get; set; }                 // (3632) Net volume (imperial quarts)
        public string netVolumeLitPer100Kg { get; set; }           // (3642) Net volume (liters per 100 kg)

        public string numConsumerUnits { get; set; }               // (3900) Number of consumer units
        public string amountPayable { get; set; }                  // (3902) Amount payable
        public string amountPayableISO { get; set; }               // (3903) Amount payable with ISO currency
        public string amountPayableVar { get; set; }               // (3912) Amount payable variable
        public string amountPayableVarISO { get; set; }            // (3913) Amount payable variable with ISO currency

        public string customerPONumber { get; set; }                // (400) Customer purchase order number
        public string consignmentNumber { get; set; }              // (401) Consignment number
        public string gsin { get; set; }                            // (402) Global Shipment Identification Number
        public string routingCode { get; set; }                     // (403) Routing code
        public string shipTo { get; set; }                          // (410) Ship to party
        public string billTo { get; set; }                          // (411) Bill to party
        public string purchaseFrom { get; set; }                    // (412) Purchase from party
        public string forwardTo { get; set; }                        // (413) Forward to party
        public string physicalLoc { get; set; }                     // (414) Physical location
        public string invoicingParty { get; set; }                  // (415) Invoicing party
        public string shipToPost { get; set; }                       // (420) Ship to postal code
        public string shipToPostWithIso { get; set; }               // (421) Ship to postal code with ISO country code
        [StringLength(3,ErrorMessage ="Must be country ISO code.")]
        [RegularExpression(@"^d{3}", ErrorMessage ="Maximum 3 digit long.")]
        public string origin { get; set; }                           // (422) Country of origin
        [StringLength(3, ErrorMessage = "Must be country ISO code.")]
        [RegularExpression(@"^d{3}", ErrorMessage = "Maximum 3 digit long.")]
        public string initialProcessingCountry { get; set; }        // (423) Initial processing country
        [StringLength(3, ErrorMessage = "Must be country ISO code.")]
        [RegularExpression(@"^d{3}", ErrorMessage = "Maximum 3 digit long.")]
        public string processingCountry { get; set; }               // (424) Processing country
        [StringLength(3, ErrorMessage = "Must be country ISO code.")]
        [RegularExpression(@"^d{3}", ErrorMessage = "Maximum 3 digit long.")]
        public string disassemblyCountry { get; set; }              // (425) Disassembly country
        [StringLength(3, ErrorMessage = "Must be country ISO code.")]
        [RegularExpression(@"^d{3}", ErrorMessage = "Maximum 3 digit long.")]
        public string fullProcessCountry { get; set; }              // (426) Full process country
        [RegularExpression(@"^d{6}$", ErrorMessage = "Date must be in YYMMDD format.")]
        public string expirationDateTime { get; set; }              // (427) Expiration date/time

        public string componentStructure { get; set; }              // (7001) Component/part structure
        public string prodUrl { get; set; }                          // (8001) Production URL
        public string prodCharacteristics { get; set; }             // (8002) Product characteristics
        public string healthcareClass { get; set; }                  // (8018) Healthcare class
        public string prodDataStatus { get; set; }                   // (8020) Product data status
        public string shelfLifeDuration { get; set; }                // (8100) Shelf life duration
    }
}
