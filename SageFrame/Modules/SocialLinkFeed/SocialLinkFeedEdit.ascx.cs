using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_SocialLinkFeed_SocialLinkFeedEdit : BaseAdministrationUserControl
{   
    public string UserName;
    public string modulePath;
    public int UserModuleID, PortalID;
    protected void Page_Load(object sender, EventArgs e)
    {
        modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var WorkLogPath='" + ResolveUrl(modulePath) + "';", true);
        UserName = GetUsername;
        PortalID = GetPortalID;
        UserModuleID = Int32.Parse(SageUserModuleID);
        IncludeJs("SocialLinkjs", "/Modules/SocialLinkFeed/js/SocialLink.js", "/Modules/SocialLinkFeed/js/jquery.alerts.js");
        IncludeCss("SocialLinkcss", "/Modules/SocialLinkFeed/css/SocialLink.css");
        
    }
}
