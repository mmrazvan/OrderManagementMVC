using iText.Barcodes;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

using OrderManagementMVC.Models;

using Image = iText.Layout.Element.Image;

namespace OrderManagementMVC.Helpers
{
    public static class PdfHelper
    {
        public static byte[] CreateLabel(OrdersModel order, List<OrderLabelsModel> labels, LabelsModel boxLabel)
        {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = new PdfWriter(memoryStream);
            PdfDocument pdfDocument = new PdfDocument(writer.SetSmartMode(true));
            float width = MmToPoints(boxLabel.Width);
            float height = MmToPoints(boxLabel.Heigth);
            float fontSize = boxLabel.Heigth / 5;
            PageSize pageSize = new PageSize(width, height);
            Document d = new Document(pdfDocument, pageSize);
            d.SetMargins(0f, 0f, 0f, 0f);
            foreach (var item in labels)
            {
                Table t = new Table(1).SetWidth(width).SetHeight(height).SetTextAlignment(TextAlignment.CENTER);

                t.AddCell(new Cell().SetBorder(Border.NO_BORDER).Add(CustomParagraph($"{order.Client}", bold, fontSize + 2)));
                t.AddCell(new Cell().SetBorder(Border.NO_BORDER).Add(CustomParagraph($"{order.DocumentName} {fontSize}", font, fontSize)));
                t.AddCell(new Cell().SetBorder(Border.NO_BORDER).Add(CustomParagraph($"Box nr. {item.BoxNumber} Quantity: {item.Quantity}", bold, fontSize)));
                t.AddCell(new Cell().SetBorder(Border.NO_BORDER).Add(CustomParagraph($"from {item.StartIndex} to {item.StopIndex}", bold, fontSize)));
                t.AddCell(Barcode(item.IdBoxNumber, pdfDocument, fontSize/5/4));
                d.Add(t);
                d.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));                
            }            
                        
            d.Close();

            return memoryStream.ToArray();
        }

        private static Paragraph CustomParagraph(string text, PdfFont font, float fontsize)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Add(text).SetFont(font).SetFontSize(fontsize);
            return paragraph;
        }

        private static Cell Barcode(string barcodeText, PdfDocument pdfDocument, float scale)
        {
            Barcode128 barcode = new Barcode128(pdfDocument);
            barcode.SetCode(barcodeText);
            PdfFormXObject barcodeObject = barcode.CreateFormXObject(pdfDocument);
            Image barcodeImage = new Image(barcodeObject);
            barcodeImage.Scale(scale , scale);
            barcodeImage.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            return new Cell().SetBorder(Border.NO_BORDER).SetHorizontalAlignment(HorizontalAlignment.CENTER).Add(barcodeImage);
        }

        private static float MmToPoints(float value)
        {
            return value * (float)2.83465;
        }
    }
}
