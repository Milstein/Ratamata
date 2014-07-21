using SageFrame.Web;
using System;
using System.Web.UI;

public partial class Modules_AspxCommerce_AspxABTesting_AspxABTesting : BaseAdministrationUserControl
{
    public string AspxABTestingModulePath;
    public bool IsUseFriendlyUrls = true;
    protected void page_init(object sender, EventArgs e)
    {
        try
        {
            SageFrameConfig pagebase = new SageFrameConfig();
            IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            AspxABTestingModulePath = ResolveUrl(modulePath);
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

                IncludeCss("ABTesting",
                    "/Templates/" + TemplateName + "/css/GridView/tablesort.css",
                    "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                    "/Templates/" + TemplateName + "/css/JQueryUIFront/jquery.ui.all.css",
                    "/Templates/" + TemplateName + "/css/ToolTip/tooltip.css",
                    "/Templates/" + TemplateName + "/css/PopUp/style.css");

                IncludeCss("ABTesting", "/Modules/AspxCommerce/AspxABTesting/css/module.css");
                IncludeCss("ABTesting", "/Modules/SiteAnalytics/css/module.css");
                IncludeCss("ABTesting", "/Modules/SiteAnalytics/css/jquery.jqplot.css");
                IncludeCss("ABTesting", "/Modules/SiteAnalytics/syntaxhighlighter/styles/shCoreDefault.min.css");
                IncludeCss("ABTesting", "/Modules/SiteAnalytics/syntaxhighlighter/styles/shThemejqPlot.min.css");
                IncludeJs("ABTesting", "/Modules/SiteAnalytics/pjs/jquery.jqplot.min.js");
                IncludeJs("ABTesting", "/Modules/SiteAnalytics/pjs/excanvas.min.js");
                IncludeJs("ABTesting", "/Modules/SiteAnalytics/pjs/jqplot.categoryAxisRenderer.js");
                IncludeJs("ABTesting", "/Modules/SiteAnalytics/pjs/jqplot.pointLabels.min.js");
                IncludeJs("ABTesting", "/Modules/SiteAnalytics/pjs/jqplot.cursor.min.js");
                IncludeJs("ABTesting", "/Modules/SiteAnalytics/pjs/jqplot.dateAxisRenderer.min.js");
                IncludeJs("ABTesting", "/Modules/AspxCommerce/AspxABTesting/js/jqplot.canvasAxisLabelRenderer.min.js");
                IncludeJs("ABTesting", "/Modules/AspxCommerce/AspxABTesting/js/jqplot.canvasTextRenderer.min.js");


                IncludeJs("ABTesting", "/js/FormValidation/jquery.validate.js",
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
                       "/Modules/AspxCommerce/AspxABTesting/js/AspxABTesting.js"

                     

                      );
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