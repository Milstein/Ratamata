using System;
using System.Runtime.Serialization;

namespace AspxCommerce.ABTesting
{
    [DataContract]
    [Serializable]
    public class ABTestSaveUpdateSettingsInfo
    {
      
        private int _rowTotal;
        private int _abTestID;
        private string _abTestName;
        private string _orginalPageURL;
        private string _variation1PageURL;
        private string _variation2PageURL;
        private string _variation3PageURL;
        private int _trafficPercentage;
        private bool _emailNotification;
        private string _startDate;
        private string _endsOnDate;
        private int _endsOnMaxVisit;
        private string _usersInRole;
        private bool _isActive;
        private string _status;

        [DataMember]
        public int RowTotal
        {
            get { return this._rowTotal; }
            set
            {
                if (_rowTotal != value)
                {
                    _rowTotal = value;
                }
            }
        }
         [DataMember]
        public int ABTestID
        {
            get { return this._abTestID; }
            set
            {
                if (_abTestID != value)
                {
                    _abTestID = value;
                }
            }
        }
         [DataMember]
        public string ABTestName
        {
            get { return this._abTestName; }
            set
            {
                if (_abTestName != value)
                {
                    _abTestName = value;
                }
            }
        }
         [DataMember]
        public string OriginalPageURL
        {
            get { return this._orginalPageURL; }
            set
            {
                if (_orginalPageURL != value)
                {
                    _orginalPageURL = value;
                }
            }
        }
         [DataMember]
        public string Variation1PageURL
        {
            get { return this._variation1PageURL; }
            set
            {
                if (_variation1PageURL != value)
                {
                    _variation1PageURL = value;
                }
            }
        }
         [DataMember]
        public string Variation2PageURL
        {
            get { return this._variation2PageURL; }
            set
            {
                if (_variation2PageURL != value)
                {
                    _variation2PageURL = value;
                }
            }
        }
         [DataMember]
        public string Variation3PageURL
        {
            get { return this._variation3PageURL; }
            set
            {
                if (_variation3PageURL != value)
                {
                    _variation3PageURL = value;
                }
            }
        }
         [DataMember]
        public int TrafficPercentage
        {
            get { return this._trafficPercentage; }
            set
            {
                if (_trafficPercentage != value)
                {
                    _trafficPercentage = value;
                }
            }
        }
         [DataMember]
        public bool EmailNotification
        {
            get { return this._emailNotification; }
            set
            {
                if (_emailNotification != value)
                {
                    _emailNotification = value;
                }
            }
        }
         [DataMember]
        public string StartDate
        {
            get { return this._startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                }
            }
        }
         [DataMember]
        public string EndsOnDate
        {
            get { return this._endsOnDate; }
            set
            {
                if (_endsOnDate != value)
                {
                    _endsOnDate = value;
                }
            }
        }
         [DataMember]
        public int EndsOnMaxVisit
        {
            get { return this._endsOnMaxVisit; }
            set
            {
                if (_endsOnMaxVisit != value)
                {
                    _endsOnMaxVisit = value;
                }
            }
        }
         [DataMember]
        public string UsersInRole
        {
            get { return this._usersInRole; }
            set
            {
                if (_usersInRole != value)
                {
                    _usersInRole = value;
                }
            }
        }
         [DataMember]
        public bool IsActive
        {
            get { return this._isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                }
            }
        }

         [DataMember]
        public string Status
        {
            get { return this._status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                }
            }
        }






    }
}
