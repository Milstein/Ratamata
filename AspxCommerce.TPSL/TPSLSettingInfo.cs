using System;
using System.Runtime.Serialization;

namespace AspxCommerce.TPSL
{
    [DataContract]
    [Serializable]   
    public class TPSLSettingInfo
    {
        private string _BillerID;
        private string _ResponseUrl;
        private string _Currency;
        private string _CheckSumKey;
        private string  _IsTestTPSL;
        private string _LogfileName;
        public TPSLSettingInfo()
        {
        }
        [DataMember]
        public string BillerID
		{
			get
			{
                return this._BillerID;
			}
			set
			{
                if ((this._BillerID != value))
				{
                    this._BillerID = value;
				}
			}
		}

        [DataMember]
        public string ResponseUrl
		{
			get
			{
                return this._ResponseUrl;
			}
			set
			{
                if ((this._ResponseUrl != value))
				{
                    this._ResponseUrl = value;
				}
			}
		}

        [DataMember]
        public string Currency
		{
			get
			{
                return this._Currency;
			}
			set
			{
                if ((this._Currency != value))
				{
                    this._Currency = value;
				}
			}
		}

        [DataMember]
        public string CheckSumKey
		{
			get
			{
                return this._CheckSumKey;
			}
			set
			{
                if ((this._CheckSumKey != value))
				{
                    this._CheckSumKey = value;
				}
			}
		}
        [DataMember]
        public string IsTestTPSL
        {
            get
            {
                return this._IsTestTPSL;
            }
            set
            {
                if ((this._IsTestTPSL != value))
                {
                    this._IsTestTPSL = value;
                }
            }
        }
        [DataMember]
        public string LogfileName
        {
            get
            {
                return this._LogfileName;
            }
            set
            {
                if ((this._LogfileName != value))
                {
                    this._LogfileName = value;
                }
            }
        }
		
    }
}
