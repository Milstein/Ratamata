using System;
using System.Web;
using AspxCommerce.Core;
using SageFrame.Core;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxFlightManagement_FlightEntryForm : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName, modulePath, homePageUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("FlightEntryForm", "/Templates/" + TemplateName + "/css/JqueryUI/jquery.ui.all.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Modules/AspxCommerce/AspxFlightManagement/css/style.css");
                IncludeJs("FlightEntryForm", "/js/FormValidation/jquery.validate.js", "/js/FormValidation/jquery.metadata.js", "/Modules/AspxCommerce/AspxFlightManagement/js/FlightEntryForm.js", "js/jquery-ui-1.8.10.custom.js","/js/json2.js", "/js/jquery.cookie.js");
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                SageFrameConfig sfConfig = new SageFrameConfig();
                if (PortalID > 1)
                {
                    homePageUrl = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                }
                else
                {
                    homePageUrl = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }
}
