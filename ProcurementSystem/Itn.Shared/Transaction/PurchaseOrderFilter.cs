using System.Collections.Specialized;
using Itn.Utilities;

namespace Itn.Shared.Transaction
{
    public class PurchaseOrderFilter : SearchFilterBase
    {
        public string Email { get; set; }
        public string Status { get; set; }
        public string PoNbr { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            var nvc = new NameValueCollection();
            nvc.AddIfNotEmpty("email", Email);
            nvc.AddIfNotEmpty("status", Status);
            nvc.AddIfNotEmpty("ponbr", PoNbr);
            return nvc;
        }
    }
}
