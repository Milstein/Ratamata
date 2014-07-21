using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;
using AspxCommerce.PesoPay;
using SageFrame.Web.Utilities;

public partial class Modules_AspxCommerce_PesoPay_PesoPaySuccess : BaseAdministrationUserControl
{
    private string _invoice, _addressPath;
    bool _isUseFriendlyUrls = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                var sfConfig = new SageFrameConfig();
                _isUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
                string sageRedirectPath = string.Empty;
                if (_isUseFriendlyUrls)
                {
                    if (GetPortalID > 1)
                    {
                        sageRedirectPath =
                            ResolveUrl("~/portal/" + GetPortalSEOName + "/" +
                                       sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                        _addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/portal/" + GetPortalSEOName + "/";
                    }
                    else
                    {
                        sageRedirectPath =
                            ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) +
                                       ".aspx");
                        _addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
                
                    }
                }
                else
                {
                    sageRedirectPath =
                        ResolveUrl("{~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" +
                                   sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
                }

                var imgProgress = (Image) UpdateProgress1.FindControl("imgPrgress");
                if (imgProgress != null)
                {
                    imgProgress.ImageUrl = GetTemplateImageUrl("ajax-loader.gif", true);
                }
                hlnkHomePage.NavigateUrl = sageRedirectPath;

                if (Session["OrderID"] != null)
                {
                    try
                    {

                        int orderId = int.Parse(Session["OrderID"].ToString());
                        int storeId = GetStoreID;
                        int portalId = GetPortalID;
                        string userName = GetUsername;
                        int customerId = GetCustomerID;
                        _invoice = Request.QueryString["Ref"];

                        var orderdata = new OrderDetailsCollection();

                        if (HttpContext.Current.Session["OrderCollection"] != null)
                        {

                            orderdata = (OrderDetailsCollection) HttpContext.Current.Session["OrderCollection"];
                            if (string.IsNullOrEmpty(_invoice))
                            {
                                _invoice = orderdata.ObjOrderDetails.InvoiceNumber;
                            }
                        }

                        string transactionid = GetTransactioinId(_invoice, orderdata.ObjCommonInfo.StoreID,
                                                                 orderdata.ObjCommonInfo.PortalID);
                        GetOrderItemDetails(orderId, orderdata.ObjCommonInfo.StoreID, orderdata.ObjCommonInfo.PortalID,
                                            orderdata.ObjCommonInfo.CultureName, orderdata.ObjCommonInfo.AddedBy);
                      
                        lblTransaction.Text = transactionid;
                        lblInvoice.Text = _invoice;
                        lblPaymentMethod.Text = "PesoPay";
                        lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy ");
                        var objHandler = new PesoPayHandler();
                        objHandler.ClearCartAfterPayment(customerId, HttpContext.Current.Session.SessionID, storeId,
                                                         portalId);
                        if (HttpContext.Current.Session["OrderCollection"] != null)
                        {
                            EmailTemplate.SendEmailForOrder(GetPortalID, orderdata, _addressPath, TemplateName,
                                                            transactionid);
                            if (Request.QueryString["Ref"] == null)
                            {
                                var obj = new AspxOrderDetails();
                                obj.UpdateItemQuantity(orderdata);
                            }
                        }
                        ClearAllSessionObjects();
                    }
                    catch (Exception ex)
                    {

                        ProcessException(ex);
                    }
                }
                else
                {
                    lblerror.Text = GetSageMessage("Payment", "PaymentError");
                }

            }
            catch (Exception ex)
            {

                ProcessException(ex);
            }
        }
    }

    private string GetTransactioinId(string invoiceno, int storeid, int portalid)
    {
        var objHandler = new PesoPayHandler();
        return objHandler.GetTransactionId(invoiceno, storeid, portalid);
    }
    void ClearAllSessionObjects()
    {
        if (Session["IsFreeShipping"] != null)
        {
            HttpContext.Current.Session.Remove("IsFreeShipping");

        }
        if (Session["DiscountAmount"] != null)
        {
            HttpContext.Current.Session.Remove("DiscountAmount");

        }
        if (Session["CouponCode"] != null)
        {
            HttpContext.Current.Session.Remove("CouponCode");

        }
        if (Session["CouponApplied"] != null)
        {
            HttpContext.Current.Session.Remove("CouponApplied");
        }
        if (Session["IsTestPesoPay"] != null)
        {
            HttpContext.Current.Session.Remove("IsTestPesoPay");
        }
        if (Session["DiscountAll"] != null)
        {
            HttpContext.Current.Session.Remove("DiscountAll");
        }
        if (Session["TaxAll"] != null)
        {
            HttpContext.Current.Session.Remove("TaxAll");
        }
        if (Session["ShippingCostAll"] != null)
        {
            HttpContext.Current.Session.Remove("ShippingCostAll");
        }
        if (Session["GrandTotalAll"] != null)
        {
            HttpContext.Current.Session.Remove("GrandTotalAll");
        }
        if (Session["Gateway"] != null)
        {
            HttpContext.Current.Session.Remove("Gateway");
        }
        if (Session["OrderID"] != null)
        {
            HttpContext.Current.Session.Remove("OrderID");
        }
        if (Session["OrderCollection"] != null)
        {
            HttpContext.Current.Session.Remove("OrderCollection");
        }

    }

    private void GetOrderItemDetails(int orderId, int storeId, int portalId, string cultureName, string userName)
    {
        try
        {

            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            parameter.Add(new KeyValuePair<string, object>("@OrderID", orderId));
            SQLHandler sqlh = new SQLHandler();
            List<InvoiceDetailByorderIDInfo> info = sqlh.ExecuteAsList<InvoiceDetailByorderIDInfo>("usp_Aspx_GetInvoiceDetailsByOrderID", parameter);

            StringBuilder orderItems = new StringBuilder();
            int index = 0;
            orderItems.Append("<table><tr><th>Item</th><th>Price</th><th>Quantity</th></tr>");

            foreach (InvoiceDetailByorderIDInfo item in info)
            {

                orderItems.Append("<tr>");
                if (!string.IsNullOrEmpty(item.CostVariants))
                    orderItems.Append("<td>" + item.ItemName + "(" + item.CostVariants + ")</td>");
                else
                {
                    orderItems.Append("<td>" + item.ItemName + "</td>");

                }
                orderItems.Append("<td>" + item.Price + "</td>");
                orderItems.Append("<td>" + item.Quantity + "</td>");
                orderItems.Append("<tr/>");
                if (index == 0)
                {
                    lblSubTotal.Text = item.GrandSubTotal.ToString();
                    lblShippingCost.Text = item.TotalShippingCost.ToString();
                    lblTotalDiscount.Text = (item.DiscountAmount + item.CouponAmount).ToString();
                    lblTaxTotal.Text = item.TaxTotal.ToString();
                    lblGrandTotal.Text = item.GrandTotal.ToString();
                }
                index++;

            }
            orderItems.Append("</table>");
            ltOrderItems.Text = orderItems.ToString();


        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

}
