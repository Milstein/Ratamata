using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
public partial class Modules_SocialLinkFeed_SocialLinkFeed :BaseAdministrationUserControl
{
    public string UserName;
    public string modulePath;
    public int UserModuleID, PortalID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude("JEncoder", ResolveUrl("~/js/encoder.js"));
        IncludeCss("TweetsCss", "/Modules/SocialLinkFeed/css/SocialLink.css");
        IncludeJs("TweetsJs", "/Modules/SocialLinkFeed/js/SocialLinkView.js");
        UserName = GetUsername;
        modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var WorkLogPath='" + ResolveUrl(modulePath) + "';", true);
        PortalID = GetPortalID;
        UserModuleID = Int32.Parse(SageUserModuleID);
    }
}
