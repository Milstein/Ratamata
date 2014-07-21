using System;
using AspxCommerce.Core;
using System.Collections.Generic;

namespace AspxCommerce.ABTesting
{
    public class ABTestingController
    {
        public static bool ABTestCheckPageExists(AspxCommonInfo aspxCommonObj, string pageTabPath)
        {
            try
            {
                bool IsExists = ABTestingProvider.ABTestCheckPageExists(aspxCommonObj, pageTabPath);
                return IsExists;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void ABTestSaveUpdateSettings(ABTestSaveUpdateSettingsInfo settingsInfo, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ABTestingProvider.ABTestSaveUpdateSettings(settingsInfo, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<ABTestGetSettingsAllInfo> ABTestGetSettingsAll(int offset, int limit, string abTestName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ABTestGetSettingsAllInfo> lstSettings = ABTestingProvider.ABTestGetSettingsAll(offset, limit, abTestName, aspxCommonObj);
                return lstSettings;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static void ABTestDeleteSettings(int abTestID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ABTestingProvider.ABTestDeleteSettings(abTestID, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static ABTestVisitCountInfo ABTestVisitCount(int abTestID, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ABTestVisitCountInfo visitCount = ABTestingProvider.ABTestVisitCount(abTestID, aspxCommonObj);
                return visitCount;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static void ABTestSaveVisitAndConversion(ABTestSaveVisitAndConversionInfo visitConversionInfo, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ABTestingProvider.ABTestSaveVisitAndConversion(visitConversionInfo, aspxCommonObj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<ABTestPortalRoles> GetPortalRoles(AspxCommonInfo aspxCommonObj)
        {
            List<ABTestPortalRoles> lstPortalRole = ABTestingProvider.GetPortalRoles(aspxCommonObj);
            return lstPortalRole;
        }

        public static bool ABTestIsUserInRoles(AspxCommonInfo aspxCommonObj, string DefinedRoleNames)
        {
            try
            {
                bool IsExists = ABTestingProvider.ABTestIsUserInRoles(aspxCommonObj, DefinedRoleNames);
                return IsExists;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<ABTestingSettingsViewInfo> ABTestResultByID(int abTestID, string shortBy, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<ABTestingSettingsViewInfo> lstSettingsView = ABTestingProvider.ABTestResultByID(abTestID, shortBy, aspxCommonObj);
                return lstSettingsView;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static ABTestConversionRateForChartInfoList ABTestConversionRateForChart(int abTestID, string shortBy, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                ABTestConversionRateForChartInfoList lstConversion = ABTestingProvider.ABTestConversionRateForChart(abTestID, shortBy, aspxCommonObj);
                return lstConversion;
            }
            catch (Exception e)
            {
                throw e;
            }

        }




    }
}
