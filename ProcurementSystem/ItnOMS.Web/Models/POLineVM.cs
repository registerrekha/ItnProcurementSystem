

namespace ItnOMS.Web.Models
{
    public class POLineVM
    {
        public int LineNbr { get; set; }
        public string ItemNbr { get; set; }
        public string  Qty { get; set; }

        public string Desc { get; set; }

        public static POLineVM Create()
        {
            return  new POLineVM();
        }
    }
}