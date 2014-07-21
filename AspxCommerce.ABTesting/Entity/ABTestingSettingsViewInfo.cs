using System;
using System.Runtime.Serialization;

namespace AspxCommerce.ABTesting
{
    [DataContract]
    [Serializable]
    public class ABTestingSettingsViewInfo
    {
       
        private System.Nullable<int> _variationID;
        private string _variation;
        private System.Nullable<int> _visit;
        private System.Nullable<int> _conversion;
        private string _conversionRate;
        private string _compareToOriginal;
        private string _chancesToBeatOriginal;
        private System.Nullable<int> _totalVisit;
        private System.Nullable<int> _daysOfData;
        private string _status;
       
        [DataMember]
        public System.Nullable<int> VariationID
        {
            get { return this._variationID; }
            set
            {
                if (_variationID != value)
                {
                    _variationID = value;
                }
            }
        }
        [DataMember]
        public string Variation
        {
            get { return this._variation; }
            set
            {
                if (_variation != value)
                {
                    _variation = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> Visit
        {
            get { return this._visit; }
            set
            {
                if (_visit != value)
                {
                    _visit = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> Conversion
        {
            get { return this._conversion; }
            set
            {
                if (_conversion != value)
                {
                    _conversion = value;
                }
            }
        }
        [DataMember]
        public string ConversionRate
        {
            get { return this._conversionRate; }
            set
            {
                if (_conversionRate != value)
                {
                    _conversionRate = value;
                }
            }
        }
        [DataMember]
        public string CompareToOriginal
        {
            get { return this._compareToOriginal; }
            set
            {
                if (_compareToOriginal != value)
                {
                    _compareToOriginal = value;
                }
            }
        }
        [DataMember]
        public string ChancesToBeatOriginal
        {
            get { return this._chancesToBeatOriginal; }
            set
            {
                if (_chancesToBeatOriginal != value)
                {
                    _chancesToBeatOriginal = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> TotalVisit
        {
            get { return this._totalVisit; }
            set
            {
                if (_totalVisit != value)
                {
                    _totalVisit = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> DaysOfData
        {
            get { return this._daysOfData; }
            set
            {
                if (_daysOfData != value)
                {
                    _daysOfData = value;
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
