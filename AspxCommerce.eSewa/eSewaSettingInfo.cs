using System;
using System.Runtime.Serialization;

namespace AspxCommerce.eSewa
{
    [DataContract]
    [Serializable]   
    public class eSewaSettingInfo
    {
        private string _eSewaMerchantID;	
        private string _eSewaSuccessURL;
        private string _eSewaFailureURL;
        private string _eSewaCurrencyCode;
        private string  _IsTesteSewa;      
        
        public eSewaSettingInfo()
        {
        }

        [DataMember]
        public string eSewaMerchantID
        {
            get
            {
                return this._eSewaMerchantID;
            }
            set
            {
                if ((this._eSewaMerchantID != value))
                {
                    this._eSewaMerchantID = value;
                }
            }
        }
        [DataMember]
        public string eSewaSuccessURL
		{
			get
			{
                return this._eSewaSuccessURL;
			}
			set
			{
                if ((this._eSewaSuccessURL != value))
				{
                    this._eSewaSuccessURL = value;
				}
			}
		}
        [DataMember]
        public string eSewaFailureURL
        {
            get
            {
                return this._eSewaFailureURL;
            }
            set
            {
                if ((this._eSewaFailureURL != value))
                {
                    this._eSewaFailureURL = value;
                }
            }
        }  

       
        [DataMember]
        public string IsTesteSewa
        {
            get
            {
                return this._IsTesteSewa;
            }
            set
            {
                if ((this._IsTesteSewa != value))
                {
                    this._IsTesteSewa = value;
                }
            }
        }
        
        [DataMember]
        public string eSewaCurrencyCode
        {
            get
            {
                return this._eSewaCurrencyCode;
            }
            set
            {
                if ((this._eSewaCurrencyCode != value))
                {
                    this._eSewaCurrencyCode = value;
                }
            }
        }
		
    }
}
