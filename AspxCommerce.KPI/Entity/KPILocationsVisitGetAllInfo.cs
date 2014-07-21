using System;
using System.Runtime.Serialization;

namespace AspxCommerce.KPI
{
    [DataContract]
    [Serializable]
    public class KPILocationsVisitGetAllInfo
    {
        //MetricName,Visits, VisitPer
        private string _metricName;
        private int _visits;
        private string _visitPer;


        [DataMember]
        public string MetricName
        {
            get
            {
                return this._metricName;
            }
            set
            {
                if (this._metricName != value)
                {
                    _metricName = value;
                }
            }
        }
        [DataMember]
        public int Visits
        {
            get
            {
                return this._visits;
            }
            set
            {
                if (this._visits != value)
                {
                    _visits = value;
                }
            }
        }
        [DataMember]
        public string VisitPer
        {
            get
            {
                return this._visitPer;
            }
            set
            {
                if (this._visitPer != value)
                {
                    _visitPer = value;
                }
            }
        }




    }
}
