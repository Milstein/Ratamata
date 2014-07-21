using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AspxCommerce.ABTesting
{


   
    public class ABTestConversionRateForChartInfoList
    {

        public List<ABTestConversionRateForChartInfo> First { get; set; }
        public List<ABTestConversionRateForChartInfo> Second { get; set; }
        public List<ABTestConversionRateForChartInfo> Third { get; set; }
        public List<ABTestConversionRateForChartInfo> Fourth{ get; set; }

    
    }


    [Serializable]
    [DataContract]
    public class ABTestConversionRateForChartInfo
    {
        private string _visitedDate;
        private decimal _conversionRate;
       

        [DataMember]
        public string VisitedDate
        {
            get { return this._visitedDate; }
            set
            {
                if (_visitedDate != value)
                {
                    _visitedDate = value;
                }
            }
        }
        [DataMember]
        public decimal ConversionRate
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

       


    }
}
