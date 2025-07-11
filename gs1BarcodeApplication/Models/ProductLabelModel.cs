using System.ComponentModel.DataAnnotations;

namespace gs1BarcodeApplication.Models
{
    public class ProductLabelModel
    {
        // --- Core Identifiers ---

        [Display(Name = "SSCC (00)")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "SSCC must be exactly 18 characters long.")]
        [RegularExpression(@"^\d{18}$", ErrorMessage = "SSCC must contain only digits.")]
        public string sscc { get; set; }

        [Display(Name = "GTIN (01)")]
        [Required(ErrorMessage = "GTIN is required.")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "GTIN must be exactly 14 digits.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "GTIN must contain only digits.")]
        public string GTIN { get; set; }

        [Display(Name = "GTIN of Contained Items (02)")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Contained GTIN must be exactly 14 digits.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "Contained GTIN must contain only digits.")]
        public string gtinContained { get; set; }

        [Display(Name = "Batch / Lot Number (10)")]
        [StringLength(20, ErrorMessage = "Batch/Lot number cannot exceed 20 characters.")]
        public string batch_lotNumber { get; set; }

        // --- Dates (All YYMMDD) ---

        [Display(Name = "Production Date (11)")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Production Date must be in YYMMDD format.")]
        public string productionDate { get; set; }

        [Display(Name = "Due Date (12)")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Due Date must be in YYMMDD format.")]
        public string dueDate { get; set; }

        [Display(Name = "Packaging Date (13)")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Packaging Date must be in YYMMDD format.")]
        public string packDate { get; set; }

        [Display(Name = "Best Before Date (15)")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Best Before Date must be in YYMMDD format.")]
        public string bestBeforeDate { get; set; }

        [Display(Name = "Expiration Date (17)")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Expiration Date must be in YYMMDD format.")]
        public string expirationDate { get; set; }

        // --- Other Identifiers ---

        [Display(Name = "Product Variant (20)")]
        [RegularExpression(@"^\d{2}$", ErrorMessage = "Product Variant must be exactly 2 digits.")]
        public string productVariant { get; set; }

        [Display(Name = "Serial Number (21)")]
        [StringLength(20, ErrorMessage = "Serial number cannot exceed 20 characters.")]
        public string serialNumber { get; set; }

        [Display(Name = "Healthcare GTIN (22)")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "Healthcare GTIN must be 18 characters long.")]
        [RegularExpression(@"^\d{18}$", ErrorMessage = "Healthcare GTIN must contain only digits.")]
        public string healthCareGTIN { get; set; }

        [Display(Name = "Pharma Lot Number (241)")]
        [StringLength(30, ErrorMessage = "Pharma Lot Number cannot exceed 30 characters.")]
        public string pharmaLotNumber { get; set; }

        [Display(Name = "Additional Product ID (242)")]
        [StringLength(30, ErrorMessage = "Additional Product ID cannot exceed 30 characters.")]
        public string additionalProductID { get; set; }

        [Display(Name = "Customer Part Number (243)")]
        [StringLength(30, ErrorMessage = "Customer Part Number cannot exceed 30 characters.")]
        public string customerPartNumber { get; set; }

        [Display(Name = "Made-to-Order Variation (250)")]
        [RegularExpression(@"^\d{1,6}$", ErrorMessage = "Made-to-Order Variation must be up to 6 digits.")]
        public string madeToOrderVariation { get; set; }

        [Display(Name = "Secondary Serial Number (251)")]
        [StringLength(30, ErrorMessage = "Secondary Serial Number cannot exceed 30 characters.")]
        public string secondarySerialNumber { get; set; }

        [Display(Name = "Global Document Type Identifier (253)")]
        [RegularExpression(@"^\d{13}[a-zA-Z0-9]{0,17}$", ErrorMessage = "GDTI must be 13 digits + up to 17 alphanumeric chars.")]
        public string gdti { get; set; }

        [Display(Name = "Global Coupon Number (255)")]
        [RegularExpression(@"^\d{13,25}$", ErrorMessage = "GCN must be between 13 and 25 digits.")]
        public string gcn { get; set; }

        [Display(Name = "Variable Count (30)")]
        [RegularExpression(@"^\d{1,8}$", ErrorMessage = "Variable Count must be up to 8 digits.")]
        public string variableCount { get; set; }

        [Display(Name = "Count of Contained Items (37)")]
        [RegularExpression(@"^\d{1,8}$", ErrorMessage = "Count of Contained Items must be up to 8 digits.")]
        public string countContained { get; set; }

        // --- All Measurements ---
        private const string MeasurementRegex = @"^\d{1,6}$";
        private const string MeasurementError = "Measurement value must between 1 - 6 digits.";

        [Display(Name = "Net Weight (kg) (3102)")]
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        public string netWeightKg { get; set; }

        [Display(Name = "Length (m) (3112)")]
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        public string lengthM { get; set; }

        [Display(Name = "Width (m) (3122)")]
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        public string widthM { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Depth (m) (3132)")]
        public string depthM { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Area (m²) (3142)")]
        public string areaSqM { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (L) (3152)")]
        public string netVolumeL { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (m³) (3162)")]
        public string netVolumeM3 { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Weight (lbs) (3202)")]
        public string netWeightLb { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Length (in) (3212)")]
        public string lengthIn { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Width (in) (3222)")]
        public string widthIn { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Depth (in) (3232)")]
        public string depthIn { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Area (in²) (3242)")]
        public string areaSqIn { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (US gal) (3252)")]
        public string netVolumeUSGal { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (cubic in) (3262)")]
        public string netVolumeCubicIn { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Weight (kg) (3302)")]
        public string grossWeightKg { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Length (m) (3312)")]
        public string lengthGrossM { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Width (m) (3322)")]
        public string widthGrossM { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Depth (m) (3332)")]
        public string depthGrossM { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Area (m²) (3342)")]
        public string areaGrossSqM { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Volume (L) (3352)")]
        public string grossVolumeL { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Volume (m³) (3362)")]
        public string grossVolumeM3 { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Weight (lbs) (3402)")]
        public string grossWeightLb { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Length (in) (3412)")]
        public string lengthGrossIn { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Width (in) (3422)")]
        public string widthGrossIn { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Depth (in) (3432)")]
        public string depthGrossIn { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Area (in²) (3442)")]
        public string areaGrossSqIn { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Volume (US gal) (3452)")]
        public string grossVolumeUSGal { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Volume (cubic in) (3462)")]
        public string grossVolumeCubicIn { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Area (ft²) (3472)")]
        public string areaSqFt { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Length (ft) (3482)")]
        public string lengthFt { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Width (ft) (3492)")]
        public string widthFt { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Depth (ft) (3502)")]
        public string depthFt { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Gross Weight (metric tons) (3512)")]
        public string grossWeightMetricTon { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Weight (metric tons) (3522)")]
        public string netWeightMetricTon { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (cubic ft) (3532)")]
        public string netVolumeCubicFt { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (quarts) (3542)")]
        public string netVolumeQt { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Weight (oz) (3552)")]
        public string netWeightOz { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (US fluid oz) (3562)")]
        public string netVolumeUSFlOz { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (imperial gal) (3572)")]
        public string netVolumeImpGal { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (imperial fluid oz) (3582)")]
        public string netVolumeImpFlOz { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (cubic yards) (3592)")]
        public string netVolumeCubicYd { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (US pints) (3602)")]
        public string netVolumeUSPint { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (US quarts) (3612)")]
        public string netVolumeUSQt { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (imperial pints) (3622)")]
        public string netVolumeImpPint { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (imperial quarts) (3632)")]
        public string netVolumeImpQt { get; set; }
        [RegularExpression(MeasurementRegex, ErrorMessage = MeasurementError)]
        [Display(Name = "Net Volume (liters per 100 kg) (3642)")]
        public string netVolumeLitPer100Kg { get; set; }

        // --- Amounts and Counts ---
        [Display(Name = "Number of Consumer Units (3900)")]
        [RegularExpression(@"^\d{1,15}$", ErrorMessage = "Number of consumer units must be numeric.")]
        public string numConsumerUnits { get; set; }

        [Display(Name = "Amount Payable (3902)")]
        public string amountPayable { get; set; }
        [Display(Name = "Amount Payable with ISO Currency (3903)")]
        public string amountPayableISO { get; set; }
        [Display(Name = "Amount Payable Variable (3912)")]
        public string amountPayableVar { get; set; }
        [Display(Name = "Amount Payable Variable with ISO Currency (3913)")]
        public string amountPayableVarISO { get; set; }

        // --- Logistics and Party Information ---
        [Display(Name = "Customer PO Number (400)")]
        [StringLength(30, ErrorMessage = "Customer PO Number cannot exceed 30 characters.")]
        public string customerPONumber { get; set; }

        [Display(Name = "Consignment Number (401)")]
        [StringLength(30, ErrorMessage = "Consignment Number cannot exceed 30 characters.")]
        public string consignmentNumber { get; set; }

        [Display(Name = "Global Shipment Identification Number (402)")]
        [RegularExpression(@"^\d{17}$", ErrorMessage = "GSIN must be 17 digits.")]
        public string gsin { get; set; }

        [Display(Name = "Routing Code (403)")]
        [StringLength(30, ErrorMessage = "Routing Code cannot exceed 30 characters.")]
        public string routingCode { get; set; }

        private const string GlnRegex = @"^\d{13}$";
        private const string GlnError = "GLN (Global Location Number) must be 13 digits.";

        [Display(Name = "Ship To (GLN) (410)")]
        [RegularExpression(GlnRegex, ErrorMessage = GlnError)]
        public string shipTo { get; set; }

        [Display(Name = "Bill To (GLN) (411)")]
        [RegularExpression(GlnRegex, ErrorMessage = GlnError)]
        public string billTo { get; set; }

        [Display(Name = "Purchase From (GLN) (412)")]
        [RegularExpression(GlnRegex, ErrorMessage = GlnError)]
        public string purchaseFrom { get; set; }

        [Display(Name = "Forward To (GLN) (413)")]
        [RegularExpression(GlnRegex, ErrorMessage = GlnError)]
        public string forwardTo { get; set; }

        [Display(Name = "Physical Location (GLN) (414)")]
        [RegularExpression(GlnRegex, ErrorMessage = GlnError)]
        public string physicalLoc { get; set; }

        [Display(Name = "Invoicing Party (GLN) (415)")]
        [RegularExpression(GlnRegex, ErrorMessage = GlnError)]
        public string invoicingParty { get; set; }

        [Display(Name = "Ship To Postal Code (420)")]
        [StringLength(20, ErrorMessage = "Postal Code cannot exceed 20 characters.")]
        public string shipToPost { get; set; }

        [Display(Name = "Ship To Postal Code with ISO (421)")]
        public string shipToPostWithIso { get; set; }

        // --- Country Codes (3 digits numeric) ---
        private const string CountryCodeRegex = @"^\d{3}$";
        private const string CountryCodeError = "Country code must be 3 digits (ISO 3166-1 numeric).";

        [Display(Name = "Country of Origin (422)")]
        [RegularExpression(CountryCodeRegex, ErrorMessage = CountryCodeError)]
        public string origin { get; set; }

        [Display(Name = "Initial Processing Country (423)")]
        [RegularExpression(CountryCodeRegex, ErrorMessage = CountryCodeError)]
        public string initialProcessingCountry { get; set; }

        [Display(Name = "Processing Country (424)")]
        [RegularExpression(CountryCodeRegex, ErrorMessage = CountryCodeError)]
        public string processingCountry { get; set; }

        [Display(Name = "Disassembly Country (425)")]
        [RegularExpression(CountryCodeRegex, ErrorMessage = CountryCodeError)]
        public string disassemblyCountry { get; set; }

        [Display(Name = "Full Process Country (426)")]
        [RegularExpression(CountryCodeRegex, ErrorMessage = CountryCodeError)]
        public string fullProcessCountry { get; set; }

        [Display(Name = "Expiration Date/Time (427)")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Expiration Date/Time must be in YYMMDDHHMM format (10 digits).")]
        public string expirationDateTime { get; set; }

        // --- Other Information ---
        [Display(Name = "Component/Part Structure (7001)")]
        [StringLength(30)]
        public string componentStructure { get; set; }

        [Display(Name = "Production URL (8001)")]
        [StringLength(200)]
        public string prodUrl { get; set; }

        [Display(Name = "Product Characteristics (8002)")]
        [StringLength(30)]
        public string prodCharacteristics { get; set; }

        [Display(Name = "Healthcare Class (8018)")]
        [StringLength(30)]
        public string healthcareClass { get; set; }

        [Display(Name = "Product Data Status (8020)")]
        [StringLength(30)]
        public string prodDataStatus { get; set; }

        [Display(Name = "Shelf Life Duration (days) (8100)")]
        [RegularExpression(@"^\d{1,6}$", ErrorMessage = "Shelf life duration must be up to 6 digits representing days.")]
        public string shelfLifeDuration { get; set; }
    }
}