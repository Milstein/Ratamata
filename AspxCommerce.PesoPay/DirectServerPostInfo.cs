using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspxCommerce.PesoPay
{
    public class DirectServerPostInfo
    {

        public string ErrMsg { get; set; }
        public string SuccessCode { get; set; }
        public string Ref { get; set; }
        public string PayRef { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Prc { get; set; }
        public string Src { get; set; }
        public string Ord { get; set; }
        public string Holder { get; set; }
        public string AuthId { get; set; }
        public string TxTime { get; set; }

    }
}
