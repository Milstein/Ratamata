using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class FlightReservationInfo
    {
        [DataMember(Name = "_RowTotal", Order = 0)]
        private System.Nullable<int> _RowTotal;

        [DataMember(Name = "_reservationID", Order = 1)]
        private int _reservationID;

        [DataMember(Name = "_name", Order = 2)]
        private string _name;

        [DataMember(Name = "_flightTypeName", Order = 3)]
        private string _flightTypeName;

        [DataMember(Name = "_tripName", Order = 4)]
        private string _tripName;

        [DataMember(Name = "_fromTo", Order = 5)]
        private string _fromTo;

        [DataMember(Name = "_phone", Order = 6)]
        private string _phone;

        [DataMember(Name = "_mobileNumber", Order = 7)]
        private string _mobileNumber;

        [DataMember(Name = "_depart", Order = 8)]
        private System.Nullable<System.DateTime> _depart;

        [DataMember(Name = "_return", Order = 9)]
        private System.Nullable<System.DateTime> _return;

        [DataMember(Name = "_customer", Order = 10)]
        private string _customer;

        [DataMember(Name = "_flightClass", Order = 11)]
        private string _flightClass;

        [DataMember(Name = "_nationality", Order = 12)]
        private string _nationality;

        [DataMember(Name = "_nameOfOtherTraveller", Order = 13)]
        private string _nameOfOtherTraveller;

        [DataMember(Name = "_additionalInfo", Order = 14)]
        private string _additionalInfo;

        public System.Nullable<int> RowTotal
        {
            get
            {
                return this._RowTotal;
            }
            set
            {
                if ((this._RowTotal != value))
                {
                    this._RowTotal = value;
                }
            }
        }


        public int ReservationID
        {
            get
            {
                return this._reservationID;

            }
            set
            {
                if (this._reservationID != value)
                {
                    _reservationID = value;
                }

            }


        }
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (this._name != value)
                {
                    _name = value;
                }
            }

        }
        public string FlightTypeName
        {
            get
            {
                return this._flightTypeName;

            }
            set
            {
                if (this._flightTypeName != value)
                {
                    _flightTypeName = value;
                }

            }


        }
        public string TripName
        {
            get
            {
                return this._tripName;

            }
            set
            {
                if (this._tripName != value)
                {
                    _tripName = value;
                }

            }


        }
        public string FromTo
        {
            get
            {
                return this._fromTo;

            }
            set
            {
                if (this._fromTo != value)
                {
                    _fromTo = value;
                }

            }


        }
        public string Phone
        {
            get
            {
                return this._phone;
            }
            set
            {
                if (this._phone != value)
                {
                    _phone = value;
                }

            }
        }
        public string MobileNumber
        {
            get
            {
                return this._mobileNumber;
            }
            set
            {
                if (this._mobileNumber != value)
                {
                    _mobileNumber = value;
                }

            }
        }
        System.Nullable<System.DateTime> Depart
        {
            get
            {
                return this._depart;
            }
            set
            {
                if (this._depart != value)
                {
                    _depart = value;
                }

            }
        }

        System.Nullable<System.DateTime> Return
        {
            get
            {
                return this._return;
            }
            set
            {
                if (this._return != value)
                {
                    _return = value;
                }

            }
        }

        public string Customer
        {
            get
            {
                return this._customer;
            }
            set
            {
                if (this._customer != value)
                {
                    _customer = value;
                }

            }
        }
        public string FlightClass
        {
            get
            {
                return this._flightClass;
            }
            set
            {
                if (this._flightClass != value)
                {
                    _flightClass = value;
                }

            }
        }
         
      

        public string Nationality
        {
            get
            {
                return this._nationality;
            }
            set
            {
                if (this._nationality != value)
                {
                    _nationality = value;
                }

            }
        }

        public string NameOfOtherTraveller
        {
            get
            {
                return this._nameOfOtherTraveller;
            }
            set
            {
                if (this._nameOfOtherTraveller != value)
                {
                    _nameOfOtherTraveller = value;
                }

            }
        }
        public string AdditionalInfo
        {
            get
            {
                return this._additionalInfo;
            }
            set
            {
                if (this._additionalInfo != value)
                {
                    _additionalInfo = value;
                }

            }
        }
    }
}
