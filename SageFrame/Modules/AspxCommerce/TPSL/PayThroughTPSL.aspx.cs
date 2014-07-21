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
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame;
using SageFrame.Framework;
using System.Web.Services;
using System.IO;
using AspxCommerce.Core;
using System.Text;
using AspxCommerce.TPSL;

public partial class Modules_AspxCommerce_TPSLGateWay_PayThroughTPSL : PageBase
{
    public string aspxPaymentModulePath;
    public int storeID;
    public int portalID;
    public int customerID;
    public string userName;
    public string cultureName, MainCurrency;
    public string sessionCode = string.Empty;
    public string BankCode = string.Empty;
    public int TPSL;
    public string Spath, itemIds, couponCode;
    public double rate;
    COM.TPSLUtil1 objTPSLUtil1 = new COM.TPSLUtil1();
    COM.CheckSumRequestBean objCheckSumRequestBean = new COM.CheckSumRequestBean();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["TPSLData"] != null)
            {
                string[] data = Session["TPSLData"].ToString().Split('#');
                storeID = int.Parse(data[0].ToString());
                portalID = int.Parse(data[1].ToString());
                userName = data[2];
                customerID = int.Parse(data[3].ToString());
                sessionCode = data[4].ToString();
                cultureName = data[5];
                BankCode = data[6].Trim();
                itemIds = data[7];
                couponCode = data[8];
                Spath = ResolveUrl("~/Modules/AspxCommerce/AspxCommerceServices/");
                StoreSettingConfig ssc = new StoreSettingConfig();
                MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, storeID, portalID, cultureName);
                if (TPSLSupportedCurrency.tpslSupportedCurrency.Split(',').Where(s => string.Compare(MainCurrency, s, true) == 0).Count() > 0)
                {
                    rate = 1;
                }
                else
                {
                    AspxCommerceWebService aws = new AspxCommerceWebService();
                    rate = aws.GetCurrencyRate(MainCurrency, "INR");
                    MainCurrency = "USD";
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
        TPSLWCFService pw = new TPSLWCFService();
        List<TPSLSettingInfo> sf;
        OrderDetailsCollection orderdata2 = new OrderDetailsCollection();
        orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];        
        string itemidsWithVar = "";
        foreach (var item in orderdata2.LstOrderItemsInfo)
        {
            itemidsWithVar += item.ItemID + "&" + item.Quantity + "&" + orderdata2.ObjOrderDetails.OrderID + "&" + item.Variants + ",";
        }
       

        try
        {
            sf = pw.GetAllTPSLSetting(int.Parse(Session["GateWay"].ToString()), storeID, portalID);
            
            if (bool.Parse(sf[0].IsTestTPSL.ToString()))
            {
                FileStream fs = null;
                string fileLoc = Server.MapPath("Property\\" + "MerchantDetails_sharedhosting.property");
                StreamWriter sw = new StreamWriter(fileLoc);

                if (!File.Exists(fileLoc))
                {
                    fs = File.Create(fileLoc);
                }

                if (File.Exists(fileLoc))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("BillerId=" + sf[0].BillerID);
                    sb.AppendLine("ResponseUrl=" + sf[0].ResponseUrl);
                    sb.AppendLine("CRN=" + sf[0].Currency);                   
                    sb.AppendLine("Logfile=");                   
                    sb.Append("CheckSumKey=" + sf[0].CheckSumKey);
                    sw.Write(sb);

                }
                sw.Close();

                objCheckSumRequestBean.MerchantTranId = sf[0].BillerID.ToString(); //MERCHANT unique reference number
                //objCheckSumRequestBean.MarketCode = couponCode; //storeID.ToString(); //BillerID(L1803) or any additional values(of upto 20 characters, like customer’s name or contact number
               // objCheckSumRequestBean.AccountNo = "5#4,3#2"; //customerID.ToString();   //CustomeFields
                objCheckSumRequestBean.MarketCode = storeID.ToString(); //BillerID(L1803) or any additional values(of upto 20 characters, like customer’s name or contact number
                objCheckSumRequestBean.AccountNo = customerID.ToString();   //CustomeFields
                objCheckSumRequestBean.Amt = (Convert.ToDouble(Session["GrandTotalAll"]) * rate).ToString(); ////Total Amount
                objCheckSumRequestBean.BankCode = BankCode;                     //Dropdownlist Value
                objCheckSumRequestBean.PropertyPath = Server.MapPath("Property\\" + "MerchantDetails_sharedhosting.property");
                

                string strMsg = objTPSLUtil1.transactionRequestMessage(objCheckSumRequestBean);
                Session["myString"] = strMsg;
                if (!strMsg.Equals(""))
                {
                    Response.Redirect("https://www.tpsl-india.in/PaymentGateway/TransactionRequest.jsp?msg=" + strMsg, false);
                    //Response.Redirect("https://www.tekprocess.co.in/PaymentGateway/TransactionRequest.jsp?msg=" + strMsg, false);
                    
                }
                HttpContext.Current.Session["IsTestTPSL"] = true;
            }
            else
            {
                FileStream fs2 = null;
                string fileLoc2 = Server.MapPath("Checksum\\" + sf[0].LogfileName + ".txt");
                StreamWriter sw2 = new StreamWriter(fileLoc2);
                if (!File.Exists(fileLoc2))
                {
                    fs2 = File.Create(fileLoc2);
                }
                sw2.Close();

                FileStream fs = null;
                string fileLoc = Server.MapPath("Property\\" + "MerchantDetails.property");
                StreamWriter sw = new StreamWriter(fileLoc);               
                if (!File.Exists(fileLoc))
                {
                    fs = File.Create(fileLoc);
                }

                if (File.Exists(fileLoc))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("BillerId=" + sf[0].BillerID);
                    sb.AppendLine("ResponseUrl=" + sf[0].ResponseUrl);
                    sb.AppendLine("CRN=" + sf[0].Currency);                    
                    sb.AppendLine("Logfile=" + Server.MapPath("Checksum\\" + sf[0].LogfileName + ".txt")); 
                    sb.Append("CheckSumKey=" + sf[0].CheckSumKey);
                    sw.Write(sb);

                }              
                                
                sw.Close();

                objCheckSumRequestBean.MerchantTranId = sf[0].BillerID.ToString(); //MERCHANT unique reference number
                //objCheckSumRequestBean.MarketCode = couponCode; //storeID.ToString(); //BillerID(L1803) or any additional values(of upto 20 characters, like customer’s name or contact number
                //objCheckSumRequestBean.AccountNo = couponCode; //customerID.ToString();   //CustomeFields
                objCheckSumRequestBean.MarketCode = storeID.ToString(); //BillerID(L1803) or any additional values(of upto 20 characters, like customer’s name or contact number
                objCheckSumRequestBean.AccountNo = customerID.ToString();   //CustomeFields
                objCheckSumRequestBean.Amt = (Convert.ToDouble(Session["GrandTotalAll"]) * rate).ToString(); ////Total Amount
                objCheckSumRequestBean.BankCode = BankCode;                    //Dropdownlist Value
                objCheckSumRequestBean.PropertyPath = Server.MapPath("Property\\" + "MerchantDetails.property");

                string strMsg = objTPSLUtil1.transactionRequestMessage(objCheckSumRequestBean);
                Session["myString"] = strMsg;
                if (!strMsg.Equals(""))
                {
                    Response.Redirect("https://www.tpsl-india.in/PaymentGateway/TransactionRequest.jsp?msg=" + strMsg, false);
                }

                HttpContext.Current.Session["IsTestTPSL"] = false;

            }
            //string ids = Session["OrderID"].ToString() + "#" + storeID + "#" + portalID + "#" + userName + "#" + customerID + "#" + sessionCode + "#" + Session["IsTestTPSL"].ToString() + "#" + Session["GateWay"].ToString();           
              

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
