using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.Core
{
    [Serializable]
    [DataContract]
    public class AdvertiseSettingInfo
    {
        [DataMember]
        private int _noOfAdvertise;
        [DataMember]
        private string _showUrl;
        [DataMember]
        private string _showDetails;
        

        public int NoOfAdvertise
        {
            get { return this._noOfAdvertise; }
            set
            {
                if (this._noOfAdvertise != value)
                {
                    this._noOfAdvertise = value;
                }
            }
        }

        public string ShowUrl
        {
            get { return this._showUrl; }
            set
            {
                if (this._showUrl != value)
                {
                    this._showUrl = value;
                }
            }
        }

        public string ShowDetails
        {
            get { return this._showDetails; }
            set
            {
                if (this._showDetails != value)
                {
                    this._showDetails = value;
                }
            }
        }
    }
}
