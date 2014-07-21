using System;
using SageFrame.Web;

public partial class DragonPay_DragonPaySetting : BaseAdministrationUserControl
{
    #region Variables
    public string aspxPaymentModulePath, username, cultureName;
    public int storeID, portalID; 
    #endregion

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
