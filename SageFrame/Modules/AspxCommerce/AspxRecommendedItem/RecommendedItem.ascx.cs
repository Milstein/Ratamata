using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Collections;
using AspxCommerce.Core;
using System.Text;
using AspxCommerce.RecommendedItem;
using AspxCommerce.Personalization;

public partial class Modules_AspxCommerce_AspxRecommendedItem_RecommendedItem : BaseAdministrationUserControl
{
    public string SessionCode = string.Empty;
    public int recommendedItemCount;
    public string ModulePath = string.Empty;

    private AspxCommonInfo aspxCommonObj = new AspxCommonInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                aspxCommonObj.StoreID = GetStoreID;
                aspxCommonObj.PortalID = GetPortalID;
                aspxCommonObj.UserName = GetUsername;
                aspxCommonObj.CultureName = GetCurrentCultureName;
                aspxCommonObj.CustomerID = GetCustomerID;

                IncludeCss("RecommendedItemsCss", "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                           "/Templates/" + TemplateName + "/css/MessageBox/style.css");
                IncludeJs("RecommendedItemsJs", "/Modules/AspxCommerce/AspxRecommendedItem/js/RecommendedItem.js", "/js/MessageBox/alertbox.js");
                ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            }
            //     IncludeLanguageJS();
            GetRecommendedItemList();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    Hashtable hst = null;
    public void GetRecommendedItemList()
    {
        bool isModuleInstalled = AspxCommonController.GetModuleInstallationInfo("AspxPersonalization", aspxCommonObj);
        if (isModuleInstalled)
        {
            PersonalizeModuleController pmc = new PersonalizeModuleController();
            PersonalizationSettingKeyPair psetting = new PersonalizationSettingKeyPair();
            psetting = pmc.GetPersonalizeSettingValueByKey(aspxCommonObj, PersonalizationSetting.AspxPersonalization);
            bool isPersonalizationActive = Convert.ToBoolean(psetting.SettingValue);
            if (isPersonalizationActive)
            {
                if (GetCustomerID > 0)
                {
                    BindRecommendedItemList();
                }
            }
            else { }
        }
    }

    private void BindRecommendedItemList()
    {

        string modulePath = this.AppRelativeTemplateSourceDirectory;
        string aspxTemplateFolderPath = ResolveUrl("~/") + "Templates/" + TemplateName;
        string aspxRootPath = ResolveUrl("~/");
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        StringBuilder strRecommendedItemBdl = new StringBuilder();
        List<ItemCommonInfo> recommendedItemList = new List<ItemCommonInfo>();
        RecommendedItemController ric = new RecommendedItemController();
        int count = 0;
        List<RecommendedItemSettingInfo> recItemSettingObj = ric.GetRecommendedItemSetting(aspxCommonObj);
        if (recItemSettingObj != null && recItemSettingObj.Count > 0)
        {
            foreach (var item in recItemSettingObj)
            {
                count = item.RecommendedItemCount;
            }
        }

        recommendedItemList = ric.GetPersonalizeRecommendedItem(aspxCommonObj, count);
        strRecommendedItemBdl.Append("<div class=\"cssClassLeftSideBox cssClassRecommendedItemWrapper\">");
        strRecommendedItemBdl.Append("<div id=\"divRecommendedItem\">");
        strRecommendedItemBdl.Append("<h2 class=\"cssClassLeftHeader\"> Recommended Item</h2></div>");
        strRecommendedItemBdl.Append("<div class=\"cssClassProductLists\">");
        strRecommendedItemBdl.Append("<ul class=\"cssClassRecommendedItemUlList\">");
        if (recommendedItemList != null && recommendedItemList.Count > 0)
        {
            foreach (ItemCommonInfo item in recommendedItemList)
            {
                string hrefItem = aspxRedirectPath + "item/" + fixedEncodeURIComponent(item.SKU) + pageExtension;
                strRecommendedItemBdl.Append("<li><a href=\"" + hrefItem + "\">" + item.ItemName + "</a></li>");
            }
        }
        else
        {
            strRecommendedItemBdl.Append("<li>No recommended Items are listed yet!</li>");
        }
        strRecommendedItemBdl.Append("</ul>");
        strRecommendedItemBdl.Append("</div>");
        strRecommendedItemBdl.Append("</div>");
        litRecommendedItemList.Text = strRecommendedItemBdl.ToString();
    }

    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }
}