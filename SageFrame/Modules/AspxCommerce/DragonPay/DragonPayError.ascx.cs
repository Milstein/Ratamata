﻿/*
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;
using AspxCommerce.DragonPay;

public partial class Modules_AspxCommerce_DragonPay_DragonPayError : BaseAdministrationUserControl
{
    #region Variables
    string sageRedirectPath = string.Empty, invoice = string.Empty, payStatus = "Failed", transactionID;
    bool IsUseFriendlyUrls = true;
    #endregion
    public bool IsAppointment = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
                {
                    payStatus = Request.QueryString["Status"];
                }
                SageFrameConfig sfConfig = new SageFrameConfig();

                IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);                
                if (IsUseFriendlyUrls)
                {
                    if (GetPortalID > 1)
                    {
                        sageRedirectPath = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");                       
                    }
                    else
                    {
                        sageRedirectPath = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");                       
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

                if (Session["IsAppointment"] != null)
                {
                    // For Appointement
                    var scheduleObj = (SaveSeminarShedule)HttpContext.Current.Session["AppointmentData"];
                    if (Session["AppointmentData"] != null)
                    {
                        try
                        {
                            //var orderdata = new OrderDetailsCollection();
                            if (HttpContext.Current.Session["AppointmentData"] != null)
                            {
                                invoice = scheduleObj.InvoceNumber;
                            }
                            if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
                            {
                                payStatus = Request.QueryString["Status"];
                            }
                            // transactionID = GetTransactioinId(invoice, GetStoreID, GetPortalID);
                            //  lblPaymentStatus.Text = payStatus;
                            //   lblTransaction.Text = transactionID;
                            lblInvoice.Text = invoice;
                            // lblPaymentMethod.Text = "DragonPay";
                            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy ");

                            lblSeminarSchedule.Text = scheduleObj.ScheduleName;
                            lblScheduleTransID.Text = scheduleObj.TransactionID; //transactionID;
                            lblUserName.Text = scheduleObj.CustomerName;
                            lblContact.Text = scheduleObj.ContactNumber;
                            lblEmail.Text = scheduleObj.Email;
                            lblQuantity.Text = scheduleObj.Quantity.ToString();
                            lblAmount.Text = scheduleObj.Amount.ToString();
                            lblSchedulePaymentMethod.Text = scheduleObj.PaymentMethodName;
                            lblSchedulePaymentStatus.Text = payStatus;
                            IsAppointment = true;
                            ClearAllSession();
                        }
                        catch (Exception ex)
                        {
                            ProcessException(ex);
                        }
                    }
                    else
                    {
                        Response.Redirect(sageRedirectPath, false);
                    }
                }
                else if (Session["OrderID"] != null)
                {
                    OrderDetailsCollection orderdata = new OrderDetailsCollection();
                    if (HttpContext.Current.Session["OrderCollection"] != null)
                    {
                        orderdata = (OrderDetailsCollection) HttpContext.Current.Session["OrderCollection"];
                        invoice = orderdata.ObjOrderDetails.InvoiceNumber.ToString();
                    }

                    lblStatus.Text = payStatus;
                    lblInvoice.Text = invoice;
                    lblPaymentStatus.Text = payStatus;
                    lblPaymentMethod.Text = "DragonPay";
                    lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy ");
                    ClearAllSession();
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
    

    private void ClearAllSession()
    {
        if (Session["DragonPayData"] != null)
        {
            HttpContext.Current.Session.Remove("DragonPayData");
        }
        if (Session["IsTestDragonPay"] != null)
        {
            HttpContext.Current.Session.Remove("IsTestDragonPay");
        }
        if (Session["AmountCoupon"] != null)
        {
            HttpContext.Current.Session.Remove("AmountCoupon");
        }
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
        if (Session["SelectedCurrency"] != null)
        {
            HttpContext.Current.Session.Remove("SelectedCurrency");
        }
        if (Session["UsedGiftCard"] != null)
        {
            HttpContext.Current.Session.Remove("UsedGiftCard");
        }
        if (Session["AppointmentData"] != null)
        {
            HttpContext.Current.Session.Remove("AppointmentData");
        }
        if (Session["IsAppointment"] != null)
        {
            HttpContext.Current.Session.Remove("AppointmentData");
        }
    }
}
