using System;
using System.Runtime.Serialization;

namespace AspxCommerce.ABTesting
{
    [DataContract]
    [Serializable]
 public  class ABTestSaveVisitAndConversionInfo
    {
        private System.Nullable<int> _abTestID;
        private System.Nullable<int> _variationID;       
        private string _abTestPageURL;
        private System.Nullable<int> _visit;
        private System.Nullable<int> _conversion;


        [DataMember]
        public System.Nullable<int> ABTestID
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
        public string ABTestPageURL
        {
            get { return this._abTestPageURL; }
            set
            {
                if (_abTestPageURL != value)
                {
                    _abTestPageURL = value;
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

    }
}
