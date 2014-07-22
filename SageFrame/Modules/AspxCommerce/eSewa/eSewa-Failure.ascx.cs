/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 20011-2012 by AspxCommerce
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;
using AspxCommerce.eSewa;
using SageFrame.Web.Utilities;

public partial class Modules_AspxCommerce_AspxPaymentSuccess_eSewaFailure : BaseAdministrationUserControl
{
    public string invoice, addressPath, sessionCode, pgid, couponCode, paymentStatus, responseCode, cultureName, MainCurrency;
    bool IsUseFriendlyUrls = true;
    public int orderID;
    public decimal amount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {

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

                Image imgProgress = (Image)UpdateProgress1.FindControl("imgPrgress");
                if (imgProgress != null)
                {
                    imgProgress.ImageUrl = GetTemplateImageUrl("ajax-loader.gif", true);
                }
                hlnkHomePage.NavigateUrl = sageRedirectPath;

                string transID = string.Empty;
                transID = GetTransactionDetailById(int.Parse(Session["OrderID"].ToString()));



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

                        string itemids = Session["OrderID"].ToString() + "#" + storeID + "#" + portalID + "#" + userName + "#" + customerID + "#" + sessionCode + "#" + Session["IsTesteSewa"].ToString() + "#" + Session["GateWay"].ToString();
                        couponCode = orderdata.ObjOrderDetails.CouponCode.ToString();
                        TransactionLogInfo tinfo = new TransactionLogInfo();
                        TransactionLog Tlog = new TransactionLog();
                        responseCode = "Transaction Failed";
                        paymentStatus = "Failed";
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
                    }
                    lblTransaction.Text = transID;
                    lblPaymentMethod.Text = "eSewa";
                    lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy ");
                    lblInvoice.Text = invoice;
                    lblPaymentStatus.Text = "Your eSewa payment transaction was unsuccessfull";
                    ClearAllSession();
                }                

                IncludeLanguageJS();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }

    public static string GetTransactionDetailById(int orderID)
    {
        var paraMeter = new List<KeyValuePair<string, object>>();
        paraMeter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
        var sqlH = new SQLHandler();
        return sqlH.ExecuteAsScalar<string>("usp_Aspx_GetTransactionDetailById", paraMeter);
    }
    private void ClearAllSession()
    {
        Session.Remove("OrderCollection");

        if (Session["IsFreeShipping"] != null && Session["IsFreeShipping"] != "")
        {
            HttpContext.Current.Session.Remove("IsFreeShipping");
        }
        if (Session["OrderID"] != null && Session["OrderID"] != "")
        {
            HttpContext.Current.Session.Remove("OrderID");
        }
        if (Session["Transaction_id"] != null && Session["Transaction_id"] != "")
        {
            HttpContext.Current.Session.Remove("Transaction_id");
        }
        if (Session["Invoice"] != null && Session["Invoice"] != "")
        {
            HttpContext.Current.Session.Remove("Invoice");
        }
        if (Session["DiscountAmount"] != null && Session["DiscountAmount"] != "")
        {
            HttpContext.Current.Session.Remove("DiscountAmount");
        }
        if (Session["CouponCode"] != null && Session["CouponCode"] != "")
        {
            HttpContext.Current.Session.Remove("CouponCode");
        }
        if (Session["CouponApplied"] != null && Session["CouponApplied"] != "")
        {
            HttpContext.Current.Session.Remove("CouponApplied");
        }
        if (Session["DiscountAll"] != null && Session["DiscountAll"] != "")
        {
            HttpContext.Current.Session.Remove("DiscountAll");
        }
        if (Session["TaxAll"] != null && Session["TaxAll"] != "")
        {
            HttpContext.Current.Session.Remove("TaxAll");
        }
        if (Session["ShippingCostAll"] != null && Session["ShippingCostAll"] != "")
        {
            HttpContext.Current.Session.Remove("ShippingCostAll");
        }
        if (Session["GrandTotalAll"] != null && Session["GrandTotalAll"] != "")
        {
            HttpContext.Current.Session.Remove("GrandTotalAll");
        }
        if (Session["Gateway"] != null && Session["Gateway"] != "")
        {
            HttpContext.Current.Session.Remove("Gateway");
        }

    }
}
