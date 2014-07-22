using System;
using System.Web;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Core;

public partial class Modules_AspxCommerce_AspxFlight_FlightEdit : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName, modulePath;
    public string DefaultPortalHomePage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                IncludeCss("FlightEdit", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/JqueryUI/jquery.ui.all.css");
                IncludeJs("FlightEdit", "/js/FormValidation/jquery.validate.js", "/js/FormValidation/jquery.metadata.js", "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js",
                            "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js",
                            "/js/DateTime/date.js",
                            "js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js",
                            "/Modules/AspxCommerce/AspxFlightManagement/js/FlightManage.js", "/js/JQueryUI/jquery-ui-1.8.10.custom.js");
                InitializeJS();
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                SageFrameConfig sfConfig = new SageFrameConfig();
                if (PortalID > 1)
                {
                    DefaultPortalHomePage = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                }
                else
                {
                    DefaultPortalHomePage = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                }
            }

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    }

}
