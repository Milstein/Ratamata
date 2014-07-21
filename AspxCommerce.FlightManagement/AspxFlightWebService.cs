using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using AspxCommerce.Core;
using SageFrame.Shared;
using SageFrame.Common;
using SageFrame.SageFrameClass.MessageManagement;

/// <summary>
/// Summary description for AspxFlightWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AspxFlightWebService : System.Web.Services.WebService
{

    public AspxFlightWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<FlightMapping> GetFlightMappingList(int offset, int limit, string PlaceFrom, string PlaceTo, int storeID, int portalID, string CultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@FlightFromId", PlaceFrom));
            parameter.Add(new KeyValuePair<string, object>("@FlightToId", PlaceTo));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<FlightMapping>("usp_Aspx_GetFlightMapping", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void DeleteFlightMapping(string mappingID, int storeID, int portalID, string userName, string CultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@DomesticPlacesMapID", mappingID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));

            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteFlightMapping", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<FlightPlacesInfo> GetDomesticFlightLocation(int storeID, int portalID, string CultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<FlightPlacesInfo>("usp_Aspx_GetFlightDomesticPlaces", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public bool InsertFlightMapping(int fromId, int ToId, int storeID, int portalID, string userName, string CultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@FlightFromId", fromId));
            ParaMeter.Add(new KeyValuePair<string, object>("@FlightToId", ToId));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));

            SQLHandler sqlH = new SQLHandler();

            return sqlH.ExecuteNonQueryAsBool("usp_Aspx_InsertFlightMapping", ParaMeter, "@returnValue");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<FlightPlacesInfo> GetDomesticPlacesToIDbyFromID(int storeID, int portalID, string CultureName, int DomesticPlacesFromID)
    {

        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            parameter.Add(new KeyValuePair<string, object>("@DomesticPlacesFromID", DomesticPlacesFromID));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<FlightPlacesInfo>("usp_Aspx_GetDomesticPlacesToIDbyFromID", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<FlightTypeInfo> GetFlightType(int storeID, int portalID, string CultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<FlightTypeInfo>("usp_Aspx_GetFlightType", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<FlightTripTypeInfo> GetFlightTripType(int storeID, int portalID, string CultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<FlightTripTypeInfo>("usp_Aspx_GetFlightTripType", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<FlightClassInfo> GetFlightClass(int storeID, int portalID, string CultureName, int FlightTypeID)
    {

        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            parameter.Add(new KeyValuePair<string, object>("@FlightTypeID", FlightTypeID));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<FlightClassInfo>("usp_Aspx_GetFlightClass", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<FlightNationalityInfo> GetNationality(int storeID, int portalID, string CultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<FlightNationalityInfo>("usp_Aspx_GetFlightNationality", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void InsertFlightReservation(int reservationID, int flightTypId, int tripType, string from, string to, string departTime, string returnTime, int adult, int child, int infant, int flightClassId, int nationalityId, string fname, string mname, string lname, string otherTravel, string phone, string mobNum, string email, string addInfo, AspxCommonInfo aspxCommonObj, string msgDetail, string subject)
    {

        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ReservationID", reservationID));
            ParaMeter.Add(new KeyValuePair<string, object>("@FlightType", flightTypId));
            ParaMeter.Add(new KeyValuePair<string, object>("@TripType", tripType));
            ParaMeter.Add(new KeyValuePair<string, object>("@From", from));
            ParaMeter.Add(new KeyValuePair<string, object>("@To", to));
            ParaMeter.Add(new KeyValuePair<string, object>("@Depart", departTime));
            ParaMeter.Add(new KeyValuePair<string, object>("@Return", returnTime));
            ParaMeter.Add(new KeyValuePair<string, object>("@Adult", adult));
            ParaMeter.Add(new KeyValuePair<string, object>("@Child", child));
            ParaMeter.Add(new KeyValuePair<string, object>("@Infant", infant));
            ParaMeter.Add(new KeyValuePair<string, object>("@ClassId", flightClassId));
            ParaMeter.Add(new KeyValuePair<string, object>("@NationalityId", nationalityId));
            ParaMeter.Add(new KeyValuePair<string, object>("@FirstName", fname));
            ParaMeter.Add(new KeyValuePair<string, object>("@MiddleName", mname));
            ParaMeter.Add(new KeyValuePair<string, object>("@LastName", lname));
            ParaMeter.Add(new KeyValuePair<string, object>("@NoOfOtherTravelller", otherTravel));
            ParaMeter.Add(new KeyValuePair<string, object>("@phone", phone));
            ParaMeter.Add(new KeyValuePair<string, object>("@MobileNumber", mobNum));
            ParaMeter.Add(new KeyValuePair<string, object>("@email", email));
            ParaMeter.Add(new KeyValuePair<string, object>("@AdditionalInfo", addInfo));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_InsertFlightReservation", ParaMeter);
            string emailSuperAdmin;
            string emailSiteAdmin;
            var name = fname + ' ' + mname + ' ' + lname;
            SageFrameConfig pagebase = new SageFrameConfig();
            emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
            emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
            EmailTemplate.SendEmailForReservation(aspxCommonObj, name, email, subject, msgDetail);
            //MailHelper.SendMailNoAttachment(emailSiteAdmin, email, subject, msgDetail, emailSiteAdmin, emailSuperAdmin);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<FlightDetailReservationInfo> GetFlightReservationList(int offset, int limit, string From, string To, string FirstName, string LastName, int storeID, int portalID, string CultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@offset", offset));
            parameter.Add(new KeyValuePair<string, object>("@limit", limit));
            parameter.Add(new KeyValuePair<string, object>("@from", From));
            parameter.Add(new KeyValuePair<string, object>("@to", To));
            parameter.Add(new KeyValuePair<string, object>("@fname", FirstName));
            parameter.Add(new KeyValuePair<string, object>("@lname", LastName));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<FlightDetailReservationInfo>("usp_Aspx_GetFlightReservation", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void DeleteFlightReservation(string ReservationID, int storeID, int portalID, string userName, string CultureName)
    {
        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ReservationID", ReservationID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));

            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_DeleteFlightReservation", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void SendEmailForFlightReservation(string subject, string msgDetail, string email, AspxCommonInfo aspxCommonObj, string userName)
    {
        try
        {
            string emailSuperAdmin;
            string emailSiteAdmin;
            SageFrameConfig pagebase = new SageFrameConfig();
            emailSuperAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SuperUserEmail);
            emailSiteAdmin = pagebase.GetSettingsByKey(SageFrameSettingKeys.SiteAdminEmailAddress);
            EmailTemplate.SendEmailForReservation(aspxCommonObj, userName, email, subject, msgDetail);
            //MailHelper.SendMailNoAttachment(emailSiteAdmin, email, subject, msgDetail, emailSiteAdmin, emailSuperAdmin);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public List<FlightDetailByReservationIDInfo> GetFlightReservationDetailByID(int reservationID, int storeID, int portalID, string CultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@ReservationID", reservationID));
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            SQLHandler sqlH = new SQLHandler();
            return sqlH.ExecuteAsList<FlightDetailByReservationIDInfo>("usp_Aspx_GetFlightReservationDetailByID", parameter); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public void UpdateFlightReservation(int reservationID, int flightTypId, int tripType, string from, string to, string departTime, string returnTime, int adult, int child, int infant, int flightClassId, int nationalityId, string fname, string mname, string lname, string otherTravel, string phone, string mobNum, string email, string addInfo, int storeID, int portalID, string CultureName, string userName)
    {

        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@ReservationID", reservationID));
            ParaMeter.Add(new KeyValuePair<string, object>("@FlightType", flightTypId));
            ParaMeter.Add(new KeyValuePair<string, object>("@TripType", tripType));
            ParaMeter.Add(new KeyValuePair<string, object>("@From", from));
            ParaMeter.Add(new KeyValuePair<string, object>("@To", to));
            ParaMeter.Add(new KeyValuePair<string, object>("@Depart", departTime));
            ParaMeter.Add(new KeyValuePair<string, object>("@Return", returnTime));
            ParaMeter.Add(new KeyValuePair<string, object>("@Adult", adult));
            ParaMeter.Add(new KeyValuePair<string, object>("@Child", child));
            ParaMeter.Add(new KeyValuePair<string, object>("@Infant", infant));
            ParaMeter.Add(new KeyValuePair<string, object>("@ClassId", flightClassId));
            ParaMeter.Add(new KeyValuePair<string, object>("@NationalityId", nationalityId));
            ParaMeter.Add(new KeyValuePair<string, object>("@FirstName", fname));
            ParaMeter.Add(new KeyValuePair<string, object>("@MiddleName", mname));
            ParaMeter.Add(new KeyValuePair<string, object>("@LastName", lname));
            ParaMeter.Add(new KeyValuePair<string, object>("@NoOfOtherTravelller", otherTravel));
            ParaMeter.Add(new KeyValuePair<string, object>("@phone", phone));
            ParaMeter.Add(new KeyValuePair<string, object>("@MobileNumber", mobNum));
            ParaMeter.Add(new KeyValuePair<string, object>("@email", email));
            ParaMeter.Add(new KeyValuePair<string, object>("@AdditionalInfo", addInfo));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            SQLHandler sqlH = new SQLHandler();
            sqlH.ExecuteNonQuery("usp_Aspx_InsertFlightReservation", ParaMeter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    [WebMethod]
    public bool InsertDomesticLocation(string Location, int storeID, int portalID, string userName, string CultureName)
    {

        try
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@name", Location));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));

            SQLHandler sqlH = new SQLHandler();

            return sqlH.ExecuteNonQueryAsBool("usp_Aspx_InsertDomesticPlaces", ParaMeter, "@returnValue");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}

