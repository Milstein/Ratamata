using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class AdvetiseGalleryInfo
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private int _rowTotal;
        [DataMember(Name = "_imageID", Order = 1)]
        private int _imageID;
        [DataMember(Name = "_advertiseName", Order = 2)]
        private string _advertiseName;
        [DataMember(Name = "_advertiseUrl", Order = 3)]
        private string _advertiseUrl;
        [DataMember(Name = "_advertiseDescription", Order = 4)]
        private string _advertiseDescription;
        [DataMember(Name = "_imagePath", Order = 5)]
        private string _imagePath;
        [DataMember(Name = "_isActive", Order = 6)]
        private string  _isActive;



        public int RowTotal
        {
            get { return this._rowTotal; }
            set
            {
                if (this._rowTotal != value)
                {
                    this._rowTotal = value;
                }
            }
        }

        public int ImageID
        {
            get { return this._imageID; }
            set
            {
                if (this._imageID != value)
                {
                    this._imageID = value;
                }
            }
        }

        public string AdvertiseName
        {
            get { return this._advertiseName; }
            set
            {
                if (this._advertiseName != value)
                {
                    this._advertiseName = value;
                }
            }
        }

        public string AdvertiseUrl
        {
            get { return this._advertiseUrl; }
            set
            {
                if (this._advertiseUrl != value)
                {
                    this._advertiseUrl = value;
                }
            }
        }

        public string AdvertiseDescription
        {
            get { return this._advertiseDescription; }
            set
            {
                if (this._advertiseDescription != value)
                {
                    this._advertiseDescription = value;
                }
            }
        }
        public string ImagePath
        {
            get { return this._imagePath; }
            set
            {
                if (this._imagePath != value)
                {
                    this._imagePath = value;
                }
            }
        }
        public string IsActive
        {
            get { return this._isActive; }
            set
            {
                if (this._isActive != value)
                {
                    this._isActive = value;
                }
            }
        }
    }
}