using AspxCommerce.Core;
using SageFrame.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Xml;

namespace AspxCommerce.KPI
{
    public class KPIProvider
    {

        public static void KPISaveUpdateSettings(KPISaveUpdateSettingsInfo settingsInfo, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@KPISettingsID", settingsInfo.KPISettingsID));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", settingsInfo.IsActive));
                parameter.Add(new KeyValuePair<string, object>("@EmailNotification", settingsInfo.EmailNotification));
                parameter.Add(new KeyValuePair<string, object>("@EndDate", settingsInfo.EndDate));
                parameter.Add(new KeyValuePair<string, object>("@IPInfoDBAPIkey", settingsInfo.IPInfoDBAPIkey));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_KPISaveUpdateSettings]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static KPISettingsGetAllInfo KPISettingsGetAll(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                SQLHandler sqLH = new SQLHandler();
                KPISettingsGetAllInfo lstSettings = sqLH.ExecuteAsObject<KPISettingsGetAllInfo>("[dbo].[usp_Aspx_KPISettingsGetAll]", parameter);
                return lstSettings;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static bool KPISettingsIsActive(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                SQLHandler sqlH = new SQLHandler();
                bool IsActive = sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_KPISettingsIsActive]", parameter, "@IsExists");
                return IsActive;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void KPISaveVisitAndConversion(KPISaveVisitAndConversionInfo vcInfo, KPIIPDetailsInfo ipInfo, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetFullParam(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@TabPath", vcInfo.TabPath));
                parameter.Add(new KeyValuePair<string, object>("@SubTabPath", vcInfo.SubTabPath));
                parameter.Add(new KeyValuePair<string, object>("@Visit", vcInfo.Visit));
                parameter.Add(new KeyValuePair<string, object>("@Conversion", vcInfo.Conversion));
                parameter.Add(new KeyValuePair<string, object>("@IPAddress", ipInfo.IPAddress));
                parameter.Add(new KeyValuePair<string, object>("@CountryName", ipInfo.CountryName));
                parameter.Add(new KeyValuePair<string, object>("@CountryCode", ipInfo.CountryCode));
                parameter.Add(new KeyValuePair<string, object>("@CityName", ipInfo.CityName));
                parameter.Add(new KeyValuePair<string, object>("@RegionName", ipInfo.RegionName));
                parameter.Add(new KeyValuePair<string, object>("@Latitude", ipInfo.Latitude));
                parameter.Add(new KeyValuePair<string, object>("@Longitude", ipInfo.Longitude));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_KPISaveVisitAndConversion]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<KPIFunnelCartGetAllInfo> KPIFunnelCartGetAll(string shortBy, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ShortBy", shortBy));
                SQLHandler sqLH = new SQLHandler();
                List<KPIFunnelCartGetAllInfo> lstFunnelCart = sqLH.ExecuteAsList<KPIFunnelCartGetAllInfo>("usp_Aspx_KPIFunnelCartGetAll", parameter);
                return lstFunnelCart;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static KPISalesConversionsGetAllInfo KPISalesConversionsGetAll(string shortBy, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ShortBy", shortBy));
                SQLHandler sqLH = new SQLHandler();
                KPISalesConversionsGetAllInfo salesConversion = sqLH.ExecuteAsObject<KPISalesConversionsGetAllInfo>("[dbo].[usp_Aspx_KPISalesConversionsGetAll]", parameter);
                return salesConversion;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static XmlTextReader GetLocation(string ipaddress, string iPInfoDBAPIkey)
        {
            // Register at ipinfodb.com for a free key and put it here
            //string myKey = "fd43dd8fc8e8748365bd0de5be024d201cea18455a25a22c0f7e7e6f71621ff3";
            string myKey = iPInfoDBAPIkey;
            WebRequest rssReq = WebRequest.Create("http://api.ipinfodb.com/v3/ip-city/?key=" + myKey + "&ip=" + ipaddress + "&format=xml");
            WebProxy px = new WebProxy("api.ipinfodb.com/v3/ip-city/?key=" + myKey + "&ip=" + ipaddress + "&format=xml", 80);
            px.BypassProxyOnLocal = true;
            rssReq.Proxy = px;            
            rssReq.Timeout = 10000;
            try
            {
                WebResponse rep = rssReq.GetResponse();
                XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());
                return xtr;
            }
            catch (WebException ex)
            {
                return null;
            }
        }

        public static List<KPILocationsVisitGetAllInfo> KPILocationsVisitGetAll(string metric, string shortBy, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@Metric", metric));
                parameter.Add(new KeyValuePair<string, object>("@ShortBy", shortBy));
                SQLHandler sqLH = new SQLHandler();
                List<KPILocationsVisitGetAllInfo> lstLocation = sqLH.ExecuteAsList<KPILocationsVisitGetAllInfo>("usp_Aspx_KPILocationsVisitGetAll", parameter);
                return lstLocation;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static KPIVisitDetailsGetAllInfoList KPIVisitDetailsGetAll(string metric, string shortBy, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                KPIVisitDetailsGetAllInfoList ih = new KPIVisitDetailsGetAllInfoList();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@Metric", metric));
                parameter.Add(new KeyValuePair<string, object>("@ShortBy", shortBy));
                SQLHandler sqLH = new SQLHandler();
                DataSet ds = sqLH.ExecuteAsDataSet("[dbo].[usp_Aspx_KPIVisitDetailsGetAll]", parameter);
                List<VisitorsInfo> visitors = new List<VisitorsInfo>();
                visitors = DataSourceHelper.FillCollection<VisitorsInfo>(ds.Tables[0]);
                ih.Visitor = visitors;
                List<PageViewsInfo> pageViews = new List<PageViewsInfo>();
                pageViews = DataSourceHelper.FillCollection<PageViewsInfo>(ds.Tables[1]);
                ih.PageViews = pageViews;
                return ih;
            }
            catch (Exception e)
            {
                throw e;
            }

        }






    }
}
