using System;
using System.Runtime.Serialization;

namespace AspxCommerce.KPI
{
    [DataContract]
    [Serializable]
    public class KPISaveUpdateSettingsInfo
    {
        private System.Nullable<int> _kpiSettingsID;
        private bool _isActive;
        private bool _emailNotification;      
        private string _endDate;
        private string _iPInfoDBAPIkey;

        [DataMember]
        public System.Nullable<int> KPISettingsID
        {
            get
            {
                return this._kpiSettingsID;
            }
            set
            {
                if (this._kpiSettingsID != value)
                {
                    _kpiSettingsID = value;
                }
            }

        }
        [DataMember]
        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if (this._isActive != value)
                {
                    _isActive = value;
                }
            }
        }
        [DataMember]
        public bool EmailNotification
        {
            get
            {
                return this._emailNotification;
            }
            set
            {
                if (this._emailNotification != value)
                {
                    _emailNotification = value;
                }
            }
        }
       
        [DataMember]
        public string EndDate
        {
            get
            {
                return this._endDate;
            }
            set
            {
                if (this._endDate != value)
                {
                    _endDate = value;
                }
            }
        }
        [DataMember]
        public string IPInfoDBAPIkey
        {
            get
            {
                return this._iPInfoDBAPIkey;
            }
            set
            {
                if (this._iPInfoDBAPIkey != value)
                {
                    _iPInfoDBAPIkey = value;
                }
            }
        }



    }
}
