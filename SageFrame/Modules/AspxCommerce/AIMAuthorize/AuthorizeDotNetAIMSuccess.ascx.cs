﻿using System;
using System.Web;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;
using System.Collections.Generic;
using SageFrame.Web.Utilities;

public partial class Modules_AspxCommerce_AIMAuthorize_AuthorizeDotNetAIMSuccess : BaseAdministrationUserControl
{
          public string SendEmailFrom, SendOrderNotice;
    bool _isUseFriendlyUrls = true;
    string _sageRedirectPath = string.Empty;
    decimal currencyRate = 1;
    public int orderID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.UserName = GetUsername;
            aspxCommonObj.CultureName = GetCurrentCultureName;


            if (!IsPostBack)
            {
                if (Session["OrderID"] != null)
                {
                    orderID = int.Parse(Session["OrderID"].ToString());
                }
                var sfConfig = new SageFrameConfig();
                _isUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);

                if (_isUseFriendlyUrls)
                {
                    if (!IsParent)
                    {
                        _sageRedirectPath = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                    }
                    else
                    {
                        _sageRedirectPath = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                    }
                }
                else
                {
                    _sageRedirectPath = ResolveUrl("{~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
                }

                var imgProgress = (Image)UpdateProgress1.FindControl("imgPrgress");
                if (imgProgress != null)
                {
                    imgProgress.ImageUrl = GetTemplateImageUrl("ajax-loader.gif", true);
                }
                hlnkHomePage.NavigateUrl = _sageRedirectPath;

                SendEmailFrom = StoreSetting.GetStoreSettingValueByKey(StoreSetting.SendEcommerceEmailsFrom, GetStoreID, GetPortalID, GetCurrentCultureName);
                SendOrderNotice = StoreSetting.GetStoreSettingValueByKey(StoreSetting.SendOrderNotification, GetStoreID, GetPortalID, GetCurrentCultureName);
                SendConfrimMessage();
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void SendConfrimMessage()
    {
        try
        {
            if (Session["OrderID"] != null)
            {
                if (Session["TransDetailsAIM"] != null)
                {
                    string transID = GetTransactionDetailById(int.Parse(Session["OrderID"].ToString()));
                    string[] details = Session["TransDetailsAIM"].ToString().Split('#');
                    lblTransaction.Text = transID;
                    lblInvoice.Text = details[0];
                    lblOrderNo.Text = "#" + Session["OrderID"].ToString();
                    lblPaymentMethod.Text = details[2];
                    lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy ");
                    lblerror.Text = GetSageMessage("Payment", "PaymentProcessed");

                    HttpContext.Current.Session.Remove("OrderCollection");
                    HttpContext.Current.Session.Remove("TransDetailsAIM");



                }
                HttpContext.Current.Session.Remove("OrderID");
                CheckOutHelper helper = new CheckOutHelper();
                helper.ClearSessions();
            }
            else
            {
                Response.Redirect(_sageRedirectPath, false);
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public static string GetTransactionDetailById(int orderID)
    {
        try
        {
            var paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@OrderID", orderID));
            var sqlH = new SQLHandler();
            return sqlH.ExecuteAsScalar<string>("usp_Aspx_GetTransactionDetailById", paraMeter);

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}
