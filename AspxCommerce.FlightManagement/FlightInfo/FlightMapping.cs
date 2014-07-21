using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class FlightMapping
    {
        [DataMember(Name = "_RowTotal", Order = 0)]
        private System.Nullable<int> _RowTotal;

        [DataMember(Name = "_domesticPlacesMapID", Order = 1)]
        private int _domesticPlacesMapID;

        [DataMember(Name = "_placeFrom", Order = 2)]
        private string _placeFrom;

        [DataMember(Name = "_PlaceTo", Order = 3)]
        private string _PlaceTo;

        [DataMember(Name = "_storeID", Order = 4)]
         private int _storeID;

        [DataMember(Name = "_portalID", Order = 5)]
         private int _portalID;

        [DataMember(Name = "_isActive", Order = 6)]
        private System.Nullable<bool> _isActive;

        [DataMember(Name = "_isDeleted", Order = 7)]
        private System.Nullable<bool> _isDeleted;

        [DataMember(Name = "_isModified", Order = 8)]
        private System.Nullable<bool> _isModified;

        [DataMember(Name = "_addedOn", Order = 9)]
        private System.Nullable<System.DateTime> _addedOn;

        [DataMember(Name = "_updatedOn", Order = 10)]
        private System.Nullable<System.DateTime> _updatedOn;

        [DataMember(Name = "_deletedOn", Order = 11)]
        private System.Nullable<System.DateTime> _deletedOn;

        [DataMember(Name = "_addedBy", Order = 12)]
        private string _addedBy;

        [DataMember(Name = "_updatedBy", Order = 13)]
        private string _updatedBy;

        [DataMember(Name = "_deletedBy", Order = 14)]
        private string _deletedBy;



        public System.Nullable<int> RowTotal
        {
            get
            {
                return this._RowTotal;
            }
            set
            {
                if ((this._RowTotal != value))
                {
                    this._RowTotal = value;
                }
            }
        }


        public int DomesticPlacesMapID
        {
            get
            {
                return this._domesticPlacesMapID;

            }
            set
            {
                if (this._domesticPlacesMapID != value)
                {
                    _domesticPlacesMapID = value;
                }

            }


        }
        public string PlaceFrom
        {
            get
            {
                return this._placeFrom;
            }
            set
            {
                if (this._placeFrom != value)
                {
                    _placeFrom = value;
                }
            }

        }
        public string PlaceTo
        {
            get
            {
                return this._PlaceTo;

            }
            set
            {
                if (this._PlaceTo != value)
                {
                    _PlaceTo = value;
                }

            }


        }
        public int StoreID
        {
            get
            {
                return this._storeID;
            }
            set
            {
                if ((this._storeID != value))
                {
                    this._storeID = value;
                }
            }
        }

        public int PortalID
        {
            get
            {
                return this._portalID;
            }
            set
            {
                if ((this._portalID != value))
                {
                    this._portalID = value;
                }
            }
        }

        public System.Nullable<bool> IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if ((this._isActive != value))
                {
                    this._isActive = value;
                }
            }
        }

        public System.Nullable<bool> IsDeleted
        {
            get
            {
                return this._isDeleted;
            }
            set
            {
                if ((this._isDeleted != value))
                {
                    this._isDeleted = value;
                }
            }
        }

        public System.Nullable<bool> IsModified
        {
            get
            {
                return this._isModified;
            }
            set
            {
                if ((this._isModified != value))
                {
                    this._isModified = value;
                }
            }
        }

        public System.Nullable<System.DateTime> AddedOn
        {
            get
            {
                return this._addedOn;
            }
            set
            {
                if ((this._addedOn != value))
                {
                    this._addedOn = value;
                }
            }
        }

        public System.Nullable<System.DateTime> UpdatedOn
        {
            get
            {
                return this._updatedOn;
            }
            set
            {
                if ((this._updatedOn != value))
                {
                    this._updatedOn = value;
                }
            }
        }

        public System.Nullable<System.DateTime> DeletedOn
        {
            get
            {
                return this._deletedOn;
            }
            set
            {
                if ((this._deletedOn != value))
                {
                    this._deletedOn = value;
                }
            }
        }

        public string AddedBy
        {
            get
            {
                return this._addedBy;
            }
            set
            {
                if ((this._addedBy != value))
                {
                    this._addedBy = value;
                }
            }
        }

        public string UpdatedBy
        {
            get
            {
                return this._updatedBy;
            }
            set
            {
                if ((this._updatedBy != value))
                {
                    this._updatedBy = value;
                }
            }
        }

        public string DeletedBy
        {
            get
            {
                return this._deletedBy;
            }
            set
            {
                if ((this._deletedBy != value))
                {
                    this._deletedBy = value;
                }
            }
        }

    }
}



