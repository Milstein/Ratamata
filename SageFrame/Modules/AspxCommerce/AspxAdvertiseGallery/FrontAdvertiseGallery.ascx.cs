using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxAdvertiseGallery_FrontAdvertiseGallery : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName, NoImageFeaturedItemPath,modulePath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("FrontAdvertiseGallery", "/Modules/AspxCommerce/AspxAdvertiseGallery/module.css");
                IncludeJs("FrontAdvertiseGallery", "/js/FrontImageGallery/jquery.nivo.slider.js", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxAdvertiseGallery/js/FrontAdvertiseGallery.js");
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                StoreSettingConfig ssc = new StoreSettingConfig();
                NoImageFeaturedItemPath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
                modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            }
            IncludeLanguageJS();
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
            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }


    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JEncoder", ResolveUrl("~/js/encoder.js"));
    }
}
