using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using AspxCommerce.PesoPay;
using Com.Asiapay.Secure;
using SageFrame.Web;

public partial class Modules_AspxCommerce_PesoPay_processor : BaseAdministrationUserControl
{
    public string AspxPaymentModulePath;
    public int StoreID;
    public int PortalID;
    public int CustomerID;
    public string UserName;
    public string CultureName, MainCurrency;
    public string SessionCode = string.Empty;
    public string Spath, ItemIds, CouponCode;
    public double Rate;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["AppointmentData"] != null)
            {
                string[] data = Session["AppointmentData"].ToString().Split('#');
                StoreID = int.Parse(data[0]);
                PortalID = int.Parse(data[1]);
                UserName = data[2];
                CustomerID = int.Parse(data[3]);
                SessionCode = data[4];
                CultureName = data[5];
                Spath = ResolveUrl("~/Modules/AspxCommerce/AspxCommerceServices/");
                //AspxCommerceWebService aws = new AspxCommerceWebService();
                //rate = 1;// aws.GetCurrencyRate("INR", "USD");

                LoadSetting();
            }
            else
            {
                lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
                clickhere.Visible = false;
            }


        }
        catch (Exception ex)
        {
            lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
            clickhere.Visible = false;
            ProcessException(ex);
        }

    }


    private void LoadSetting()
    {



        var ph = new PesoPayHandler();
        string invoice = "";
        //var orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];

        try
        {
            PesoPaySettingInfo sf = ph.GetPesoPaySettings(StoreID, int.Parse(Session["GateWay"].ToString()), PortalID);
            Response.Clear();
            string ids = 1 + "#" + StoreID + "#" + PortalID + "#" + UserName + "#" +
                         CustomerID + "#" + SessionCode + "#" + sf.IsTestPesoPay + "#" + Session["GateWay"].ToString() +
                         "#" + "" + "#" + ""+"#true";//last added new for isAppointment
            string secureHash = "";
            var ssc = new StoreSettingConfig();
            MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, StoreID, PortalID, CultureName);
            string gateWayCurrency =
                GetMerchantSupportedCurrency(sf.PesoPayCurrencyCode.ToString(CultureInfo.InvariantCulture));

            if (gateWayCurrency.ToLower().Trim() == MainCurrency.ToLower().Trim())
            {
                Rate = 1;
            }
            else
            {
                var aws = new AspxCommerceWebService();
                Rate = aws.GetCurrencyRate(MainCurrency, gateWayCurrency.Trim());
            }
            double amount = double.Parse(Session["GrandTotalAll"].ToString()) * Rate;


            if (bool.Parse(sf.PesoPayIsSecureHashEnabled))
            {
                ids += "#" +
                       sf.PesoPayIsSecureHashEnabled + "#" + sf.PesoPaySecureHashSecret;
                var sha1 = new SHAPaydollarSecure();
                secureHash = sha1.generatePaymentSecureHash(
                    sf.PesoPayMerchantID.ToString(CultureInfo.InvariantCulture),
                   invoice, //::TODO add invoice no
                    sf.PesoPayCurrencyCode.ToString(CultureInfo.InvariantCulture),
                    Math.Round(amount, 2).ToString(CultureInfo.InvariantCulture), sf.PesoPayPayType,
                    sf.PesoPaySecureHashSecret);
            }
            else
            {
                ids += "#" + sf.PesoPayIsSecureHashEnabled;
            }

            // string sAdd;
            string postUrl;
            if (sf.PesoPayPaymentType == 1)
            {
                postUrl = bool.Parse(sf.IsTestPesoPay)
                              ? "https://test.pesopay.com/b2cDemo/eng/payment/payForm.jsp"
                              : "https://www.pesopay.com/b2c2/eng/payment/payForm.jsp";
                var sb = new StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat(@"<body onload='document.forms[""payForm""].submit()' >");
                sb.AppendFormat("<form  action='{0}' method='post' name='payForm' >", postUrl);
                sb.AppendFormat("<input type=\"hidden\" name=\"merchantId\"  value=\"" + sf.PesoPayMerchantID + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"orderRef\" value=\"" +
                              invoice+ "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"currCode\" value=\"" + sf.PesoPayCurrencyCode + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"payType\"  value=\"" + sf.PesoPayPayType + "\"  />");
                sb.AppendFormat("<input type=\"hidden\" name=\"payMethod\"  value='ALL' />"); //sf.PesoPayPaymentMethod
                sb.AppendFormat("<input type=\"hidden\" name=\"remark\" value=\"" + ids + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"redirect\"  value=\"" + sf.PesoPayRedirectTime + "\"  />");
                sb.AppendFormat("<input type=\"hidden\" name=\"mpsMode\"  value=\"NIL\"  />");
                sb.AppendFormat("<input type=\"hidden\" name=\"amount\"  value=\"" + Math.Round(amount, 2) + "\"  />");
                sb.AppendFormat("<input type=\"hidden\" name=\"lang\" value=\"" + sf.PesoPayLanguage + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"cancelUrl\" value=\"" + sf.PesoPayCancelURL + "\"  />");
                sb.AppendFormat("<input type=\"hidden\" name=\"failUrl\" value=\"" + sf.PesoPayErrorURL + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"successUrl\" value=\"" + sf.PesoPaySuccessURL + "\" />");
                if (bool.Parse(sf.PesoPayIsSecureHashEnabled))
                {
                    sb.AppendFormat("<input type=\"hidden\" name=\"secureHash\" value=\"" + secureHash + "\" />");

                }
                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");
                Response.Write(sb.ToString());
                // Server.Execute(sb.ToString());
                // Response.End(); //this wiil throw execption of ThreadAbortException  
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else if (sf.PesoPayPaymentType == 2)
            {
                postUrl = bool.Parse(sf.IsTestPesoPay)
                              ? "https://test.pesopay.com/b2cDemo/eng/dPayment/payComp.jsp"
                              : "https://www.pesopay.com/b2c2/eng/dPayment/payComp.jsp";
                var sb = new StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat(@"<body onload='document.forms[""payForm""].submit()' >");
                sb.AppendFormat("<form  action='{0}' method='post' name='payForm' >", postUrl);
                sb.AppendFormat("<input type=\"hidden\" name=\"merchantId\"  value=\"" + sf.PesoPayMerchantID + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"orderRef\" value=\"" +
                                invoice + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"currCode\" value=\"" + sf.PesoPayCurrencyCode + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"payType\"  value=\"" + sf.PesoPayPayType + "\"  />");
                sb.AppendFormat("<input type=\"hidden\" name=\"payMethod\"  value='ALL' />"); //sf.PesoPayPaymentMethod
                sb.AppendFormat("<input type=\"hidden\" name=\"remark\" value=\"" + ids + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"redirect\"  value=\"" + sf.PesoPayRedirectTime + "\"  />");
                sb.AppendFormat("<input type=\"hidden\" name=\"mpsMode\"  value=\"NIL\"  />");
                sb.AppendFormat("<input type=\"hidden\" name=\"amount\"  value=\"" + Math.Round(amount, 2) + "\"  />");
                sb.AppendFormat("<input type=\"hidden\" name=\"lang\" value=\"" + sf.PesoPayLanguage + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"cancelUrl\" value=\"" + sf.PesoPayCancelURL + "\"  />");
                sb.AppendFormat("<input type=\"hidden\" name=\"failUrl\" value=\"" + sf.PesoPayErrorURL + "\" />");
                sb.AppendFormat("<input type=\"hidden\" name=\"successUrl\" value=\"" + sf.PesoPaySuccessURL + "\" />");
                if (bool.Parse(sf.PesoPayIsSecureHashEnabled))
                {
                    sb.AppendFormat("<input type=\"hidden\" name=\"secureHash\" value=\"" + secureHash + "\" />");

                }
                if (sf.PesoPayPaymentType == 2)
                {
                    //FOR CARD TYPE  

                    //sb.AppendFormat("<input type=\"hidden\" name=\"pMethod\"  value=\"" +
                    //                orderdata2.ObjPaymentInfo.CardType + "\"  />"); //eg. Visa ,Masters
                    //sb.AppendFormat("<input type=\"hidden\" name=\"epMonth\" value=\"" +
                    //                orderdata2.ObjPaymentInfo.ExpireMonth + "\" />");
                    //sb.AppendFormat("<input type=\"hidden\" name=\"epYear\" value=\"" +
                    //                orderdata2.ObjPaymentInfo.ExpireYear + "\" />");
                    //sb.AppendFormat("<input type=\"hidden\" name=\"cardNo\" value=\"" +
                    //                orderdata2.ObjPaymentInfo.CardNumber + "\" />");
                    //sb.AppendFormat("<input type=\"hidden\" name=\"securityCode\" value=\"" +
                    //                orderdata2.ObjPaymentInfo.CardCode + "\" />");
                    //sb.AppendFormat("<input type=\"hidden\" name=\"cardHolder\" value=\"" +
                    //                orderdata2.ObjPaymentInfo.CardHolder + "\" />");
                }

                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");
                Response.Write(sb.ToString());
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }

        }
        catch (Exception ex)
        {
            lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
            clickhere.Visible = false;
            ProcessException(ex);
        }

    }

    protected void clickhere_Click(object sender, EventArgs e)
    {
        if (Session["AppointmentData"] != null)
        {
            LoadSetting();
        }
    }

    private string GetMerchantSupportedCurrency(string curCode)
    {
        var dict = new Dictionary<string, string>
                       {
                           {"PHP", "608"},
                           {"USD", "344"},
                           {"SGD", "702"},
                           {"CNY(RMB)", "156"},
                           {"JPY", "392"},
                           {"TWD", "901"},
                           {"AUD", "036"},
                           {"EUR", "978"},
                           {"GBP", "826"},
                           {"CAD", "124"},
                           {"MOP", "446"},
                           {"THB", "764"},
                           {"MYR", "458"},
                           {"IDR", "360"},
                           {"KRW", "410"},
                           {"SAR", "682"},
                           {"NZD", "554"},
                           {"AED", "784"},
                           {"BND", "096"}
                       };

        string currencycode = string.Empty;
        foreach (KeyValuePair<string, string> pair in dict)
        {
            if (pair.Value == curCode)
            {
                currencycode = pair.Key;
            }
        }
        return currencycode;

    }
}
