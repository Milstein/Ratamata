using System;
using System.Runtime.Serialization;
namespace AspxCommerce.ABTesting
{
    [DataContract]
    [Serializable]
  public  class ABTestPortalRoles
    {

        private System.Guid _roleID;
        private string _roleName;

        [DataMember]
        public System.Guid RoleID
        {
            get
            {
                return this._roleID;
            }
            set
            {
                if ((this._roleID != value))
                {
                    this._roleID = value;
                }
            }
        }
        
      
        [DataMember]
        public string RoleName
        {
            get
            {
                return this._roleName;
            }
            set
            {
                if ((this._roleName != value))
                {
                    this._roleName = value;
                }
            }
        }

       
    }
}
