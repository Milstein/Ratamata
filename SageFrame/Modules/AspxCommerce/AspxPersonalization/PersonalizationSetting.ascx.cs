using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using AspxCommerce.Core;

public partial class Modules_AspxCommerce_AspxPersonalization_PersonalizationSetting : BaseAdministrationUserControl 
{
    public string ModulePath;
    public AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                
                aspxCommonObj.StoreID = GetStoreID;
                aspxCommonObj.PortalID = GetPortalID;
                aspxCommonObj.CultureName = GetCurrentCultureName;

                IncludeLanguageJS();



            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}