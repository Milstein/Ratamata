using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using AspxCommerce.Core;
using SageFrame.Framework;
using SageFrame.Web;
using AspxCommerce.eSewa;
public partial class Modules_AspxCommerce_eSewa_eSewaSuccessful : PageBase
{
    public string transID, invoice, addressPath, sessionCode, pgid, couponCode, paymentStatus, responseCode, cultureName, MainCurrency;
    bool IsUseFriendlyUrls = true;
    public int orderID;
    public decimal amount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Session["eSewaData"] != null)
                {
                    string[] data = Session["eSewaData"].ToString().Split('#');
                    couponCode = data[7];
                }

                string odrID = HttpContext.Current.Request.QueryString["oid"];
                string tamt = HttpContext.Current.Request.QueryString["amt"];
                transID = HttpContext.Current.Request.QueryString["refId"];

                orderID = Int32.Parse(odrID);
                amount = Convert.ToDecimal(tamt);

                SageFrameConfig sfConfig = new SageFrameConfig();
                IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
                string sageRedirectPath = string.Empty;
                if (IsUseFriendlyUrls)
                {
                    if (GetPortalID > 1)
                    {
                        sageRedirectPath = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                        addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/portal/" + GetPortalSEOName + "/";
                    }
                    else
                    {
                        sageRedirectPath = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                        addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
                    }
                }
                else
                {
                    sageRedirectPath = ResolveUrl("{~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
                }
                

                if (Session["OrderID"] != null)
                {
                    AspxCommerceWebService clSes = new AspxCommerceWebService();
                    int storeID = int.Parse(GetStoreID.ToString());
                    int portalID = int.Parse(GetPortalID.ToString());
                    string userName = GetUsername.ToString();
                    int customerID = int.Parse(GetCustomerID.ToString());

                    OrderDetailsCollection orderdata = new OrderDetailsCollection();
                    List<eSewaSettingInfo> setting;
                    if (HttpContext.Current.Session["OrderCollection"] != null)
                    {
                        orderdata = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                        invoice = orderdata.ObjOrderDetails.InvoiceNumber.ToString();                        
                        eSewaWCFService pw = new eSewaWCFService();
                        int i = int.Parse(orderdata.ObjOrderDetails.PaymentGatewayTypeID.ToString());
                        setting = pw.GetAlleSewaSetting(i, storeID, portalID);                        
                                               
                        sessionCode = orderdata.ObjOrderDetails.SessionCode.ToString();
                        pgid = orderdata.ObjOrderDetails.PaymentGatewayTypeID.ToString();

                        Session["Transaction_id"] = transID;
                        Session["OrderID"] = odrID;
                        Session["Invoice"] = invoice;
                        string itemidsWithVar = "";
                        foreach (var item in orderdata.LstOrderItemsInfo)
                        {
                            itemidsWithVar += item.ItemID + "&" + item.Quantity + "&" + orderdata.ObjOrderDetails.OrderID + "&" + item.Variants + ",";
                        }
                        string itemids = itemidsWithVar;
                        //couponCode = orderdata.ObjOrderDetails.CouponCode.ToString(); 

                        TransactionLogInfo tinfo = new TransactionLogInfo();
                        TransactionLog Tlog = new TransactionLog();
                        responseCode = "Transaction occured successfully";
                        paymentStatus = "Successful";
                        tinfo.TransactionID = transID;
                        tinfo.AuthCode = "";
                        tinfo.TotalAmount = amount;
                        tinfo.ResponseCode = responseCode;
                        tinfo.ResponseReasonText = paymentStatus.ToString();
                        tinfo.OrderID = orderID;
                        tinfo.StoreID = storeID;
                        tinfo.PortalID = portalID;
                        tinfo.AddedBy = userName;
                        tinfo.CustomerID = customerID;
                        tinfo.SessionCode = sessionCode;
                        tinfo.PaymentGatewayID = int.Parse(pgid);
                        tinfo.PaymentStatus = paymentStatus;
                        tinfo.PayerEmail = "";
                        tinfo.CreditCard = "";
                        tinfo.RecieverEmail = "";
                        Tlog.SaveTransactionLog(tinfo);
                                         
                        eSewaHandler.ParseIPN(orderID, transID, paymentStatus, storeID, portalID, userName, customerID, sessionCode);
                        eSewaHandler.UpdateItemQuantity(itemids, couponCode, storeID, portalID, userName);
                        CartManageSQLProvider cms = new CartManageSQLProvider();
                        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                        aspxCommonObj.CustomerID = customerID;
                        aspxCommonObj.SessionCode = sessionCode;
                        aspxCommonObj.StoreID = storeID;
                        aspxCommonObj.PortalID = portalID;
                        aspxCommonObj.CultureName = null;
                        aspxCommonObj.UserName = null;
                        cms.ClearCartAfterPayment(aspxCommonObj);
                        EmailTemplate.SendEmailForOrder(portalID, orderdata, addressPath, TemplateName, transID);
                        Response.Redirect("eSewa-Success.aspx",false);                                                        
                       
                    }
                }
                else
                {
                    Response.Redirect(sageRedirectPath, false);
                }               

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
   

}
