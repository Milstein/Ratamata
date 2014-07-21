﻿using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Collections.Generic;
using SageFrame.Web.Utilities;
using AspxCommerce.TPSL;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class TPSLWCFService
{
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]    
    public List<CartInfoforTPSL> GetCartDetails(int storeID, int portalID, int customerID, string userName, string cultureName, string sessionCode)
    {
        List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
        ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
        ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
        ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
        ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
        ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
        ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
        SQLHandler sqLH = new SQLHandler();
        return sqLH.ExecuteAsList<CartInfoforTPSL>("usp_Aspx_GetCartDetails", ParaMeter);
    }

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    public List<TPSLSettingInfo> GetAllTPSLSetting(int paymentGatewayID, int storeId, int portalId)
    {
        try
        {
            List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
            parameterCollection.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", paymentGatewayID));
            parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            SQLHandler sqLH = new SQLHandler();
            return sqLH.ExecuteAsList<TPSLSettingInfo>("usp_Aspx_TPSLSettingsGetAll", parameterCollection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
