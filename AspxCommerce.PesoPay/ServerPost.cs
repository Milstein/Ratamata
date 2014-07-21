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
using System.Text;
using System.IO;

namespace Com.Asiapay.Secure
{
    public class ServerPost
    {
        public string post(string ip_postData, string ip_pageUrl)
        {
            ServicePointManager.CertificatePolicy = new CertPolicy();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ip_pageUrl);
            request.CookieContainer = new CookieContainer();
            request.Method = "POST";
            request.Accept = "*/*";
            request.ContentType="application/x-www-form-urlencoded";
            byte[] buffer = this.encoding.GetBytes(ip_postData);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), encoding);
            this.respHtml = reader.ReadToEnd();
            foreach (System.Net.Cookie ck in response.Cookies)
            {
                this.cookie += ck.Name + "=" + ck.Value + ";";
            }
            reader.Close();
            return respHtml;
        }

        public string RespHtml
        {
            get { return respHtml; }
        }

        public string Cookie
        {
            set { cookie = value; }
            get { return cookie; }
        }

        public Encoding Encoding
        {
            set { encoding = value; }
            get { return encoding; }
        }

        private string respHtml = "";
        private string cookie = "";
        private Encoding encoding = Encoding.Default;


    }
}
