using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AspxCommerce.KPI
{
    public class KPIVisitDetailsGetAllInfoList
    {
        public List<VisitorsInfo> Visitor { get; set; }
        public List<PageViewsInfo> PageViews { get; set; }
    }
    [Serializable]
    [DataContract]
    public class VisitorsInfo
    {
        //TotalVisitor,NewVisitor,NewVisitorPer,ReturningVisitor,ReturningVisitorPer
        private int _totalVisitor;
        private int _newVisitor;
        private string _newVisitorPer;
        private int _returningVisitor;
        private string _returningVisitorPer;

        [DataMember]
        public int TotalVisitor
        {
            get { return this._totalVisitor; }
            set
            {
                if (_totalVisitor != value)
                {
                    _totalVisitor = value;
                }
            }
        }
        [DataMember]
        public int NewVisitor
        {
            get { return this._newVisitor; }
            set
            {
                if (_newVisitor != value)
                {
                    _newVisitor = value;
                }
            }
        }
        [DataMember]
        public string NewVisitorPer
        {
            get { return this._newVisitorPer; }
            set
            {
                if (_newVisitorPer != value)
                {
                    _newVisitorPer = value;
                }
            }
        }
        [DataMember]
        public int ReturningVisitor
        {
            get { return this._returningVisitor; }
            set
            {
                if (_returningVisitor != value)
                {
                    _returningVisitor = value;
                }
            }
        }
        [DataMember]
        public string ReturningVisitorPer
        {
            get { return this._returningVisitorPer; }
            set
            {
                if (_returningVisitorPer != value)
                {
                    _returningVisitorPer = value;
                }
            }
        }      

    }
    public class PageViewsInfo
    {
      //SubTabPath, Visits,VisitsPer,AverageDuration,TotalVisits
        private string _subTabPath;
        private int _visits;
        private string _visitsPer;
        private string _averageDuration;
        private int _totalVisits;

        [DataMember]
        public string SubTabPath
        {
            get { return this._subTabPath; }
            set
            {
                if (_subTabPath != value)
                {
                    _subTabPath = value;
                }
            }
        }
        [DataMember]
        public int Visits
        {
            get { return this._visits; }
            set
            {
                if (_visits != value)
                {
                    _visits = value;
                }
            }
        }
        [DataMember]
        public string VisitsPer
        {
            get { return this._visitsPer; }
            set
            {
                if (_visitsPer != value)
                {
                    _visitsPer = value;
                }
            }
        }
        [DataMember]
        public string AverageDuration
        {
            get { return this._averageDuration; }
            set
            {
                if (_averageDuration != value)
                {
                    _averageDuration = value;
                }
            }
        }
        [DataMember]
        public int TotalVisits
        {
            get { return this._totalVisits; }
            set
            {
                if (_totalVisits != value)
                {
                    _totalVisits = value;
                }
            }
        }

      

    }


}
