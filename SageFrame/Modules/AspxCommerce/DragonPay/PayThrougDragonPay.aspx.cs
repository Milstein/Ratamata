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
using System.Linq;
using System.Web;
using AspxCommerce.Core;
using AspxCommerce.DragonPay;
using SageFrame.Framework;
using System.IO;

public partial class Modules_AspxCommerce_DragonPayGateWay_PayThrougDragonPay : PageBase
{
    #region Variables
    private static string MainCurrency, Spath, itemIds, couponCode, merchantId, txnId;
    private static string currency = string.Empty, description = string.Empty, email = string.Empty, secretkey = string.Empty, paymentSwitchUrl = string.Empty, SelectedCurrency = string.Empty;
    private static double rate = 1, amount = 0.00;
    AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["DragonPayData"] != null)
                {
                    string[] data = Session["DragonPayData"].ToString().Split('#');
                    aspxCommonObj.StoreID = int.Parse(data[0].ToString());
                    aspxCommonObj.PortalID = int.Parse(data[1].ToString());
                    aspxCommonObj.UserName = data[2];
                    aspxCommonObj.CustomerID = int.Parse(data[3].ToString());
                    aspxCommonObj.SessionCode = data[4].ToString();
                    aspxCommonObj.CultureName = data[5];
                    itemIds = data[6];
                    couponCode = data[7];
                    Spath = ResolveUrl("~/Modules/AspxCommerce/AspxCommerceServices/");

                    TansactionProcess();
                }
                else
                {
                    lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
                    clickhere.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
            clickhere.Visible = false;
            ProcessException(ex);
        }

    }

    private void TansactionProcess()
    {
        try
        {
            SetRate();
            if (rate != 0)
            {
                // Set Transaction Currency code in session to save transactionLog table
                Session["SelectedCurrency"] = SelectedCurrency;
                PaymentProceed();
            }
            else
            {
                lblnotity.Text = "Something goes wrong, hit refresh or go back to checkout";
                clickhere.Visible = false;
            }

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }

    private void SetRate()
    {
        AspxCommerceWebService aws = new AspxCommerceWebService();
        StoreSettingConfig ssc = new StoreSettingConfig();
        var ph = new DragonPayHandler();
        DragonPaySettingInfo sf = ph.GetDragonPaySettings(aspxCommonObj.StoreID, int.Parse(Session["GateWay"].ToString()), aspxCommonObj.PortalID);
        MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, aspxCommonObj.StoreID, aspxCommonObj.PortalID, aspxCommonObj.CultureName);
        
        if (DragonPaySupportedCurrency.dragonPaySupportedCurrency.Split(',').Where(s => string.Compare(MainCurrency, s, true) == 0).Count() > 0)
        {
            if (sf.DragonPayCurrencyCode.Trim().ToLower() == MainCurrency.Trim().ToLower())
            {
                rate = 1;
                SelectedCurrency = sf.DragonPayCurrencyCode;
            }
            else
            {
                rate = aws.GetCurrencyRate(MainCurrency, sf.DragonPayCurrencyCode);
                SelectedCurrency = sf.DragonPayCurrencyCode;
                if (rate == 1)
                {
                    rate = 0;
                }
            }

        }
        else
        {
            //rate = aws.GetCurrencyRateOnChange(aspxCommonObj, MainCurrency, "PHP", "fil-PH");
            rate = aws.GetCurrencyRate(MainCurrency, "PHP");
            MainCurrency = "PHP";
            SelectedCurrency = MainCurrency;

            /* Some time if selected currency does not exist in currency table then it returns 1, 
               if we take 1 as rate then it will convert same as previous amount
                So avoid Transaction by making it 0 */
            if (rate == 1)
            {
                rate = 0;
            }
        }
    }

    private void PaymentProceed()
    {
        var ph = new DragonPayHandler();
        DragonPaySettingInfo sf = ph.GetDragonPaySettings(aspxCommonObj.StoreID, int.Parse(Session["GateWay"].ToString()), aspxCommonObj.PortalID);
        var orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
        string itemsIds = string.Empty;

        try
        {
            //Assign Common values           
                foreach (var item in orderdata2.LstOrderItemsInfo)
                {
                    itemsIds += item.ItemID + "&" + item.Quantity + "&" + orderdata2.ObjOrderDetails.OrderID + "&" + item.Variants + ",";
                }        

            amount = double.Parse(Session["GrandTotalAll"].ToString()) * rate;
            string tAmount = String.Format("{0:0.00}", amount);
            merchantId = sf.DragonPayMerchantID;
            txnId = orderdata2.ObjOrderDetails.InvoiceNumber;
            currency = sf.DragonPayCurrencyCode;
            description = itemsIds;
            email = orderdata2.ObjBillingAddressInfo.EmailAddress;
            secretkey = sf.DragonPaySecretKey;

            Session["AmountCoupon"] = tAmount + "#" + couponCode;

            paymentSwitchUrl = bool.Parse(sf.IsTestDragonPay)
                              ? "http://test.dragonpay.ph/Pay.aspx"
                              : "https://api.dragonpay.ph/Pay.aspx";

            string message = String.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}",
                            merchantId,
                            txnId,
                            tAmount,
                            currency,
                            description,
                            email,
                            secretkey);
            String digest = ph.GetSHA1Digest(message);

            String redirectString =
                                    String.Format("{0}?merchantid={1}&txnid={2}&amount={3}&ccy={4}&description={5}&email={6}&digest={7}",
                                    paymentSwitchUrl,
                                    merchantId,
                                    txnId,
                                    tAmount,
                                    currency,
                                    Server.UrlEncode(description),
                                    Server.UrlEncode(email),
                                    digest);

            //StreamWriter ss = new StreamWriter(Server.MapPath("~/POST.txt"));
            //ss.WriteLine("POST");
            //ss.WriteLine("redirectString=" + redirectString);
            //ss.Close();

            // send browser to Payment Switch
            Response.Redirect(redirectString, false);
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
        if (Session["DragonPayData"] != null)
        {
            PaymentProceed();
        }
    }



}
