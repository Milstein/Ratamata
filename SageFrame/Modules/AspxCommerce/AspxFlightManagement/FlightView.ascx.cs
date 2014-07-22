using System;
using System.Web;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Core;

public partial class Modules_AspxCommerce_AspxFlight_FlightView : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName,modulePath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("FlightView", "/Templates/" + TemplateName + "/css/JqueryUI/jquery.ui.all.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css","/Modules/AspxCommerce/AspxFlightManagement/css/style.css");
                IncludeJs("FlightView", "/js/FormValidation/jquery.validate.js", "/js/FormValidation/jquery.metadata.js", "/Modules/AspxCommerce/AspxFlightManagement/js/FlightView.js", "js/jquery-ui-1.8.10.custom.js","/js/json2.js", "/js/jquery.cookie.js");
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }
}
