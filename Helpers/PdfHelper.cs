using System.Drawing;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using OrderManagementMVC.Models;

namespace OrderManagementMVC.Helpers
{
    public class PdfHelper
    {
        public byte[] CreateLabel(OrdersModel order, List<OrderLabelsModel> labels, LabelsModel boxLabel)
        {
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = new PdfWriter(memoryStream);
            PdfDocument pdfDocument = new PdfDocument(writer.SetSmartMode(true));
            Document d = new Document(pdfDocument, new iText.Kernel.Geom.PageSize(MmToPoints(boxLabel.Width), MmToPoints(boxLabel.Heigth)));
            d.SetMargins(0f, 0f, 0f, 0f);
            foreach (var item in labels)
            {
                Table t = new Table(1).SetWidth(boxLabel.Width).SetHeight(boxLabel.Heigth);
                t.AddCell(new Cell().Add(new Paragraph($"{order.Client}")));
                
                
            }            
                        
            d.Close();

            return memoryStream.ToArray();
        }

        private float MmToPoints(float value)
        {
            Console.WriteLine(value * (float)2.83465);
            return value * (float)2.83465;
        }
    }
}
