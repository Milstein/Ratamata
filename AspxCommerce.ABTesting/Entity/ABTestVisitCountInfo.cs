using System;
using System.Runtime.Serialization;

namespace AspxCommerce.ABTesting
{
    [DataContract]
    [Serializable]
  public  class ABTestVisitCountInfo
    {
        private int _originalPageVisitCount;
        private int _variation1VisitCount;
        private int _variation2VisitCount;
        private int _variation3VisitCount;

        [DataMember]
        public int OriginalPageVisitCount
        {
            get { return this._originalPageVisitCount; }
            set
            {
                if (_originalPageVisitCount != value)
                {
                    _originalPageVisitCount = value;
                }
            }
        }
        [DataMember]
        public int Variation1VisitCount
        {
            get { return this._variation1VisitCount; }
            set
            {
                if (_variation1VisitCount != value)
                {
                    _variation1VisitCount = value;
                }
            }
        }
        [DataMember]
        public int Variation2VisitCount
        {
            get { return this._variation2VisitCount; }
            set
            {
                if (_variation2VisitCount != value)
                {
                    _variation2VisitCount = value;
                }
            }
        }
        [DataMember]
        public int Variation3VisitCount
        {
            get { return this._variation3VisitCount; }
            set
            {
                if (_variation3VisitCount != value)
                {
                    _variation3VisitCount = value;
                }
            }
        }
       


    }
}
