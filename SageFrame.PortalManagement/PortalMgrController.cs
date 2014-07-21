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
#endregion

namespace SageFrame.PortalManagement
{
    public class PortalMgrController
    {
        public static void AddPortal(string PortalName, bool IsParent, string UserName, string TemplateName,int ParentPortal,string  PSEOName)
        {
            try
            {
                PortalMgrDataProvider.AddPortal(PortalName, IsParent, UserName, TemplateName, ParentPortal, PSEOName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdatePortal(int PortalID, string PortalName, bool IsParent, string UserName, string TemplateName)
        {
            try
            {
                PortalMgrDataProvider.UpdatePortal(PortalID, PortalName, IsParent, UserName, TemplateName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //IsParent, GetUsername, newPortalname, ParentPortal, PSEOName
        public static void AddStoreSubscriber(string StoreName, bool IsParent, string UserName, string TemplateName, int ParentPortal, string PSEOName)
        {
            try
            {
                PortalMgrDataProvider pmdp = new PortalMgrDataProvider();
                pmdp.AddStoreSubscriber(StoreName, IsParent, UserName, TemplateName, ParentPortal, PSEOName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
