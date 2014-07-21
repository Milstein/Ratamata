using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.PesoPay
{
    [DataContract]
    [Serializable]   
   public class PesoPaySettingInfo
    {

       public PesoPaySettingInfo()
       {
       }
       private string _pesoPayMpsMode;
       private int _pesoPayPaymentType;
       private string _pesoPayLanguage;
       private string _pesoPayCancelUrl;
       private string _pesoPaySuccessUrl;
       private string _pesoPayErrorUrl;
       private string _pesoPayDataFeed;
       private int _pesoPayMerchantId;
       private string _pesoPayPayType;
       private string _pesoPayPpaymentMethod;
       private int _pesoPayRedirectTime;
       private int _pesoPayCurrencyCode;
       private string _isTestPesoPay;
       private string _pesoPaySecureHashSecret;
       private string _pesoPayIsSecureHashEnabled;

      
       [DataMember]
       public string PesoPayMpsMode
       {
           get { return this._pesoPayMpsMode; }
           set { this._pesoPayMpsMode = value; }
       }
       [DataMember]
       public int PesoPayPaymentType
       {
           get { return this._pesoPayPaymentType; }
           set { this._pesoPayPaymentType = value; }
       }
       [DataMember]
       public string PesoPayLanguage
       {
           get { return this._pesoPayLanguage; }
           set { this._pesoPayLanguage = value; }
       }
       [DataMember]
       public string PesoPayCancelURL
       {
           get { return this._pesoPayCancelUrl; }
           set { this._pesoPayCancelUrl = value; }
       }
       [DataMember]
       public string PesoPayDataFeed
       {
           get { return this._pesoPayDataFeed; }
           set { this._pesoPayDataFeed = value; }
       }
       [DataMember]
       public string PesoPaySuccessURL
       {
           get { return this._pesoPaySuccessUrl; }
           set { this._pesoPaySuccessUrl = value; }
       }
       [DataMember]
       public string PesoPayErrorURL
       {
           get { return this._pesoPayErrorUrl; }
           set { this._pesoPayErrorUrl = value; }
       }
       [DataMember]
       public int PesoPayMerchantID
       {
           get { return this._pesoPayMerchantId; }
           set { this._pesoPayMerchantId = value; }
       }
       [DataMember]
       public string PesoPayPayType
       {
           get { return this._pesoPayPayType; }
           set { this._pesoPayPayType = value; }
       }
       [DataMember]
       public string PesoPayPaymentMethod
       {
           get { return this._pesoPayPpaymentMethod; }
           set { this._pesoPayPpaymentMethod = value; }
       }
       [DataMember]
       public int PesoPayRedirectTime
       {
           get { return this._pesoPayRedirectTime; }
           set { this._pesoPayRedirectTime = value; }
       }
       [DataMember]
       public int PesoPayCurrencyCode
       {
           get { return this._pesoPayCurrencyCode; }
           set { this._pesoPayCurrencyCode = value; }
       }
       [DataMember]
       public string IsTestPesoPay
       {
           get { return this._isTestPesoPay; }
           set { this._isTestPesoPay = value; }
       }
       [DataMember]
       public string PesoPaySecureHashSecret
       {
           get { return this._pesoPaySecureHashSecret; }
           set { this._pesoPaySecureHashSecret = value; }
       }
       [DataMember]
       public string PesoPayIsSecureHashEnabled
       {
           get { return this._pesoPayIsSecureHashEnabled; }
           set { this._pesoPayIsSecureHashEnabled = value; }
       }
    }

  
}
