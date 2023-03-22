namespace OrderManagementMVC
{
    public static class LabelManipulation
    {
        public static int LabelsPerBox(int pagesOnEnvelope, DocumentFormats.DocFormats docFormat)
        {
            switch (docFormat)
            {
                case DocumentFormats.DocFormats.A6:
                switch (pagesOnEnvelope)
                {
                    case 1:
                    return 800;
                    case 2:
                    return 600;
                    case 3:
                    return 500;
                    case 4:
                    return 400;
                    case 5:
                    return 350;
                    case 6:
                    return 300;
                }
                break;
                case DocumentFormats.DocFormats.A5:
                switch (pagesOnEnvelope)
                {
                    case 1:
                    return 450;
                    case 2:
                    return 400;
                    case 3:
                    return 350;
                    case 4:
                    return 300;
                    case 5:
                    return 250;
                    case 6:
                    return 200;
                }
                break;
                case DocumentFormats.DocFormats.A4:
                switch (pagesOnEnvelope)
                {
                    case 1:
                    return 600;
                    case 2:
                    return 400;
                    case 3:
                    return 350;
                    case 4:
                    return 300;
                    case 5:
                    return 250;
                    case 6:
                    return 200;
                }
                break;
            }
            return 0; 
        }
    }
}
