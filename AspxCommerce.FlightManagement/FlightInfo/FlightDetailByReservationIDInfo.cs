using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class FlightDetailByReservationIDInfo
    {
        public int ReservationID { get; set; }
        public int FlightTypeID { get; set; }
        public int TripTypeID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Depart { get; set; }
        public string Return { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        public int ClassID { get; set; }
        public int NationalityID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameOfOtherTraveller { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string AdditionalInfo { get; set; }

    }
}
