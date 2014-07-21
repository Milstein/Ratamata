<%@ WebService Language="C#" Class="PesoPayWebService" %>

using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using AspxCommerce.Core;
using AspxCommerce.PayPal;
using AspxCommerce.PesoPay;
using Com.Asiapay.Secure;
using Org.BouncyCastle.Asn1.Ocsp;
using SageFrame.Web;
using SageFrame.Web.Utilities;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class PesoPayWebService  : System.Web.Services.WebService {

    [WebMethod]
    public PesoPaySettingInfo GetPesoPaySettings(int storeId, int paymentGatewayId, int portalId)
    {
        try
        {
           var param = new List<KeyValuePair<string, object>>();
            param.Add(new KeyValuePair<string, object>("@PaymentGateWayTypeID", paymentGatewayId));
            param.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            param.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            var sqLH = new SQLHandler();
            return sqLH.ExecuteAsObject<PesoPaySettingInfo>("usp_aspx_GetPesoPaySettingAll", param);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<CartInfoPesoPay> GetCartDetails(int storeId, int portalId, int customerId, string userName, string cultureName, string sessionCode)
    {
       var paraMeter = new List<KeyValuePair<string, object>>();
        paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
        paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
        paraMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerId));
        paraMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
        paraMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        paraMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
        SQLHandler sqLH = new SQLHandler();
        return sqLH.ExecuteAsList<CartInfoPesoPay>("usp_Aspx_GetCartDetails", paraMeter);
    }


    [WebMethod]
    public DirectServerPostInfo DirectServerPost(OrderDetailsCollection orderDetails)
    {
        var setting = GetPesoPaySettings(orderDetails.ObjCommonInfo.StoreID, orderDetails.ObjOrderDetails.PaymentGatewayTypeID, orderDetails.ObjCommonInfo.PortalID);
        
        string postUrl = "";

        if (setting.PesoPayPaymentType == 1)
        {
            postUrl = bool.Parse(setting.IsTestPesoPay) ? "https://test.pesopay.com/b2cDemo/eng/dPayment/payComp.jsp" : "https://www.pesopay.com/b2c2/eng/dPayment/payComp.jsp";
        }
        else
        {
            postUrl = bool.Parse(setting.IsTestPesoPay)
                          ? "https://test.pesopay.com/b2cDemo/eng/directPay/payComp.jsp"
                          : "https://www.pesopay.com/b2c2/eng/directPay/payComp.jsp";
        }
        var dataToPost = new StringBuilder();
        dataToPost.AppendFormat("?merchantId={0}", setting.PesoPayMerchantID);
        dataToPost.AppendFormat("&orderRef={0}", orderDetails.ObjOrderDetails.InvoiceNumber.Trim());

        dataToPost.AppendFormat("&amount={0}", orderDetails.ObjOrderDetails.GrandTotal);

        dataToPost.AppendFormat("&currCode={0}", setting.PesoPayCurrencyCode);

         dataToPost.AppendFormat("&pMethod={0}", orderDetails.ObjPaymentInfo.CardType.Trim());

         dataToPost.AppendFormat("&epMonth={0}", orderDetails.ObjPaymentInfo.ExpireMonth.Trim());

         dataToPost.AppendFormat("&epYear={0}", orderDetails.ObjPaymentInfo.ExpireYear.Trim());

         dataToPost.AppendFormat("&cardNo={0}", orderDetails.ObjPaymentInfo.CardNumber.Trim());
         dataToPost.AppendFormat("&cardHolder={0}", orderDetails.ObjPaymentInfo.CardHolder.Trim());
         dataToPost.AppendFormat("&securityCode={0}", orderDetails.ObjPaymentInfo.CardCode.Trim());
            dataToPost.AppendFormat("&payType={0}", setting.PesoPayPayType);

            if (bool.Parse(setting.PesoPayIsSecureHashEnabled))
            {
                var sha1 = new SHAPaydollarSecure();
              string  secureHash = sha1.generatePaymentSecureHash(
                    setting.PesoPayMerchantID.ToString(CultureInfo.InvariantCulture),
                    orderDetails.ObjOrderDetails.InvoiceNumber,
                    setting.PesoPayCurrencyCode.ToString(CultureInfo.InvariantCulture),
                    Math.Round(orderDetails.ObjOrderDetails.GrandTotal, 2).ToString(CultureInfo.InvariantCulture), setting.PesoPayPayType,
                    setting.PesoPaySecureHashSecret);
                dataToPost.AppendFormat("&secureHash={0}", secureHash);
            }
         
           // dataToPost.AppendFormat("&remark={0}", setting.PesoPayRemark);

        postUrl = postUrl + dataToPost;

        var req = (HttpWebRequest)WebRequest.Create(postUrl);
        //Set values for the request back
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        byte[] param = HttpContext.Current.Request.BinaryRead(HttpContext.Current.Request.ContentLength);
        string strRequest = Encoding.ASCII.GetString(param);

        //  strRequest += "&cmd=_notify-validate";
        req.ContentLength = strRequest.Length;
        var streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
        streamOut.Write(strRequest);
        streamOut.Close();
        var streamIn = new StreamReader(req.GetResponse().GetResponseStream());
        string strResponse = streamIn.ReadToEnd();
        streamIn.Close();
        String[] stringArray = strResponse.Split('&');
        int i;
        var posResponse = new DirectServerPostInfo();
        for (i =0; i < stringArray.Length; i++)
        {
            String[] stringArray1 = stringArray[i].Split('=');

            String sKey = stringArray1[0];
            String sValue = HttpUtility.UrlDecode(stringArray1[1]);

            // set string vars to hold variable names using a switch
            switch (sKey)
            {
                case "successcode":
                    posResponse.SuccessCode = Convert.ToString(sValue);
                    break;

                case "Ref":
                    posResponse.Ref = Convert.ToString(sValue);
                    break;

                case "PayRef":
                    posResponse.PayRef = Convert.ToString(sValue);
                    break;

                case "Amt":
                    posResponse.Amount = Convert.ToString(sValue);
                    break;

                case "Cur":
                    posResponse.CurrencyCode = Convert.ToString(sValue);
                    break;
                case "prc":
                    posResponse.Prc = Convert.ToString(sValue);
                    break;
                case "src":
                    posResponse.Src = Convert.ToString(sValue);
                    break;
                case "Ord":
                    posResponse.Ord = Convert.ToString(sValue);
                    break;
                case "Holder":
                    posResponse.Holder = Convert.ToString(sValue);
                    break;
                case "AuthId":
                    posResponse.AuthId = Convert.ToString(sValue);
                    break;
                case "TxTime":
                    posResponse.TxTime = Convert.ToString(sValue);
                    break;
                case "errMsg":
                    posResponse.ErrMsg = Convert.ToString(sValue);
                    break;


            }
        }
        return posResponse;
    }
    
    
}

