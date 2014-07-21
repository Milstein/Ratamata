/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 20011-2012 by AspxCommerce
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SageFrame.Framework;
using System.Web.Services;
using AspxCommerce.eSewa;
using AspxCommerce.Core;
using System.Text;
using System.Globalization;

public partial class Modules_AspxCommerce_eSewaGateWay_PayThrougheSewa : PageBase
{
    public string aspxPaymentModulePath;
    public int storeID;
    public int portalID;
    public int customerID;
    public string userName;
    public string cultureName, MainCurrency;
    public string sessionCode = string.Empty;
    public int eSewa;
    public string Spath, itemIds, couponCode;
    public double rate;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["eSewaData"] != null)
            {
                string[] data = Session["eSewaData"].ToString().Split('#');
                storeID = int.Parse(data[0].ToString());
                portalID = int.Parse(data[1].ToString());
                userName = data[2];
                customerID = int.Parse(data[3].ToString());
                sessionCode = data[4].ToString();
                cultureName = data[5];
                itemIds = data[6];
                couponCode = data[7];
                Spath = ResolveUrl("~/Modules/AspxCommerce/AspxCommerceServices/");

                StoreSettingConfig ssc = new StoreSettingConfig();
                MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, storeID, portalID, cultureName);
                if (eSewaSupportedCurrency.eSewasupportedCurrency.Split(',').Where(s => string.Compare(MainCurrency, s, true) == 0).Count() > 0)
                {
                    rate = 1;
                }
                else
                {
                    AspxCommerceWebService aws = new AspxCommerceWebService();
                    rate = aws.GetCurrencyRate(MainCurrency, "NPR");
                    MainCurrency = "NPR";
                }

                LoadSetting();
            }
            else
            {
                lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
                clickhere.Visible = false;
            }


        }
        catch (Exception ex)
        {
            lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
            clickhere.Visible = false;
            ProcessException(ex);
        }

    }

    [WebMethod]
    public static void SetSessionVariable(string key, string value)
    {
        HttpContext.Current.Session[key] = value;

    }
    public void LoadSetting()
    {
        eSewaWCFService pw = new eSewaWCFService();
        List<eSewaSettingInfo> sf;
        OrderDetailsCollection orderdata2 = new OrderDetailsCollection();
        orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
        string itemidsWithVar = "";
        string orderID = orderdata2.ObjOrderDetails.OrderID.ToString();        
        foreach (var item in orderdata2.LstOrderItemsInfo)
        {
            itemidsWithVar += item.ItemID + "&" + item.Quantity + "&" + orderdata2.ObjOrderDetails.OrderID + "&" + item.Variants + ",";
        }
        double amountTotal = double.Parse(Session["GrandTotalAll"].ToString()) * rate;
        decimal grandTotal = decimal.Parse(amountTotal.ToString(CultureInfo.InvariantCulture));
        decimal amountSubTotal = 0;
        decimal amtSubTotal = 0;
        decimal taxSubTotal = 0;
        decimal shipping = Convert.ToDecimal(Convert.ToDouble(Session["ShippingCostAll"].ToString()) * rate);
        decimal shippingCost = decimal.Parse(shipping.ToString(CultureInfo.InvariantCulture));       
        decimal discountAmount = 0;
        decimal couponDiscountAmount = 0;

        if (orderdata2.ObjOrderDetails.TaxTotal != 0)
        {
            decimal tax = Convert.ToDecimal(Convert.ToDouble(orderdata2.ObjOrderDetails.TaxTotal) * rate);
            taxSubTotal = decimal.Parse(tax.ToString(CultureInfo.InvariantCulture)); 
        }
        if (orderdata2.ObjOrderDetails.DiscountAmount != 0)
        {            
            decimal discount = Convert.ToDecimal(Convert.ToDouble(orderdata2.ObjOrderDetails.DiscountAmount) * rate);
            discountAmount = decimal.Parse(discount.ToString(CultureInfo.InvariantCulture));
        }
        if (orderdata2.ObjOrderDetails.CouponDiscountAmount != 0)
        {           
         decimal couponDiscount = Convert.ToDecimal(Convert.ToDouble(orderdata2.ObjOrderDetails.CouponDiscountAmount) * rate);
         couponDiscountAmount = decimal.Parse(couponDiscount.ToString(CultureInfo.InvariantCulture));
        }

        amtSubTotal = grandTotal - shippingCost - taxSubTotal + discountAmount + couponDiscountAmount;
        amountSubTotal = amtSubTotal - discountAmount - couponDiscountAmount;
      
        string postURL = string.Empty;

        try
        {
            sf = pw.GetAlleSewaSetting(int.Parse(Session["GateWay"].ToString()), storeID, portalID);
            if (bool.Parse(sf[0].IsTesteSewa.ToString()))
            {
                postURL = "http://esewa.f1dev.com/epay/main";
               // postURL = "http://www.eSewa.com/app/test_payment.pl";              
                HttpContext.Current.Session["IsTesteSewa"] = true;
            }
            else
            {
                postURL = "https://esewa.com.np/epay/transrec";
                HttpContext.Current.Session["IsTesteSewa"] = false;
            }
            string ids = Session["OrderID"].ToString() + "#" + storeID + "#" + portalID + "#" + userName + "#" + customerID + "#" + sessionCode + "#" + Session["IsTesteSewa"].ToString() + "#" + Session["GateWay"].ToString();
            var sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""payment""].submit()' >");
            sb.AppendFormat("<form name='payment' action='{0}' method='post'><div sytle='display:none;'>", postURL);

            sb.AppendFormat("<input type=\"hidden\" name=\"tAmt\" value=\"" + Math.Round(grandTotal, 2) + "\"  />");
            sb.AppendFormat("<input type=\"hidden\" name=\"amt\" value=\"" + Math.Round(amountSubTotal, 2) + "\"  />");
            sb.AppendFormat("<input type=\"hidden\" name=\"txAmt\" value=\"" + Math.Round(taxSubTotal, 2) + "\"  />");
            sb.AppendFormat("<input type=\"hidden\" name=\"psc\" value=\"" + 0 + "\"  />");
            sb.AppendFormat("<input type=\"hidden\" name=\"pdc\" value=\"" + Math.Round(shippingCost, 2) + "\"  />");
            sb.AppendFormat("<input type=\"hidden\" name=\"scd\" value=\"" + sf[0].eSewaMerchantID + "\" />");
            sb.AppendFormat("<input type=\"hidden\" name=\"pid\" value=\"" + orderID + "\" />"); 
            sb.AppendFormat("<input type=\"hidden\" name=\"su\" value=\"" + sf[0].eSewaSuccessURL + "\" />");
            sb.AppendFormat("<input type=\"hidden\" name=\"fu\" value=\"" + sf[0].eSewaFailureURL + "\" />");
            sb.Append("</div></form>");
            sb.Append("</body>");
            sb.Append("</html>");
            Response.Write(sb.ToString());
            HttpContext.Current.ApplicationInstance.CompleteRequest();           
        }
        catch (Exception ex)
        {
            lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
            clickhere.Visible = false;
            ProcessException(ex);
        }

    }
    protected void clickhere_Click(object sender, EventArgs e)
    {
        LoadSetting();
    }
}
