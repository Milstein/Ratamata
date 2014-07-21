using System;
using System.Runtime.Serialization;
namespace AspxCommerce.ABTesting
{
    [DataContract]
    [Serializable]
    public class ABTestGetSettingsAllInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private int _rowTotal;

        [DataMember(Name = "_abTestID", Order = 1)]
        private int _abTestID;

        [DataMember(Name = "_abTestName", Order = 2)]
        private string _abTestName;

        [DataMember(Name = "_orginalPageURL", Order =3)]
        private string _orginalPageURL;

        [DataMember(Name = "_variation1PageURL", Order = 4)]
        private string _variation1PageURL;

        [DataMember(Name = "_variation2PageURL", Order = 5)]
        private string _variation2PageURL;

        [DataMember(Name = "_variation3PageURL", Order = 6)]
        private string _variation3PageURL;

        [DataMember(Name = "_trafficPercentage", Order = 7)]
        private int _trafficPercentage;

        [DataMember(Name = "_emailNotification", Order = 8)]
        private bool _emailNotification;

        [DataMember(Name = "_startDate", Order = 9)]
        private string _startDate;

        [DataMember(Name = "_endsOnDate", Order = 10)]
        private string _endsOnDate;

        [DataMember(Name = "_endsOnMaxVisit", Order = 11)]
        private int _endsOnMaxVisit;

        [DataMember(Name = "_usersInRole", Order = 12)]
        private string _usersInRole;

        [DataMember(Name = "_isActive", Order = 13)]
        private bool _isActive;        

        [DataMember(Name = "_status", Order = 14)]
        private string _status;



      
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
