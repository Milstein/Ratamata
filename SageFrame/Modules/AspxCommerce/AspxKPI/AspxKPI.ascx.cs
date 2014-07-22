using SageFrame.Web;
using System;
using System.IO;
using System.Text;
using System.Web.UI;

public partial class Modules_AspxCommerce_AspxKeyPerformanceIndicator_AspxKPI : BaseAdministrationUserControl
{
    public string AspxKPIModulePath;
    public bool IsUseFriendlyUrls = true;   

    protected void page_init(object sender, EventArgs e)
    {
        try
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);          
            AspxKPIModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            IncludeLanguageJS();
            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               
                IncludeJs("AspxCommereCore", "/js/SageFrameCorejs/aspxcommercecore.js");

                IncludeCss("AspxKPI",
                    "/Templates/" + TemplateName + "/css/GridView/tablesort.css",
                    "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                    "/Templates/" + TemplateName + "/css/ToolTip/tooltip.css",
                    "/Templates/" + TemplateName + "/css/PopUp/style.css",
                     "/Modules/AspxCommerce/AspxKPI/css/AspxKPI.css"
                    );
                IncludeJs("AspxKPI", "/js/FormValidation/jquery.validate.js",
                      "/js/GridView/jquery.grid.js",
                      "/js/GridView/SagePaging.js",
                      "/js/GridView/jquery.global.js",
                      "/js/MessageBox/jquery.easing.1.3.js",
                      "/js/PopUp/custom.js",
                      "/js/jquery.cookie.js",
                      "/js/jquery.tipsy.js",
                      "/js/MessageBox/alertbox.js",
                      "/js/DateTime/date.js",
                      "/js/GridView/jquery.dateFormat.js",
                      "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js",
                      "/js/CurrencyFormat/jquery.formatCurrency.all.js",
                      "/Modules/AspxCommerce/AspxKPI/js/AspxKPI.js"
                                        
                      );                
                IncludeCss("AspxKPI", "/Modules/AspxCommerce/AspxKPI/css/jquery.jqplot.min.css");               
                IncludeJs("AspxKPI", "/Modules/AspxCommerce/AspxKPI/js/jquery.jqplot.min.js");                
                IncludeJs("AspxKPI", "/Modules/AspxCommerce/AspxKPI/js/jqplot.donutRenderer.min.js");                
            }

            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }
    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
        Page.ClientScript.RegisterClientScriptInclude("Paging", ResolveUrl("~/js/Paging/jquery.pagination.js"));
    }

}