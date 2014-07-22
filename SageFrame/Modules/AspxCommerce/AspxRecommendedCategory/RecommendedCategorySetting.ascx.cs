using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.RecommendedCategory;

public partial class Modules_AspxCommerce_AspxRecommendedCategory_RecommendedCategorySetting : BaseAdministrationUserControl
{
    public string ModulePath;
    public int Count;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
                aspxCommonObj.StoreID = GetStoreID;
                aspxCommonObj.PortalID = GetPortalID;
                aspxCommonObj.CultureName = GetCurrentCultureName;
                RecommendCategoryController rcc = new RecommendCategoryController();
                List<RecommendedCategorySettingInfo> catSettingObj = rcc.GetRecommendedCategorySetting(aspxCommonObj);
                foreach (RecommendedCategorySettingInfo item in catSettingObj)
                {
                    Count = item.RecommendedCategoryCount;
                }
                IncludeLanguageJS();
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}