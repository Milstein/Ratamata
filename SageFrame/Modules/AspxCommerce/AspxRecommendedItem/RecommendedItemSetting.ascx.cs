using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.RecommendedItem;

public partial class Modules_AspxCommerce_AspxRecommendedItem_RecommendedItemSetting : BaseAdministrationUserControl
{
    public string ModulePath;
    public int ItemCount;
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
                RecommendedItemController ric = new RecommendedItemController();
                List<RecommendedItemSettingInfo> itemSettingObj = ric.GetRecommendedItemSetting(aspxCommonObj);
                foreach (RecommendedItemSettingInfo item in itemSettingObj)
                {
                    ItemCount = item.RecommendedItemCount;
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