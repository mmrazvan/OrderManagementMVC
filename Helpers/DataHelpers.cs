using OrderManagementMVC.Models;

namespace OrderManagementMVC.Helpers
{
    public static class DataHelpers
    {
        public static List<OrderLabelsModel> CreateLabels(OrdersModel order)
        {
            int totalCant = order.Quantity;
            int split = LabelManipulation.LabelsPerBox(order.PagesOnEnvelope, Enum.Parse<DocumentFormats.DocFormats>(order.DocumentFormat));
            int dela = 1;
            int panala = dela + split - 1;
            int nrCutie = 1;

            List<OrderLabelsModel> labels = new List<OrderLabelsModel>();

            while (panala <= totalCant)
            {
                OrderLabelsModel label = new OrderLabelsModel
                {
                    OrderNumber = order.OrderNumber,
                    BoxNumber = nrCutie,
                    IdBoxNumber = GetIdBoxNumber(order.OrderNumber, nrCutie),
                    Quantity = split,
                    StartIndex = dela,
                    StopIndex = panala
                };
                nrCutie++;
                dela += split;
                panala += split;
                labels.Add(label);
            }
            if (totalCant - dela + 1 != 0)
            {
                OrderLabelsModel label = new OrderLabelsModel()
                {
                    OrderNumber = order.OrderNumber,
                    BoxNumber = nrCutie,
                    IdBoxNumber = GetIdBoxNumber(order.OrderNumber, nrCutie),
                    Quantity = totalCant - dela + 1,
                    StartIndex = dela,
                    StopIndex = totalCant
                };

                labels.Add(label);
            }
            return labels;
        }

        public static List<OrderTraceModel> CreateTraces(List<OrderLabelsModel> labels)
        {
            var traces = new List<OrderTraceModel>();
            foreach (var label in labels)
            {
                OrderTraceModel trace = new OrderTraceModel
                {
                    IdBoxNumber = label.IdBoxNumber,
                    OrderNumber = label.OrderNumber
                };
                traces.Add(trace);
            }
            return traces;
        }
        public static string GetIdBoxNumber(int orderNumber, int boxNumber)
        {
            return orderNumber.ToString().PadLeft(6, '0') + boxNumber.ToString().PadLeft(4, '0');
        }
    }
}
