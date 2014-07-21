using AspxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;

namespace AspxCommerce.KPI
{
    public class KPIController
    {

        public static void KPISaveUpdateSettings(KPISaveUpdateSettingsInfo settingsInfo, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                KPIProvider.KPISaveUpdateSettings(settingsInfo, aspxCommonObj);
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

                KPISettingsGetAllInfo lstSettings = KPIProvider.KPISettingsGetAll(aspxCommonObj);
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
                bool IsActive = KPIProvider.KPISettingsIsActive(aspxCommonObj);
                return IsActive;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void KPISaveVisitAndConversion(KPISaveVisitAndConversionInfo vcInfo, AspxCommonInfo aspxCommonObj)
        {
            KPIIPDetailsInfo ipInfo = new KPIIPDetailsInfo();
            try
            {
                string ipaddress;
                ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipaddress == "" || ipaddress == null)
                {
                    ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                // If you run this project on your local host (your computer) then you need to uncomment the next line to avoid getting the UNKOWN IP address.
                // Otherwise, the next line should be commented out if you are running this project on a web server.
                //ipaddress = "110.44.116.232";//Nepal
                ipaddress = "202.166.211.152";//Nepal, Pokhra            
                //ipaddress = "27.121.103.12";//india
                //ipaddress = "182.73.136.62";//india,Delhi            
                //ipaddress = "27.50.103.12";//japan
                //ipaddress = "202.138.79.255";//australia

                string ipCookies = string.Empty;
                if (HttpContext.Current.Request.Cookies["CookiesKPI"] != null)
                {
                    ipCookies = HttpContext.Current.Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["IPAddress"]));
                }
                ipInfo.IPAddress = ipaddress;
                HttpCookie cookieIp = HttpContext.Current.Request.Cookies["CookiesKPI"];
                if (ipaddress != ipCookies)
                {
                    //To get kpi IPInfoDB API Key
                    KPISettingsGetAllInfo kpiSettingObj = new KPISettingsGetAllInfo();
                    kpiSettingObj=  KPIController.KPISettingsGetAll(aspxCommonObj);
                    string iPInfoDBAPIkey=kpiSettingObj.IPInfoDBAPIkey;
                    // Lookup geographic location using IP address
                    XmlTextReader XmlRdr = KPIController.GetLocation(ipaddress, iPInfoDBAPIkey);
                    if (XmlRdr != null)
                    {
                        while (XmlRdr.Read())
                        {
                            if (XmlRdr.Name.ToString() == "countryName")
                            {
                                ipInfo.CountryName = XmlRdr.ReadString().Trim();
                            }
                            if (XmlRdr.Name.ToString() == "countryCode")
                            {
                                ipInfo.CountryCode = XmlRdr.ReadString().Trim();
                            }
                            if (XmlRdr.Name.ToString() == "cityName")
                            {
                                ipInfo.CityName = XmlRdr.ReadString().Trim();
                            }
                            if (XmlRdr.Name.ToString() == "regionName")
                            {
                                ipInfo.RegionName = XmlRdr.ReadString().Trim();
                            }
                            if (XmlRdr.Name.ToString() == "longitude")
                            {
                                ipInfo.Longitude = XmlRdr.ReadString().Trim();
                            }
                            if (XmlRdr.Name.ToString() == "latitude")
                            {
                                ipInfo.Latitude = XmlRdr.ReadString().Trim();
                            }

                        }
                        XmlRdr.Close();
                        HttpCookie IPCookie = new HttpCookie("CookiesKPI");
                        IPCookie.Values["IPAddress"] = ipaddress;
                        IPCookie.Values["CountryName"] = ipInfo.CountryName;
                        IPCookie.Values["CountryCode"] = ipInfo.CountryCode;
                        IPCookie.Values["CityName"] = ipInfo.CityName;
                        IPCookie.Values["RegionName"] = ipInfo.RegionName;
                        IPCookie.Values["Longitude"] = ipInfo.Longitude;
                        IPCookie.Values["Latitude"] = ipInfo.Latitude;
                        IPCookie.Expires = DateTime.Now.AddHours(5);
                        HttpContext.Current.Response.Cookies.Add(IPCookie);
                    }
                }
                else
                {
                    if (HttpContext.Current.Request.Cookies["CookiesKPI"] != null)
                    {

                        ipInfo.IPAddress =HttpContext.Current.Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["IPAddress"]));
                        ipInfo.CountryName = HttpContext.Current.Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["CountryName"]));
                        ipInfo.CountryCode = HttpContext.Current.Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["CountryCode"]));
                        ipInfo.CityName = HttpContext.Current.Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["CityName"]));
                        ipInfo.RegionName = HttpContext.Current.Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["RegionName"]));
                        ipInfo.Longitude = HttpContext.Current.Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["Longitude"]));
                        ipInfo.Latitude = HttpContext.Current.Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["Latitude"]));
                    }
                }

                if (string.IsNullOrEmpty(ipInfo.CountryName))
                {
                    ipInfo.CountryName = null;
                }
                if (string.IsNullOrEmpty(ipInfo.CountryCode))
                {
                    ipInfo.CountryCode = null;
                }
                if (string.IsNullOrEmpty(ipInfo.CityName))
                {
                    ipInfo.CityName = null;
                }
                if (string.IsNullOrEmpty(ipInfo.RegionName))
                {
                    ipInfo.RegionName = null;
                }
                if (string.IsNullOrEmpty(ipInfo.Longitude))
                {
                    ipInfo.Longitude = null;
                }
                if (string.IsNullOrEmpty(ipInfo.Latitude))
                {
                    ipInfo.Latitude = null;
                }

               // KPIController.KPISaveVisitAndConversion(vcInfo, ipInfo, aspxCommonObj);
                KPIProvider.KPISaveVisitAndConversion(vcInfo, ipInfo, aspxCommonObj);
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
                List<KPIFunnelCartGetAllInfo> lstFunnelCart = KPIProvider.KPIFunnelCartGetAll(shortBy, aspxCommonObj);
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
                KPISalesConversionsGetAllInfo salesConversion = KPIProvider.KPISalesConversionsGetAll(shortBy, aspxCommonObj);
                return salesConversion;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static XmlTextReader GetLocation(string ipaddress, string iPInfoDBAPIkey)
        {

            try
            {
                XmlTextReader xmlReadre = KPIProvider.GetLocation(ipaddress, iPInfoDBAPIkey);
                return xmlReadre;
            }
            catch
            {
                return null;
            }
        }

        public static List<KPILocationsVisitGetAllInfo> KPILocationsVisitGetAll(string metric, string shortBy, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KPILocationsVisitGetAllInfo> lstLocation = KPIProvider.KPILocationsVisitGetAll(metric, shortBy, aspxCommonObj);
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
                KPIVisitDetailsGetAllInfoList lstVisitDetails = KPIProvider.KPIVisitDetailsGetAll(metric, shortBy, aspxCommonObj);
                return lstVisitDetails;
            }
            catch (Exception e)
            {
                throw e;
            }

        }



    }
}
