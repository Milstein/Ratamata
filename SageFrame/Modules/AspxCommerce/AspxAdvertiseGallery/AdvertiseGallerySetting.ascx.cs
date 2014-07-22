using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxAdvertiseGallery_AdvertiseGallerySetting : BaseAdministrationUserControl
{
    public string cultureName, modulePath;
    public int storeID, portalID;
    public void page_init(object sender, EventArgs e)
    {
        try
        {
             modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
             

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IncludeCss("AspxAdvertiseSetting", "/Templates/" + TemplateName + "/css/MessageBox/style.css");
        IncludeJs("AspxAdvertiseSetting", "/js/MessageBox/alertbox.js");
        storeID = GetStoreID;
        portalID = GetPortalID;
        cultureName = GetCurrentCultureName;

    }
   
}