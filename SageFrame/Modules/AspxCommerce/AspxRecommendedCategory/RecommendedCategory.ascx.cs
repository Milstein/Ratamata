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
using AspxCommerce.RecommendedCategory;
using AspxCommerce.Personalization;

public partial class Modules_AspxCommerce_AspxRecommendedCategory_RecommendedCategory : BaseAdministrationUserControl
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

                IncludeCss("RecommendedCategoryCss", "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                           "/Templates/" + TemplateName + "/css/MessageBox/style.css");
                IncludeJs("RecommendedCategoryJs", "/Modules/AspxCommerce/AspxRecommendedCategory/js/RecommendedCategory.js", "/js/MessageBox/alertbox.js");
                ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            }
            // IncludeLanguageJS();
            GetRecommendedCategoryList();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    Hashtable hst = null;
    public void GetRecommendedCategoryList()
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
                    BindRecommendedCategory();
                }
            }
            else { }
        }
    }

    private void BindRecommendedCategory()
    {

        string modulePath = this.AppRelativeTemplateSourceDirectory;
        string aspxTemplateFolderPath = ResolveUrl("~/") + "Templates/" + TemplateName;
        string aspxRootPath = ResolveUrl("~/");
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        StringBuilder strRecommendedItemBdl = new StringBuilder();

        List<RecommendedCategoryInfo> recommendedCategoryList = new List<RecommendedCategoryInfo>();
        RecommendCategoryController rcc = new RecommendCategoryController();
        int count = 0;
        List<RecommendedCategorySettingInfo> rCategorySetting = rcc.GetRecommendedCategorySetting(aspxCommonObj);
        if (rCategorySetting != null && rCategorySetting.Count > 0)
        {
            foreach (var cat in rCategorySetting)
            {
                count = cat.RecommendedCategoryCount;
            }
        }

        recommendedCategoryList = rcc.GetRecommendedCategory(aspxCommonObj, count);
        strRecommendedItemBdl.Append("<div class=\"cssClassLeftSideBox cssClassRecommendedCategoryWrapper\">");
        strRecommendedItemBdl.Append("<div id=\"divRecommendedCategory\">");
        strRecommendedItemBdl.Append("<h2 class=\"cssClassLeftHeader\"> Recommended Category</h2></div>");
        strRecommendedItemBdl.Append("<div class=\"cssClassProductLists\">");
        strRecommendedItemBdl.Append("<ul class=\"cssClassRecommendedItemUlList\">");
        if (recommendedCategoryList != null && recommendedCategoryList.Count > 1)
        {            
            foreach (RecommendedCategoryInfo item in recommendedCategoryList)
            {
                string urlPath = HttpContext.Current.Request.Url.AbsolutePath.ToString();
                char[] separator = new char[] { '/' };
                string[] urlValues = urlPath.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                char[] separator2 = new char[] { '.' };
                string cat = urlValues[2];
                string[] catName = cat.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                string categoryName = HttpUtility.UrlDecode(AspxUtility.fixedDecodeURIComponent(catName[0]));
                string dbCategoryName = AspxUtility.fixedDecodeURIComponent(item.CategoryName);
                if (categoryName != dbCategoryName)
                {
                    var hrefItem = aspxRedirectPath + "category/" + AspxUtility.fixedEncodeURIComponent(dbCategoryName) + pageExtension;
                    strRecommendedItemBdl.Append("<li><a href=\"" + hrefItem + "\">" + item.CategoryName + "</a><span class=\"cssCount\"> (" + item.CategoryCount + ")</span></li>");
                }
            }
           
        }
        else {
            strRecommendedItemBdl.Append("<li>No recommended category is listed yet!</li>");
           
        }
        strRecommendedItemBdl.Append("</ul>");
        strRecommendedItemBdl.Append("</div>");
        strRecommendedItemBdl.Append("</div>");
        litRecommendedCategoryList.Text = strRecommendedItemBdl.ToString();
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