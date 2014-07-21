using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;

/// <summary>
/// Summary description for AdvertiseWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class AdvertiseWebService : System.Web.Services.WebService
{

    public AdvertiseWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void SaveAdvertiseSetting(string SettingValues, string SettingKeys, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@SettingKeys", SettingKeys));
            parameter.Add(new KeyValuePair<string, object>("@SettingValues", SettingValues));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_AdvertiseSettingUpdate]", parameter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<AdvertiseSettingInfo> GetAdvertiseSetting(int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            List<AdvertiseSettingInfo> view = sqlH.ExecuteAsList<AdvertiseSettingInfo>("[dbo].[usp_Aspx_GetAdvertiseSetting]", parameter);
            return view;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<AdvetiseGalleryInfo> GetAdvertiseGalleyList(int offset, int limit, int storeID, int portalID, string cultureName, string advertiseName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            parameter.Add(new KeyValuePair<string, object>("@AdvertiseName", advertiseName));
            SQLHandler sqlH = new SQLHandler();
            List<AdvetiseGalleryInfo> view = sqlH.ExecuteAsList<AdvetiseGalleryInfo>("[dbo].[usp_Aspx_AdvertiseGalleryGet]", parameter);
            return view;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public List<AdvetiseGalleryInfo> AddUpdateAdvertise(int storeID, int portalID, string cultureName, int imageID, string advertiseName, string advertiseUrl, string advertiseDetails, string newFilePath, string prevFilePath, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            FileHelperController fileObj = new FileHelperController();
            string uplodedValue = string.Empty;
            if (newFilePath != null && prevFilePath != newFilePath)
            {
                string tempFolder = @"Upload\temp";
                uplodedValue = fileObj.MoveFileToSpecificFolder(tempFolder, prevFilePath, newFilePath, @"Modules\AspxCommerce\AspxAdvertiseGallery\uploads\", imageID, aspxCommonObj, "advertise_");
            }
            else
            {
                uplodedValue = prevFilePath;
            }
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            parameter.Add(new KeyValuePair<string, object>("@ImageID", imageID));
            parameter.Add(new KeyValuePair<string, object>("@AdvertiseName", advertiseName));
            parameter.Add(new KeyValuePair<string, object>("@AdvertiseUrl", advertiseUrl));
            parameter.Add(new KeyValuePair<string, object>("@AdvertiseDetails", advertiseDetails));
            parameter.Add(new KeyValuePair<string, object>("@ImagePath", uplodedValue));
            SQLHandler sqlH = new SQLHandler();
            List<AdvetiseGalleryInfo> view = sqlH.ExecuteAsList<AdvetiseGalleryInfo>("[dbo].[usp_Aspx_AdvertiseGalleryAddUpdate]", parameter);
            return view;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void DeleteAdvertise(string imageID, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ImageID", imageID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_AdvertiseImageDelete]", parameter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void ActivateAdvertise(string imageID, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ImageID", imageID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_AdvertiseActivate]", parameter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void DeActivateAdvertise(string imageID, int storeID, int portalID, string cultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ImageID", imageID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_AdvertiseDeActivate]", parameter);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<AdvetiseGalleryInfo> AdvertiseFrontImageList(int storeID, int portalID, string cultureName, int count)
    {
        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            parameter.Add(new KeyValuePair<string, object>("@Count", count));
            SQLHandler sqlH = new SQLHandler();
            List<AdvetiseGalleryInfo> view = sqlH.ExecuteAsList<AdvetiseGalleryInfo>("[dbo].[usp_Aspx_AdvertiseFrontImageList]", parameter);
            return view;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

