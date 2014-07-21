/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2014 by AspxCommerce

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
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Web.UI;
using SageFrame.Framework;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Web;
using System.Web.Security;
using AspxCommerce.Core;
using SageFrame.Core;
using System.Web.Script.Serialization;

public partial class Modules_AspxItemsManagement_ItemsManage : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName, PriceUnit, DimensionUnit, WeightUnit, AllowOutStockPurchase,OutOfStockQuantity;
    public string userEmail = string.Empty;    
    public int MaximumFileSize, MaxDownloadFileSize;
    public string LowStockItemRss, RssFeedUrl,AllowRealTimeNotifications;
    public string CurrencyCodeSlected;
    public string Settings;
    protected void Page_Load(object sender, EventArgs e)    
    {
        try
        {
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterClientScriptInclude("JQueryFormValidated", ResolveUrl("~/js/FormValidation/jquery.validate.js"));
                IncludeCss("ItemsManage", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/AjaxUploader/fileuploader.css",
                   "/Templates/" + TemplateName + "/css/Tabs/slidingtabs-vertical.css","/Modules/AspxCommerce/AspxItemsManagement/css/module.css");
              
                IncludeJs("ItemsManage",  "/js/GridView/jquery.grid.js",
                          "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js",
                          "/js/GridView/jquery.dateFormat.js", "/js/DateTime/date.js",
                          "/js/ImageGallery/jquery.mousewheel.js",
                          "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js",                        
                          "/js/Tabs/jquery.slidingtabs.js",
                          "/js/AjaxFileUploader/ajaxupload.js", "/js/PopUp/custom.js",
                          "/Modules/AspxCommerce/AspxItemsManagement/js/ItemManagement.js", "/js/PopUp/popbox.js",
                          "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js",
                          "/js/CurrencyFormat/jquery.formatCurrency.all.js", "/js/AjaxFileUploader/fileuploader.js");
               
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                SecurityPolicy objSecurity = new SecurityPolicy();
                FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
                if (ticket != null && ticket.Name != ApplicationKeys.anonymousUser)
                {
                    MembershipController member = new MembershipController();
                    UserInfo userDetail = member.GetUserDetails(GetPortalID, GetUsername);
                    userEmail = userDetail.Email;
                }
                StoreSettingConfig ssc = new StoreSettingConfig();
                MaximumFileSize = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaximumImageSize, StoreID, PortalID, CultureName));
                MaxDownloadFileSize = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaxDownloadFileSize, StoreID, PortalID, CultureName));
                PriceUnit = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, StoreID, PortalID, CultureName);
                WeightUnit =ssc.GetStoreSettingsByKey(StoreSetting.WeightUnit, StoreID, PortalID, CultureName);
                DimensionUnit = ssc.GetStoreSettingsByKey(StoreSetting.DimensionUnit, StoreID, PortalID, CultureName);
                LowStockItemRss = ssc.GetStoreSettingsByKey(StoreSetting.LowStockItemRss, StoreID, PortalID, CultureName);
                CurrencyCodeSlected = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, StoreID, PortalID,CultureName);
                AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID, CultureName);
                AllowRealTimeNotifications = ssc.GetStoreSettingsByKey(StoreSetting.AllowRealTimeNotifications, StoreID, PortalID, CultureName);
                if (AllowRealTimeNotifications.ToLower() == "true")
                {
                    IncludeJs("SignalR", false, "/js/SignalR/jquery.signalR-1.0.0-rc2.min.js", "/signalr/hubs", "/Modules/AspxCommerce/AspxStartUpEvents/js/RealTimeAspxMgmt.js");
                }
                if(LowStockItemRss.ToLower()=="true")
                {
                   RssFeedUrl = ssc.GetStoreSettingsByKey(StoreSetting.RssFeedURL, StoreID, PortalID, CultureName);
                }
                GetItemTabSetting();
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + SageUserModuleID + "';", true);
            IncludeJs("ItemsManageCk", "/Editors/ckeditor/ckeditor.js", "/Editors/ckeditor/adapters/jquery.js");
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var aspxItemModulePath='" + ResolveUrl(modulePath) + "';", true);
            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private void GetItemTabSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = GetStoreID;
        aspxCommonObj.PortalID = GetPortalID;
        aspxCommonObj.CultureName = GetCurrentCultureName;
        ItemTabSettingInfo lstItemSetting = new ItemTabSettingInfo();
        lstItemSetting = AspxItemMgntController.ItemTabSettingGet(aspxCommonObj);
        if (lstItemSetting == null)
        {
            lstItemSetting = new ItemTabSettingInfo();
        }
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        if (lstItemSetting != null)
        {
            object obj = new
           {
               EnableCostVariantOption = lstItemSetting.EnableCostVariantOption,
               EnableGroupPrice = lstItemSetting.EnableGroupPrice,
               EnableTierPrice = lstItemSetting.EnableTierPrice,
               EnableRelatedItem = lstItemSetting.EnableRelatedItem,
               EnableCrossSellItem = lstItemSetting.EnableCrossSellItem,
               EnableUpSellItem = lstItemSetting.EnableUpSellItem
           };
            Settings = json_serializer.Serialize(obj);
        }
        else
        {
            object obj = new
            {
                EnableCostVariantOption = lstItemSetting.EnableCostVariantOption,
                EnableGroupPrice = lstItemSetting.EnableGroupPrice,
                EnableTierPrice = lstItemSetting.EnableTierPrice,
                EnableRelatedItem = lstItemSetting.EnableRelatedItem,
                EnableCrossSellItem = lstItemSetting.EnableCrossSellItem,
                EnableUpSellItem = lstItemSetting.EnableUpSellItem
            };
            Settings = json_serializer.Serialize(obj);
        }

    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryFormValidate", ResolveUrl("~/js/FormValidation/jquery.form-validation-and-hints.js"));          
        Page.ClientScript.RegisterClientScriptInclude("J10", ResolveUrl("~/js/encoder.js"));
         
      
    }
}
