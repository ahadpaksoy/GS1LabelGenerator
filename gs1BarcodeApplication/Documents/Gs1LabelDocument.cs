using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace gs1BarcodeApplication.Documents
{
    public class Gs1LabelDocument : IDocument
    {
        public string Gs1String { get; }
        public byte[] BarcodeBytes { get; }
        public byte[] QrCodeBytes { get; }

        public Gs1LabelDocument(string gs1String, byte[] barcodeBytes, byte[] qrCodeBytes)
        {
            Gs1String = gs1String;
            BarcodeBytes = barcodeBytes;
            QrCodeBytes = qrCodeBytes;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A6);
                page.Margin(25, Unit.Point);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Helvetica"));

                page.Content()
                    .Column(column =>
                    {
                        column.Item().AlignCenter().Text("Generated GS1 Label")
                            .SemiBold().FontSize(14);

                        column.Item().PaddingVertical(10);

                        if (BarcodeBytes?.Length > 0)
                        {
                            column.Item().AlignCenter().Image(BarcodeBytes)
                                .FitWidth();
                        }

                        column.Item().PaddingVertical(10);

                        column.Item().Text("GS1 Data String:");
                        column.Item().Border(1).Padding(5)
                            .Text(Gs1String)
                            .FontFamily("Courier New").FontSize(9);

                        column.Item().PaddingVertical(10);

                        if (QrCodeBytes?.Length > 0)
                        {
                            column.Item().AlignCenter().Column(qrColumn =>
                            {
                                qrColumn.Item().Shrink() // Renamed method
                                    .MaxHeight(100)
                                    .MaxWidth(100)
                                    .Image(QrCodeBytes);
                            });
                        }
                    });
            });
        }
    }
}