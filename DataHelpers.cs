using OrderManagementMVC.Models;
using OrderManagementMVC.Repositories;

namespace OrderManagementMVC
{
    public class DataHelpers
    {
        public IEnumerable<OrderLabelsModel> CreateLabels(OrdersModel order)
        {
            int start = 1;
            int stop = order.Quantity;
            int split = LabelManipulation.LabelsPerBox(order.PagesOnEnvelope, Enum.Parse<DocumentFormats.DocFormats>(order.DocumentFormat));
            int nrCutie = 1;

            List<OrderLabelsModel> labels = new List<OrderLabelsModel>();

            while (start <= stop)
            {
                OrderLabelsModel label = new OrderLabelsModel
                {
                    OrderNumber = order.OrderNumber,
                    BoxNumber = nrCutie,
                    IdBoxNumber = GetIdBoxNumber(order.OrderNumber, nrCutie),
                    Quantity = split,
                    StartIndex = start,
                    StopIndex = start + split - 1
                };
                start = start + split;
                nrCutie++;
                labels.Add(label);
            }
            if (stop - start +1 != 0)
            {
                OrderLabelsModel label = new OrderLabelsModel();
                labels.Add(label);
            }
            return labels;
        }
        public static string GetIdBoxNumber(int orderNumber, int boxNumber)
        {
            return orderNumber.ToString().PadLeft(6,'0') + boxNumber.ToString().PadLeft(4,'0');
        }
    }
}
