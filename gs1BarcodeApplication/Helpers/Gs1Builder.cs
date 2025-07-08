using System.Collections.Generic;
using gs1BarcodeApplication.Models;

namespace gs1BarcodeApplication.Helpers
{
    public static class Gs1Builder
    {
        /// <summary>
        /// Creates a GS1 data string from a ProductLabelModel.
        /// This is the single source of truth for GS1 string generation.
        /// </summary>
        /// <param name="model">The model containing the label data.</param>
        /// <returns>A concatenated GS1 data string.</returns>
        public static string BuildGs1Data(ProductLabelModel model)
        {
            var parts = new List<string>();

            // Helper function to keep the code clean
            void AddIfNotEmpty(string ai, string value)
            {
                if (!string.IsNullOrWhiteSpace(value))
                    parts.Add($"({ai}){value}");
            }

            // --- Core Identifiers ---
            AddIfNotEmpty("00", model.sscc);
            AddIfNotEmpty("01", model.GTIN);
            AddIfNotEmpty("02", model.gtinContained);
            AddIfNotEmpty("10", model.batch_lotNumber);
            AddIfNotEmpty("11", model.productionDate);
            AddIfNotEmpty("12", model.dueDate);
            AddIfNotEmpty("13", model.packDate);
            AddIfNotEmpty("15", model.bestBeforeDate);
            AddIfNotEmpty("17", model.expirationDate);
            AddIfNotEmpty("20", model.productVariant);
            AddIfNotEmpty("21", model.serialNumber);
            AddIfNotEmpty("22", model.healthCareGTIN);
            AddIfNotEmpty("241", model.pharmaLotNumber);
            AddIfNotEmpty("242", model.additionalProductID);
            AddIfNotEmpty("243", model.customerPartNumber);
            AddIfNotEmpty("250", model.madeToOrderVariation);
            AddIfNotEmpty("251", model.secondarySerialNumber);
            AddIfNotEmpty("253", model.gdti);
            AddIfNotEmpty("255", model.gcn);
            AddIfNotEmpty("30", model.variableCount);
            AddIfNotEmpty("37", model.countContained);

            // --- Measurements (Net, Metric) ---
            AddIfNotEmpty("3102", model.netWeightKg);
            AddIfNotEmpty("3112", model.lengthM);
            AddIfNotEmpty("3122", model.widthM);
            AddIfNotEmpty("3132", model.depthM);
            AddIfNotEmpty("3142", model.areaSqM);
            AddIfNotEmpty("3152", model.netVolumeL);
            AddIfNotEmpty("3162", model.netVolumeM3);

            // --- Measurements (Net, Imperial) ---
            AddIfNotEmpty("3202", model.netWeightLb);
            AddIfNotEmpty("3212", model.lengthIn);
            AddIfNotEmpty("3222", model.widthIn);
            AddIfNotEmpty("3232", model.depthIn);
            AddIfNotEmpty("3242", model.areaSqIn);
            AddIfNotEmpty("3252", model.netVolumeUSGal);
            AddIfNotEmpty("3262", model.netVolumeCubicIn);

            // --- Measurements (Gross, Metric) ---
            AddIfNotEmpty("3302", model.grossWeightKg);
            AddIfNotEmpty("3312", model.lengthGrossM);
            AddIfNotEmpty("3322", model.widthGrossM);
            AddIfNotEmpty("3332", model.depthGrossM);
            AddIfNotEmpty("3342", model.areaGrossSqM);
            AddIfNotEmpty("3352", model.grossVolumeL);
            AddIfNotEmpty("3362", model.grossVolumeM3);

            // --- Measurements (Gross, Imperial) ---
            AddIfNotEmpty("3402", model.grossWeightLb);
            AddIfNotEmpty("3412", model.lengthGrossIn);
            AddIfNotEmpty("3422", model.widthGrossIn);
            AddIfNotEmpty("3432", model.depthGrossIn);
            AddIfNotEmpty("3442", model.areaGrossSqIn);
            AddIfNotEmpty("3452", model.grossVolumeUSGal);
            AddIfNotEmpty("3462", model.grossVolumeCubicIn);

            // --- Additional Measurements ---
            AddIfNotEmpty("3472", model.areaSqFt);
            AddIfNotEmpty("3482", model.lengthFt);
            AddIfNotEmpty("3492", model.widthFt);
            AddIfNotEmpty("3502", model.depthFt);
            AddIfNotEmpty("3512", model.grossWeightMetricTon);
            AddIfNotEmpty("3522", model.netWeightMetricTon);
            AddIfNotEmpty("3532", model.netVolumeCubicFt);
            AddIfNotEmpty("3542", model.netVolumeQt);
            AddIfNotEmpty("3552", model.netWeightOz);
            AddIfNotEmpty("3562", model.netVolumeUSFlOz);
            AddIfNotEmpty("3572", model.netVolumeImpGal);
            AddIfNotEmpty("3582", model.netVolumeImpFlOz);
            AddIfNotEmpty("3592", model.netVolumeCubicYd);
            AddIfNotEmpty("3602", model.netVolumeUSPint);
            AddIfNotEmpty("3612", model.netVolumeUSQt);
            AddIfNotEmpty("3622", model.netVolumeImpPint);
            AddIfNotEmpty("3632", model.netVolumeImpQt);
            AddIfNotEmpty("3642", model.netVolumeLitPer100Kg);

            // --- Amounts and Consumer Units ---
            AddIfNotEmpty("3900", model.numConsumerUnits);
            AddIfNotEmpty("3902", model.amountPayable);
            AddIfNotEmpty("3903", model.amountPayableISO);
            AddIfNotEmpty("3912", model.amountPayableVar);
            AddIfNotEmpty("3913", model.amountPayableVarISO);

            // --- Logistics and Party Information ---
            AddIfNotEmpty("400", model.customerPONumber);
            AddIfNotEmpty("401", model.consignmentNumber);
            AddIfNotEmpty("402", model.gsin);
            AddIfNotEmpty("403", model.routingCode);
            AddIfNotEmpty("410", model.shipTo);
            AddIfNotEmpty("411", model.billTo);
            AddIfNotEmpty("412", model.purchaseFrom);
            AddIfNotEmpty("413", model.forwardTo);
            AddIfNotEmpty("414", model.physicalLoc);
            AddIfNotEmpty("415", model.invoicingParty);
            AddIfNotEmpty("420", model.shipToPost);
            AddIfNotEmpty("421", model.shipToPostWithIso);
            AddIfNotEmpty("422", model.origin);
            AddIfNotEmpty("423", model.initialProcessingCountry);
            AddIfNotEmpty("424", model.processingCountry);
            AddIfNotEmpty("425", model.disassemblyCountry);
            AddIfNotEmpty("426", model.fullProcessCountry);
            AddIfNotEmpty("427", model.expirationDateTime);

            // --- Additional Information ---
            AddIfNotEmpty("7001", model.componentStructure);
            AddIfNotEmpty("8001", model.prodUrl);
            AddIfNotEmpty("8002", model.prodCharacteristics);
            AddIfNotEmpty("8018", model.healthcareClass);
            AddIfNotEmpty("8020", model.prodDataStatus);
            AddIfNotEmpty("8100", model.shelfLifeDuration);

            return string.Join("", parts);
        }
    }
}