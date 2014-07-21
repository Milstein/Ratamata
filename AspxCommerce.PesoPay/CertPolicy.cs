using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Com.Asiapay.Secure
{
    public class CertPolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvpt, X509Certificate cert, WebRequest req, int certprb)
        { return true; }
    }
}
