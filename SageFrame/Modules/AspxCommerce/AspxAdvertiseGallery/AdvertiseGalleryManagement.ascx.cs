using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxAdvertiseGallery_AdvertiseGalleryManagement : BaseAdministrationUserControl
{
    public string cultureName,modulePath;
    public int storeID, portalID, maxFileSize;
    protected void page_init(object sender, EventArgs e)
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
        IncludeCss("AspxAdvertiseManage", "/Templates/" + TemplateName + "/css/admintemplate.css", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css");
        IncludeJs("AspxAdvertiseManage", "/js/GridView/jquery.grid.js", "/js/GridView/jquery.global.js", "/js/GridView/SagePaging.js", "/js/MessageBox/alertbox.js", "/js/AjaxFileUploader/ajaxupload.js");
        InitializeJS();
        storeID = GetStoreID;
        portalID = GetPortalID;
        cultureName = GetCurrentCultureName; 
        maxFileSize = Convert.ToInt32(StoreSetting.GetStoreSettingValueByKey(StoreSetting.MaximumImageSize, storeID, portalID, cultureName));

    }
    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    }
   
}