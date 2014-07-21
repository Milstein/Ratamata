<%@ WebService Language="C#" Class="DragonPayWebService" %>

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
//using Com.Asiapay.Secure;
using Org.BouncyCastle.Asn1.Ocsp;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using AspxCommerce.DragonPay;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class DragonPayWebService  : System.Web.Services.WebService {

    [WebMethod]
    public List<DragonPaySettingInfo> GetDragonPaySettings(int paymentGatewayId, int storeId, int portalId)
    {
        try
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayId));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            SQLHandler sqLH = new SQLHandler();
            return sqLH.ExecuteAsList<DragonPaySettingInfo>("usp_aspx_GetDragonPaySettingAll", parameterCollection);            
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    
    
    
    
    [WebMethod]
    public List<CartInfoDragonPay> GetCartDetails(int storeId, int portalId, int customerId, string userName, string cultureName, string sessionCode)
    {
       var paraMeter = new List<KeyValuePair<string, object>>();
        paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
        paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
        paraMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerId));
        paraMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
        paraMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        paraMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
        SQLHandler sqLH = new SQLHandler();
        return sqLH.ExecuteAsList<CartInfoDragonPay>("usp_Aspx_GetCartDetails", paraMeter);
    }


    
    
}

