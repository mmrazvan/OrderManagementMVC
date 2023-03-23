using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using OrderManagementMVC.Models;

namespace OrderManagementMVC.Helpers
{
    public class PdfHelper
    {
        public byte[] CreateLabel(List<OrderLabelsModel> labels)
        {
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = new PdfWriter(memoryStream);
            PdfDocument pdfDocument = new PdfDocument(writer.SetSmartMode(true));
            Document d = new Document(pdfDocument, iText.Kernel.Geom.PageSize.LETTER);
            d.Add(new Paragraph("Hello world!"));
                        
            d.Close();

            return memoryStream.ToArray();
        }
    }
}
