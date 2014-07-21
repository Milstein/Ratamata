#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
#endregion

namespace SageFrame.PortalManagement
{
    public class PortalMgrDataProvider
    {
        public static void AddPortal(string PortalName, bool IsParent, string UserName, string TemplateName,int ParentPortal,string PSEOName)
        {

            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalName", PortalName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsParent", IsParent));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@TemplateName", TemplateName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));

            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalParentID", ParentPortal));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PSEOName", PSEOName));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("sp_PortalAdd", ParaMeterCollection);


        }
        public static void UpdatePortal(int PortalID, string PortalName, bool IsParent, string UserName, string TemplateName)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalName", PortalName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsParent", IsParent));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@TemplateName", TemplateName));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("[sp_PortalUpdate]", ParaMeterCollection);


        }
        public void AddStoreSubscriber(string StoreName, bool IsParent, string UserName,
          string TemplateName, int ParentPortal, string PSEOName)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreName", StoreName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsParent", IsParent));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@TemplateName", TemplateName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalParentID", ParentPortal));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PSEOName", PSEOName));
            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("[usp_Aspx_AddStoreSubscriber]", ParaMeterCollection);
        }
    }
}
