using System;
using System.Runtime.Serialization;

namespace AspxCommerce.KPI
{
    [DataContract]
    [Serializable]
  public  class KPIIPDetailsInfo
    {

        private string _iPAddress;
        private string _countryName;
        private string _countryCode;
        private string _cityName;
        private string _regionName;
        private string _latitude;
        private string _longitude;

        [DataMember]
        public string IPAddress
        {
            get
            {
                return this._iPAddress;
            }
            set
            {
                if (this._iPAddress != value)
                {
                    _iPAddress = value;
                }
            }
        }
        [DataMember]
        public string CountryName
        {
            get
            {
                return this._countryName;
            }
            set
            {
                if (this._countryName != value)
                {
                    _countryName = value;
                }
            }
        }
        [DataMember]
        public string CountryCode
        {
            get
            {
                return this._countryCode;
            }
            set
            {
                if (this._countryCode != value)
                {
                    _countryCode = value;
                }
            }
        }
        [DataMember]
        public string CityName
        {
            get
            {
                return this._cityName;
            }
            set
            {
                if (this._cityName != value)
                {
                    _cityName = value;
                }
            }
        }
        [DataMember]
        public string RegionName
        {
            get
            {
                return this._regionName;
            }
            set
            {
                if (this._regionName != value)
                {
                    _regionName = value;
                }
            }
        }
        [DataMember]
        public string Latitude
        {
            get
            {
                return this._latitude;
            }
            set
            {
                if (this._latitude != value)
                {
                    _latitude = value;
                }
            }
        }
        [DataMember]
        public string Longitude
        {
            get
            {
                return this._longitude;
            }
            set
            {
                if (this._longitude != value)
                {
                    _longitude = value;
                }
            }
        }


    }
}
