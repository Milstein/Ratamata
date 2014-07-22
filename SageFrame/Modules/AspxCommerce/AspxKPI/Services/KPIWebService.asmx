<%@ WebService Language="C#" Class="KPIWebService" %>

/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 20011-2012 by AspxCommerce
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Web;
using System.Web.Services;
//using System.Web.Services.Protocols;
using AspxCommerce.Core;
using AspxCommerce.KPI;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Xml;
using System.IO;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class KPIWebService : System.Web.Services.WebService
{
    /// <summary>
    /// Saves and Updates Settings for KPI
    /// </summary>
    /// <param name="settingsInfo">EmailNotification,EndDate,ISActive,KPISettingID</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CultureName,CustomerID,SessionCode</param>
    [WebMethod]
    public void KPISaveUpdateSettings(KPISaveUpdateSettingsInfo settingsInfo, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            KPIController.KPISaveUpdateSettings(settingsInfo, aspxCommonObj);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// Returns KPI Settings
    /// </summary>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CultureName,CustomerID,SessionCode</param>
    /// <returns>EmailNotification,EndDate,ISActive,KPISettingID</returns>
    [WebMethod]
    public KPISettingsGetAllInfo KPISettingsGetAll(AspxCommonInfo aspxCommonObj)
    {
        try
        {

            KPISettingsGetAllInfo lstSettings = KPIController.KPISettingsGetAll(aspxCommonObj);
            return lstSettings;
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    /// <summary>
    /// Checks Is KPI Setting Actives
    /// </summary>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CultureName,CustomerID,SessionCode</param>
    /// <returns>True,False</returns>
    [WebMethod]
    public bool KPISettingsIsActive(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            bool IsActive = KPIController.KPISettingsIsActive(aspxCommonObj);
            return IsActive;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// Saves Visits and Conversions with visitors Details for KPI
    /// </summary>
    /// <param name="vcInfo">Visit,Conversion,Tabpath,SubTabPath</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CultureName,CustomerID,SessionCode</param>
    [WebMethod]
    public void KPISaveVisitAndConversion(KPISaveVisitAndConversionInfo vcInfo, AspxCommonInfo aspxCommonObj)
    {

        try
        {
            KPIIPDetailsInfo ipInfo = new KPIIPDetailsInfo();

            string ipaddress;
            ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
            {
                ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            // If you run this project on your local host (your computer) then you need to uncomment the next line to avoid getting the UNKOWN IP address.
            // Otherwise, the next line should be commented out if you are running this project on a web server.
            //ipaddress = "110.44.116.232";//Nepal
            //ipaddress = "202.166.211.152";//Nepal, Pokhra            
            //ipaddress = "27.121.103.12";//india
            //ipaddress = "182.73.136.62";//india,Delhi            
            //ipaddress = "27.50.103.12";//japan
            //ipaddress = "202.138.79.255";//australia

            string ipCookies = string.Empty;
            if (HttpContext.Current.Request.Cookies["CookiesKPI"] != null)
            {
                ipCookies = Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["IPAddress"]));
            }
            ipInfo.IPAddress = ipaddress;
            HttpCookie cookieIp = HttpContext.Current.Request.Cookies["CookiesKPI"];
            if (ipaddress != ipCookies)
            {
                // Lookup geographic location using IP address
                XmlTextReader XmlRdr = KPIController.GetLocation(ipaddress);
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

                    ipInfo.IPAddress = Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["IPAddress"]));
                    ipInfo.CountryName = Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["CountryName"]));
                    ipInfo.CountryCode = Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["CountryCode"]));
                    ipInfo.CityName = Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["CityName"]));
                    ipInfo.RegionName = Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["RegionName"]));
                    ipInfo.Longitude = Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["Longitude"]));
                    ipInfo.Latitude = Server.HtmlEncode((HttpContext.Current.Request.Cookies["CookiesKPI"]["Latitude"]));
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

            KPIController.KPISaveVisitAndConversion(vcInfo, ipInfo, aspxCommonObj);
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    /// <summary>
    /// Returns Cart Abonduntment details
    /// </summary>
    /// <param name="shortBy">Day,Month,Year</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CultureName,CustomerID,SessionCode</param>
    /// <returns>Visit,Conversion,PageName</returns>
    [WebMethod]
    public List<KPIFunnelCartGetAllInfo> KPIFunnelCartGetAll(string shortBy, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<KPIFunnelCartGetAllInfo> lstFunnelCart = KPIController.KPIFunnelCartGetAll(shortBy, aspxCommonObj);
            return lstFunnelCart;
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    /// <summary>
    /// Returns Sales and Orders breakdown Information
    /// </summary>
    /// <param name="shortBy">Day, Month,Year</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CultureName,CustomerID,SessionCode</param>
    /// <returns></returns>
    [WebMethod]
    public KPISalesConversionsGetAllInfo KPISalesConversionsGetAll(string shortBy, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            KPISalesConversionsGetAllInfo salesConversion = KPIController.KPISalesConversionsGetAll(shortBy, aspxCommonObj);
            return salesConversion;
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    /// <summary>
    /// Returns Vists Information By Country and City
    /// </summary>
    /// <param name="metric">Country,City</param>
    /// <param name="shortBy">Day,Week,Month,Year,All</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CultureName,CustomerID,SessionCode</param>
    /// <returns></returns>
    [WebMethod]
    public List<KPILocationsVisitGetAllInfo> KPILocationsVisitGetAll(string metric, string shortBy, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<KPILocationsVisitGetAllInfo> lstLocation = KPIController.KPILocationsVisitGetAll(metric, shortBy, aspxCommonObj);
            return lstLocation;
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    /// <summary>
    /// Returns Visit and Vistors Information Pagewise by Country, City and Date
    /// </summary>
    /// <param name="metric">CountryName,CityName</param>
    /// <param name="shortBy">Day,Week,Month,Year,All</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CultureName,CustomerID,SessionCode</param>
    /// <returns></returns>
    [WebMethod]
    public KPIVisitDetailsGetAllInfoList KPIVisitDetailsGetAll(string metric, string shortBy, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            KPIVisitDetailsGetAllInfoList lstVisitDetails = KPIController.KPIVisitDetailsGetAll(metric, shortBy, aspxCommonObj);
            return lstVisitDetails;
        }
        catch (Exception e)
        {
            throw e;
        }

    }




}