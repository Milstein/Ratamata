using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SageFrame.Web;

public partial class PesoPay_PesoPaySetting : BaseAdministrationUserControl
{
    public string aspxPaymentModulePath;
    public int storeID;
    public int portalID;
    public string username;
    public string cultureName;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                storeID = GetStoreID;
                portalID = GetPortalID;
                username = GetUsername;
                cultureName = GetCurrentCultureName;
            }
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
            aspxPaymentModulePath = ResolveUrl(modulePath);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
