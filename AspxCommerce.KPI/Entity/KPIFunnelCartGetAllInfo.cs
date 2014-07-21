using System;
using System.Runtime.Serialization;

namespace AspxCommerce.KPI
{
    [DataContract]
    [Serializable]    
 public   class KPIFunnelCartGetAllInfo
 {    
     private string _pageName;
     private string _alternatePageName;
     private int _visit;
     private int _conversion;
     private decimal _conversionRate;
     
     [DataMember]
     public string PageName
     {
         get
         {
             return this._pageName;
         }
         set
         {
             if (this._pageName != value)
             {
                 _pageName = value;
             }
         }
     }
     [DataMember]
     public string AlternatePageName
     {
         get
         {
             return this._alternatePageName;
         }
         set
         {
             if (this._alternatePageName != value)
             {
                 _alternatePageName = value;
             }
         }
     }
     [DataMember]
     public int Visit
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
     public int Conversion
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
     [DataMember]
     public decimal ConversionRate
     {
         get
         {
             return this._conversionRate;
         }
         set
         {
             if (this._conversionRate != value)
             {
                 _conversionRate = value;
             }
         }
     }





    }
}
