using System;
using System.Collections.Generic;
using System.Web.Services;
using AspxCommerce.Core;
using AspxCommerce.ABTesting;

/// <summary>
/// Summary description for SeasonalItemWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ABTestingWebService : System.Web.Services.WebService
{
    /// <summary>
    /// It is used to check whether provided pageTabPath exists or not in database
    /// </summary>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CustomerID,CultureName,SessionCode</param>
    /// <param name="pageTabPath">e.g : /My-Cart</param>
    /// <returns>True/False</returns>
    [WebMethod]
    public bool ABTestCheckPageExists(AspxCommonInfo aspxCommonObj, string pageTabPath)
    {
        try
        {
            bool IsExists = ABTestingController.ABTestCheckPageExists(aspxCommonObj, pageTabPath);
            return IsExists;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    /// <summary>
    /// It is used to save update setting for A/B Test
    /// </summary>
    /// <param name="settingsInfo">Parameters used for settings in A/B Test</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CustomerID,CultureName,SessionCode</param>
    [WebMethod]
    public void ABTestSaveUpdateSettings(ABTestSaveUpdateSettingsInfo settingsInfo, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ABTestingController.ABTestSaveUpdateSettings(settingsInfo, aspxCommonObj);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// It is used to get setting for A/B Test
    /// </summary>
    /// <param name="offset">Offset for gridveiw e.g: 1</param>
    /// <param name="limit">limit for gridveiw e.g: 10</param>
    /// <param name="abTestName">e.g: 'My Cart'</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CustomerID,CultureName,SessionCode</param>
    /// <returns>List of Settings used in A/B test</returns>
    [WebMethod]
    public List<ABTestGetSettingsAllInfo> ABTestGetSettingsAll(int offset, int limit, string abTestName, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<ABTestGetSettingsAllInfo> lstSettings = ABTestingController.ABTestGetSettingsAll(offset, limit, abTestName, aspxCommonObj);
            return lstSettings;
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    /// <summary>
    /// It is used to delete setting for A/B Test
    /// </summary>
    /// <param name="settingsInfo">Parameters used to delete settings in A/B Test</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CustomerID,CultureName,SessionCode</param>
    [WebMethod]
    public void ABTestDeleteSettings(int abTestID, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ABTestingController.ABTestDeleteSettings(abTestID, aspxCommonObj);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// It is used to count vist for a articular test for A/B Test
    /// </summary>
    /// <param name="abTestID">Parameters used to count visit A/B Test</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CustomerID,CultureName,SessionCode</param>
    /// <returns>Number of visits for pages like Original Page, Variation1, Variation2, Variation3 for a particular AB Test ID</returns>
    [WebMethod]
    public ABTestVisitCountInfo ABTestVisitCount(int abTestID, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ABTestVisitCountInfo visitCount = ABTestingController.ABTestVisitCount(abTestID, aspxCommonObj);
            return visitCount;
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    /// <summary>
    /// It is used to save visit and conversion for A/B Test
    /// </summary>
    /// <param name="visitConversionInfo">Parameters used</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CustomerID,CultureName,SessionCode</param>
    [WebMethod]
    public void ABTestSaveVisitAndConversion(ABTestSaveVisitAndConversionInfo visitConversionInfo, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ABTestingController.ABTestSaveVisitAndConversion(visitConversionInfo, aspxCommonObj);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// Used to List All roles availabe for the particular user involved in A/B Testing
    /// </summary>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CustomerID,CultureName,SessionCode</param>
    /// <returns>List of Roles</returns>
    [WebMethod]
    public List<ABTestPortalRoles> GetAllRoles(AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<ABTestPortalRoles> lstPortalRole = ABTestingController.GetPortalRoles(aspxCommonObj);
            return lstPortalRole;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// used to check if current user exists in roles defined in A/B test
    /// </summary>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CustomerID,CultureName,SessionCode</param>
    /// <param name="DefinedRoleNames">Role Names Defined in A/B test</param>
    /// <returns>True/False</returns>
    [WebMethod]
    public bool ABTestIsUserInRoles(AspxCommonInfo aspxCommonObj, string DefinedRoleNames)
    {
        try
        {
            bool IsExists = ABTestingController.ABTestIsUserInRoles(aspxCommonObj, DefinedRoleNames);
            return IsExists;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    /// <summary>
    /// Used to get A/B test Result for the dash board for a particaular test
    /// </summary>
    /// <param name="abTestID">AB Test ID to find Result</param>
    /// <param name="shortBy">to find data of (day,month,year)</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CustomerID,CultureName,SessionCode</param>
    /// <returns>List of results for variations for a particular test id</returns>
    [WebMethod]
    public List<ABTestingSettingsViewInfo> ABTestResultByID(int abTestID, string shortBy, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            List<ABTestingSettingsViewInfo> lstSettingsView = ABTestingController.ABTestResultByID(abTestID, shortBy, aspxCommonObj);
            return lstSettingsView;
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    /// <summary>
    /// Used to find data in date and conversion rate format to display a char in A/B test
    /// </summary>
    /// <param name="abTestID">ID to show Data for</param>
    /// <param name="shortBy">to find data of (day,month,year)</param>
    /// <param name="aspxCommonObj">StoreID,PortalID,UserName,CustomerID,CultureName,SessionCode</param>
    /// <returns>data set for each variation to show chart</returns>
    [WebMethod]
    public ABTestConversionRateForChartInfoList ABTestConversionRateForChart(int abTestID, string shortBy, AspxCommonInfo aspxCommonObj)
    {
        try
        {
            ABTestConversionRateForChartInfoList lstConversion = ABTestingController.ABTestConversionRateForChart(abTestID, shortBy, aspxCommonObj);
            return lstConversion;
        }
        catch (Exception e)
        {
            throw e;
        }

    }
}

