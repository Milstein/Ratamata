using System;
using System.Runtime.Serialization;

namespace AspxCommerce.DragonPay
{
    [DataContract]
    [Serializable]   
   public class DragonPaySettingInfo
    {

       public DragonPaySettingInfo()
       {
       }

       private string _dragonPayMerchantID;
       private string _dragonPaySecretKey;
       private string _dragonPayPostBackURL;
       private string _dragonPayReturnURL;
       private string _dragonPayCurrencyCode;
       private string _isTestDragonPay;

       [DataMember]
       public string DragonPayMerchantID
       {
           get { return this._dragonPayMerchantID; }
           set { this._dragonPayMerchantID = value; }
       }
       [DataMember]
       public string DragonPaySecretKey
       {
           get { return this._dragonPaySecretKey; }
           set { this._dragonPaySecretKey = value; }
       }
       [DataMember]
       public string DragonPayPostBackURL
       {
           get { return this._dragonPayPostBackURL; }
           set { this._dragonPayPostBackURL = value; }
       }      
       [DataMember]
       public string DragonPayReturnURL
       {
           get { return this._dragonPayReturnURL; }
           set { this._dragonPayReturnURL = value; }
       }       
 
       [DataMember]
       public string DragonPayCurrencyCode
       {
           get { return this._dragonPayCurrencyCode; }
           set { this._dragonPayCurrencyCode = value; }
       }
       [DataMember]
       public string IsTestDragonPay
       {
           get { return this._isTestDragonPay; }
           set { this._isTestDragonPay = value; }
       }
       
    }

  
}
