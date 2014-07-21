using System;
using System.Runtime.Serialization;
namespace AspxCommerce.KPI
{
    [DataContract]
    [Serializable]
   public class KPISalesConversionsGetAllInfo
    {
           private int _totlaItemsSold;
           private int _itemsSoldPerOrder;
           private int _totalSKUSold;
           private int _sKUSoldPerOrder;
           private int _totalOrders;
           private int _ordersByNewAccount;
           private int _ordersByExistingAccount;
           private int _ordersByGuestAccount;
           private decimal _totalSales;
           private decimal _salesByNewAccount;
           private decimal _salesByExistingAccount;
           private decimal _salesByGuestAccount;
           private decimal _totalAverageOrderValue;
           private decimal _averageOrderValueByNewAccount;
           private decimal _averageOrderValueByExistingAccount;
           private decimal _averageOrderValueByGuest;
           private decimal _totalDiscount;
           private decimal _averageDiscount;
           private string _averageDiscountPer;
           private string _orderDiscountedPer;
           private decimal _totalShippingCost;
           private decimal _averageShipping;
           private string _shippingPer;
           private decimal _totalTax;
           private string _taxedOrderPer;
           private decimal _averageTax;
           private string _averageTaxPer;

           [DataMember]
           public int TotlaItemsSold
           {
               get
               {
                   return this._totlaItemsSold;
               }
               set
               {
                   if (this._totlaItemsSold != value)
                   {
                       _totlaItemsSold = value;
                   }
               }
           }
           [DataMember]
           public int ItemsSoldPerOrder
           {
               get
               {
                   return this._itemsSoldPerOrder;
               }
               set
               {
                   if (this._itemsSoldPerOrder != value)
                   {
                       _itemsSoldPerOrder = value;
                   }
               }
           }
           [DataMember]
           public int TotalSKUSold
           {
               get
               {
                   return this._totalSKUSold;
               }
               set
               {
                   if (this._totalSKUSold != value)
                   {
                       _totalSKUSold = value;
                   }
               }
           }
           [DataMember]
           public int SKUSoldPerOrder
           {
               get
               {
                   return this._sKUSoldPerOrder;
               }
               set
               {
                   if (this._sKUSoldPerOrder != value)
                   {
                       _sKUSoldPerOrder = value;
                   }
               }
           }
           [DataMember]
           public int TotalOrders
           {
               get
               {
                   return this._totalOrders;
               }
               set
               {
                   if (this._totalOrders != value)
                   {
                       _totalOrders = value;
                   }
               }
           }
           [DataMember]
           public int OrdersByNewAccount
           {
               get
               {
                   return this._ordersByNewAccount;
               }
               set
               {
                   if (this._ordersByNewAccount != value)
                   {
                       _ordersByNewAccount = value;
                   }
               }
           }
           [DataMember]
           public int OrdersByExistingAccount
           {
               get
               {
                   return this._ordersByExistingAccount;
               }
               set
               {
                   if (this._ordersByExistingAccount != value)
                   {
                       _ordersByExistingAccount = value;
                   }
               }
           }
           [DataMember]
           public int OrdersByGuestAccount
           {
               get
               {
                   return this._ordersByGuestAccount;
               }
               set
               {
                   if (this._ordersByGuestAccount != value)
                   {
                       _ordersByGuestAccount = value;
                   }
               }
           }
           [DataMember]
           public decimal TotalSales
           {
               get
               {
                   return this._totalSales;
               }
               set
               {
                   if (this._totalSales != value)
                   {
                       _totalSales = value;
                   }
               }
           }
           [DataMember]
           public decimal SalesByNewAccount
           {
               get
               {
                   return this._salesByNewAccount;
               }
               set
               {
                   if (this._salesByNewAccount != value)
                   {
                       _salesByNewAccount = value;
                   }
               }
           }
           [DataMember]
           public decimal SalesByExistingAccount
           {
               get
               {
                   return this._salesByExistingAccount;
               }
               set
               {
                   if (this._salesByExistingAccount != value)
                   {
                       _salesByExistingAccount = value;
                   }
               }
           }
           [DataMember]
           public decimal SalesByGuestAccount
           {
               get
               {
                   return this._salesByGuestAccount;
               }
               set
               {
                   if (this._salesByGuestAccount != value)
                   {
                       _salesByGuestAccount = value;
                   }
               }
           }
           [DataMember]
           public decimal TotalAverageOrderValue
           {
               get
               {
                   return this._totalAverageOrderValue;
               }
               set
               {
                   if (this._totalAverageOrderValue != value)
                   {
                       _totalAverageOrderValue = value;
                   }
               }
           }
           [DataMember]
           public decimal AverageOrderValueByNewAccount
           {
               get
               {
                   return this._averageOrderValueByNewAccount;
               }
               set
               {
                   if (this._averageOrderValueByNewAccount != value)
                   {
                       _averageOrderValueByNewAccount = value;
                   }
               }
           }
           [DataMember]
           public decimal AverageOrderValueByExistingAccount
           {
               get
               {
                   return this._averageOrderValueByExistingAccount;
               }
               set
               {
                   if (this._averageOrderValueByExistingAccount != value)
                   {
                       _averageOrderValueByExistingAccount = value;
                   }
               }
           }
           [DataMember]
           public decimal AverageOrderValueByGuest
           {
               get
               {
                   return this._averageOrderValueByGuest;
               }
               set
               {
                   if (this._averageOrderValueByGuest != value)
                   {
                       _averageOrderValueByGuest = value;
                   }
               }
           }
           [DataMember]
           public decimal TotalDiscount
           {
               get
               {
                   return this._totalDiscount;
               }
               set
               {
                   if (this._totalDiscount != value)
                   {
                       _totalDiscount = value;
                   }
               }
           }
           [DataMember]
           public decimal AverageDiscount
           {
               get
               {
                   return this._averageDiscount;
               }
               set
               {
                   if (this._averageDiscount != value)
                   {
                       _averageDiscount = value;
                   }
               }
           }
           [DataMember]
           public string AverageDiscountPer
           {
               get
               {
                   return this._averageDiscountPer;
               }
               set
               {
                   if (this._averageDiscountPer != value)
                   {
                       _averageDiscountPer = value;
                   }
               }
           }
           [DataMember]
           public string OrderDiscountedPer
           {
               get
               {
                   return this._orderDiscountedPer;
               }
               set
               {
                   if (this._orderDiscountedPer != value)
                   {
                       _orderDiscountedPer = value;
                   }
               }
           }
           [DataMember]
           public decimal TotalShippingCost
           {
               get
               {
                   return this._totalShippingCost;
               }
               set
               {
                   if (this._totalShippingCost != value)
                   {
                       _totalShippingCost = value;
                   }
               }
           }
           [DataMember]
           public decimal AverageShipping
           {
               get
               {
                   return this._averageShipping;
               }
               set
               {
                   if (this._averageShipping != value)
                   {
                       _averageShipping = value;
                   }
               }
           }
           [DataMember]
           public string ShippingPer
           {
               get
               {
                   return this._shippingPer;
               }
               set
               {
                   if (this._shippingPer != value)
                   {
                       _shippingPer = value;
                   }
               }
           }
           [DataMember]
           public decimal TotalTax
           {
               get
               {
                   return this._totalTax;
               }
               set
               {
                   if (this._totalTax != value)
                   {
                       _totalTax = value;
                   }
               }
           }
           [DataMember]
           public string TaxedOrderPer
           {
               get
               {
                   return this._taxedOrderPer;
               }
               set
               {
                   if (this._taxedOrderPer != value)
                   {
                       _taxedOrderPer = value;
                   }
               }
           }
           [DataMember]
           public decimal AverageTax
           {
               get
               {
                   return this._averageTax;
               }
               set
               {
                   if (this._averageTax != value)
                   {
                       _averageTax = value;
                   }
               }
           }
           [DataMember]
           public string AverageTaxPer
           {
               get
               {
                   return this._averageTaxPer;
               }
               set
               {
                   if (this._averageTaxPer != value)
                   {
                       _averageTaxPer = value;
                   }
               }
           }







      
    }
}
