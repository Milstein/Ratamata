using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;
using AspxCommerce.ServiceItem;

public partial class Modules_AspxCommerce_AspxServiceItems_ServiceItemSetting : BaseAdministrationUserControl
{
    public string  ServiceModulePath;
    public string IsEnableService;
    public string ServiceCategoryInARow;
    public string ServiceCategoryCount;
    public string IsEnableServiceRss;
    public string ServiceRssCount,ServiceRssPage;
    public string ServiceDetailsPage,BookAnAppointmentPage,AppointmentSuccessPage;
    public int StoreID, PortalID;
    public string CultureName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CultureName = GetCurrentCultureName;
                ServiceModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                GetServiceItemSetting();
                IncludeLanguageJS();
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    public void GetServiceItemSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = StoreID;
        aspxCommonObj.PortalID = PortalID;
        aspxCommonObj.CultureName = CultureName;
        ServiceItemController objService = new ServiceItemController();
        List<ServiceItemSettingInfo> lstServiceSetting = objService.GetServiceItemSetting(aspxCommonObj);
        if (lstServiceSetting != null && lstServiceSetting.Count > 0)
        {
            foreach (var serviceSetting in lstServiceSetting)
            {
                IsEnableService = serviceSetting.IsEnableService.ToString();
                ServiceCategoryInARow = serviceSetting.ServiceCategoryInARow.ToString();
                ServiceCategoryCount = serviceSetting.ServiceCategoryCount.ToString();
                IsEnableServiceRss = serviceSetting.IsEnableServiceRss.ToString();
                ServiceRssCount = serviceSetting.ServiceRssCount.ToString();
                ServiceRssPage = serviceSetting.ServiceRssPage;
                ServiceDetailsPage = serviceSetting.ServiceDetailsPage;
                BookAnAppointmentPage = serviceSetting.BookAnAppointmentPage;
                AppointmentSuccessPage = serviceSetting.AppointmentSuccessPage;
            }
        }
    }
}