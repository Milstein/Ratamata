using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.ServiceItem;

public partial class Modules_AspxCommerce_AspxServiceItems_ServiceItemRss :BaseAdministrationUserControl
{
    public int StoreID, PortalID, ServiceRssCount;
    public string CultureName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StoreID = GetStoreID;
            PortalID = GetPortalID;
            CultureName = GetCurrentCultureName;
            GetBrandSetting();
            GetBrandRssFeed();

        }
        IncludeLanguageJS();
    }
    public void GetBrandSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        ServiceItemController objService = new ServiceItemController();
        List<ServiceItemSettingInfo> lstService = objService.GetServiceItemSetting(aspxCommonObj);
        if (lstService != null && lstService.Count > 0)
        {
            foreach (ServiceItemSettingInfo item in lstService)
            {
                ServiceRssCount = int.Parse(item.ServiceRssCount);
            }
        }
    }
    private void GetBrandRssFeed()
    {
        try
        {            
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.UserName = GetUsername;
            aspxCommonObj.CultureName = GetCurrentCulture();
            string pageURL = Request.Url.AbsoluteUri;
            ServiceItemController objService = new ServiceItemController();
            objService.GetRssFeedContens(aspxCommonObj, pageURL, ServiceRssCount);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

}
