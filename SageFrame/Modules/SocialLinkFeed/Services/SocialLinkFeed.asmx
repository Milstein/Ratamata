<%@ WebService Language="C#"  Class="SocialLinkFeed" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.SocialLink.Controller;
using SageFrame.SocialLink.Entities;

/// <summary>
/// Summary description for SocialLinkFeed
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class SocialLinkFeed : System.Web.Services.WebService
{

    public SocialLinkFeed()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void ModifyLink(int linkID, string link, string type, string userName, int userModuleID, int portalID,string displayName)
    {
        SocialLinkInfo objSocialLink = new SocialLinkInfo();
        objSocialLink.LinkID = linkID;
        objSocialLink.Link = link;
        objSocialLink.Type = type;
        objSocialLink.UserName = userName;
        objSocialLink.PortalID = portalID;
        objSocialLink.UserModuleID = userModuleID;
        objSocialLink.DisplayName = displayName;
        SocialLinkController objController = new SocialLinkController();
        objController.ModifyLink(objSocialLink);
    }
    [WebMethod]
    public List<SocialLinkInfo> GetLinks(string userName, int userModuleID, int portalID)
    {
        SocialLinkInfo objSocialLink = new SocialLinkInfo();
        objSocialLink.UserName = userName;
        objSocialLink.PortalID = portalID;
        objSocialLink.UserModuleID = userModuleID;
        SocialLinkController objController = new SocialLinkController();
        return objController.GetLinks(objSocialLink);
    }
    [WebMethod]
    public void DeleteLink(int linkID)
    {
        SocialLinkInfo objSocialLink = new SocialLinkInfo();
        objSocialLink.LinkID = linkID;
        SocialLinkController objController = new SocialLinkController();
        objController.DeleteLink(objSocialLink);
    }

}


