using System;
using System.Runtime.Serialization;

namespace AspxCommerce.KPI
{
    [DataContract]
    [Serializable]
    public class KPISaveVisitAndConversionInfo
    {

        private string _tabPath;
        private string _subTabPath;
        private System.Nullable<int> _visit;
        private System.Nullable<int> _conversion;


        [DataMember]
        public string TabPath
        {
            get
            {
                return this._tabPath;
            }
            set
            {
                if (this._tabPath != value)
                {
                    _tabPath = value;
                }
            }
        }
        [DataMember]
        public string SubTabPath
        {
            get
            {
                return this._subTabPath;
            }
            set
            {
                if (this._subTabPath != value)
                {
                    _subTabPath = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> Visit
        {
            get
            {
                return this._visit;
            }
            set
            {
                if (this._visit != value)
                {
                    _visit = value;
                }
            }
        }
        [DataMember]
        public System.Nullable<int> Conversion
        {
            get
            {
                return this._conversion;
            }
            set
            {
                if (this._conversion != value)
                {
                    _conversion = value;
                }
            }
        }
       


    }
}
