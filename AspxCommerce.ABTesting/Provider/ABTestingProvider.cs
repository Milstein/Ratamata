using System;
using System.Collections.Generic;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;
using System.Data;

namespace AspxCommerce.ABTesting
{
    public class ABTestingProvider
    {

        public static bool ABTestCheckPageExists(AspxCommonInfo aspxCommonObj, string pageTabPath)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@PageTabPath", pageTabPath));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                SQLHandler sqlH = new SQLHandler();
                bool IsExists = sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_ABTestCheckPageExists]", parameter, "@IsExists");
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ABTestID", settingsInfo.ABTestID));
                parameter.Add(new KeyValuePair<string, object>("@ABTestName", settingsInfo.ABTestName));
                parameter.Add(new KeyValuePair<string, object>("@OriginalPageURL", settingsInfo.OriginalPageURL));
                parameter.Add(new KeyValuePair<string, object>("@Variation1PageURL", settingsInfo.Variation1PageURL));
                parameter.Add(new KeyValuePair<string, object>("@Variation2PageURL", settingsInfo.Variation2PageURL));
                parameter.Add(new KeyValuePair<string, object>("@Variation3PageURL", settingsInfo.Variation3PageURL));
                parameter.Add(new KeyValuePair<string, object>("@TrafficPercentage", settingsInfo.TrafficPercentage));
                parameter.Add(new KeyValuePair<string, object>("@EmailNotification", settingsInfo.EmailNotification));
                parameter.Add(new KeyValuePair<string, object>("@EndsOnDate", settingsInfo.EndsOnDate));
                parameter.Add(new KeyValuePair<string, object>("@EndsOnMaxVisit", settingsInfo.EndsOnMaxVisit));
                parameter.Add(new KeyValuePair<string, object>("@UsersInRole", settingsInfo.UsersInRole));
                parameter.Add(new KeyValuePair<string, object>("@IsActive", settingsInfo.IsActive));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_ABTestSaveUpdateSettings]", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPUC(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@Offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@Limit", limit));
                parameter.Add(new KeyValuePair<string, object>("@ABTestName", abTestName));
                SQLHandler sqLH = new SQLHandler();
                List<ABTestGetSettingsAllInfo> lstSettings = sqLH.ExecuteAsList<ABTestGetSettingsAllInfo>("[dbo].[usp_Aspx_ABTestGetSettingsAll]", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ABTestID", abTestID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_ABTestDeleteSettings]", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ABTestID", abTestID));
                SQLHandler sqLH = new SQLHandler();
                ABTestVisitCountInfo visitCount = sqLH.ExecuteAsObject<ABTestVisitCountInfo>("[dbo].[usp_Aspx_ABTestVisitCount]", parameter);
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSPU(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ABTestID", visitConversionInfo.ABTestID));
                parameter.Add(new KeyValuePair<string, object>("@VariationID", visitConversionInfo.VariationID));
                parameter.Add(new KeyValuePair<string, object>("@ABTestPageURL", visitConversionInfo.ABTestPageURL));
                parameter.Add(new KeyValuePair<string, object>("@Visit", visitConversionInfo.Visit));
                parameter.Add(new KeyValuePair<string, object>("@Conversion", visitConversionInfo.Conversion));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_ABTestSaveVisitAndConversion]", parameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static List<ABTestPortalRoles> GetPortalRoles(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
                paramList.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                paramList.Add(new KeyValuePair<string, object>("@IsAll", true));
                paramList.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                SQLHandler sqlH = new SQLHandler();
                List<ABTestPortalRoles> lstPortalRole = sqlH.ExecuteAsList<ABTestPortalRoles>("[dbo].[sp_PortalRoleList]", paramList);
                return lstPortalRole;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool ABTestIsUserInRoles(AspxCommonInfo aspxCommonObj, string DefinedRoleNames)
        {
            try
            {
                List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
                paramList.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                paramList.Add(new KeyValuePair<string, object>("@DefinedRoleNames", DefinedRoleNames));
                SQLHandler sqlH = new SQLHandler();
                bool IsExists = sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_ABTestIsUserInRoles]", paramList, "@IsExists");
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
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ABTestID", abTestID));
                parameter.Add(new KeyValuePair<string, object>("@ShortBy", shortBy));
                SQLHandler sqLH = new SQLHandler();
                List<ABTestingSettingsViewInfo> lstSettingsView = sqLH.ExecuteAsList<ABTestingSettingsViewInfo>("[dbo].[usp_Aspx_ABTestResultsByID]", parameter);
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
                ABTestConversionRateForChartInfoList ih = new ABTestConversionRateForChartInfoList();
                List<KeyValuePair<string, object>> parameter = CommonParmBuilder.GetParamSP(aspxCommonObj);
                parameter.Add(new KeyValuePair<string, object>("@ABTestID", abTestID));
                parameter.Add(new KeyValuePair<string, object>("@ShortBy", shortBy));
                SQLHandler sqLH = new SQLHandler();
                DataSet ds = sqLH.ExecuteAsDataSet("[dbo].[usp_Aspx_ABTestConversionRateForChart]", parameter);

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTableReader dr = ds.Tables[i].CreateDataReader();
                    List<ABTestConversionRateForChartInfo> mList = new List<ABTestConversionRateForChartInfo>();
                    mList = DataSourceHelper.FillCollection<ABTestConversionRateForChartInfo>(dr);
                    switch (i)
                    {
                        case 0:
                            ih.First = mList;
                            break;
                        case 1:
                            ih.Second = mList;
                            break;
                        case 2:
                            ih.Third = mList;
                            break;
                        case 3:
                            ih.Fourth = mList;
                            break;
                    }
                }

                return ih;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
