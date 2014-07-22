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
using System.Net;
using System.IO;
using AspxCommerce.Core;
using SageFrame.Framework;
using System.Collections;
using SageFrame.Web;
using AspxCommerce.TPSL;
using COM;

public partial class Modules_AspxCommerce_AspxPaymentSuccess_TPSLSuccess : BaseAdministrationUserControl
{
    string paymentStatus;
    string authToken, txToken, query;
    string strResponse;
    string transID, invoice, addressPath;
    bool IsUseFriendlyUrls = true;
    public static string merid, subscriberid, txnrefno, bankrefno, txnamt, bankid, bankmerid, tcntype, currencyname, temcode, securitytype;
    public static string securityid, securitypass, txndate, authstatus, settlementtype, addtninfo1, addtninfo2, addtninfo3;
    public static string addtninfo4, addtninfo5, addtninfo6, addtninfo7, errorstatus, errordesc, checksum;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
                try
                {
                    COM.CheckSumResponseBean objCheckSumResponseBean = new COM.CheckSumResponseBean();
                    TPSLUtil1 objTPSLUtil1 = new TPSLUtil1();

                    //Response.Write("Originally Generated String===========>" + Convert.ToString(Session["myString"]) + "End String===========>");
                    //String strResponseMsg = "01|9892381157|16462992|NA|5.00|10|NA|NA|INR|NA|NA|NA|NA|19-04-2011 12:31:40|0399|NA|12345|100000001|NA|NA|NA|NA|NA|NA|NA|777629931425";//Request["msg"] == null ? "" : Request["msg"].Trim();
                    String strResponseMsg = Request["msg"] == null ? "" : Request["msg"].Trim();

                    //Response.Write("strResponseMsg===========>" + strResponseMsg);

                    String[] token = strResponseMsg.Split('|');

                    if (token.Length == 26)
                    {
                        merid = token[0].ToString();
                        subscriberid = token[1].ToString();
                        txnrefno = token[2].ToString();
                        bankrefno = token[3].ToString();
                        txnamt = token[4].ToString();
                        bankid = token[5].ToString();
                        bankmerid = token[6].ToString();
                        tcntype = token[7].ToString();
                        currencyname = token[8].ToString();
                        temcode = token[9].ToString();
                        securitytype = token[10].ToString();
                        securityid = token[11].ToString();
                        securitypass = token[12].ToString();
                        txndate = token[13].ToString();
                        authstatus = token[14].ToString();
                        settlementtype = token[15].ToString();
                        addtninfo1 = token[16].ToString();
                        addtninfo2 = token[17].ToString();  //Custome Fields
                        addtninfo3 = token[18].ToString();
                        addtninfo4 = token[19].ToString();
                        addtninfo5 = token[20].ToString();
                        addtninfo6 = token[21].ToString();
                        addtninfo7 = token[22].ToString();
                        errorstatus = token[23].ToString();
                        errordesc = token[24].ToString();
                        checksum = token[25].ToString();
                    }
                    else
                    {
                        //Response.Write("Inside ELSE of Response***********");
                        return;
                    }

                    objCheckSumResponseBean.MSG = strResponseMsg;
                    objCheckSumResponseBean.PropertyPath = Server.MapPath("~/Modules/AspxCommerce/TPSL/Property/" + "MerchantDetails_sharedhosting.property");

                    string strCheckSumValue = objTPSLUtil1.transactionResponseMessage(objCheckSumResponseBean);

                    //Response.Write("strCheckSumValue***********" + strCheckSumValue);

                    if (authstatus.Contains("0300"))
                    {
                        paymentStatus = "Success";
                    }
                    else
                    {
                        paymentStatus = "Failed";
                    }

                   
                    if (!strCheckSumValue.Equals(""))
                    {                      

                        if (!checksum.Equals(strCheckSumValue))
                        {
                            authstatus = "0399";
                            errordesc = "Transaction Failed due to checksum mismatch";
                            lblerror.Text = errordesc;
                        }
                    }
                    else
                    {
                        authstatus = "0399";
                        errordesc = "Transaction Failed due to invalid parameters";
                        lblerror.Text = errordesc;
                    }

                    try
                    {

                    SageFrameConfig sfConfig = new SageFrameConfig();
                    IsUseFriendlyUrls = sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
                    string sageRedirectPath = string.Empty;
                    if (IsUseFriendlyUrls)
                    {
                        if (GetPortalID > 1)
                        {
                            sageRedirectPath = ResolveUrl("~/portal/" + GetPortalSEOName + "/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                            addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/portal/" + GetPortalSEOName + "/";
                        }
                        else
                        {
                            sageRedirectPath = ResolveUrl("~/" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + ".aspx");
                            addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
                        }
                    }
                    else
                    {
                        sageRedirectPath = ResolveUrl("{~/Default.aspx?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage));
                    }

                    Image imgProgress = (Image)UpdateProgress1.FindControl("imgPrgress");
                    if (imgProgress != null)
                    {
                        imgProgress.ImageUrl = GetTemplateImageUrl("ajax-loader.gif", true);
                    }
                    hlnkHomePage.NavigateUrl = sageRedirectPath;

                    if (Session["OrderID"] != null)
                    {
                        int storeID = int.Parse(GetStoreID.ToString());
                        int portalID = int.Parse(GetPortalID.ToString());
                        string userName = GetUsername.ToString();
                        int customerID = int.Parse(GetCustomerID.ToString());
                        OrderDetailsCollection orderdata = new OrderDetailsCollection();
                        List<TPSLSettingInfo> setting;
                        if (HttpContext.Current.Session["OrderCollection"] != null)
                        {

                            orderdata = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                            invoice = orderdata.ObjOrderDetails.InvoiceNumber.ToString();
                            TPSLWCFService pw = new TPSLWCFService();
                            int i = int.Parse(orderdata.ObjOrderDetails.PaymentGatewayTypeID.ToString());
                            setting = pw.GetAllTPSLSetting(i, storeID, portalID);

                        }

                        string payerEmail = orderdata.ObjBillingAddressInfo.EmailAddress;
                        string receiverEmail = "";
                        string transID = txnrefno.ToString();
                        int orderID = (int)Session["OrderID"];
                        string sessionCode = HttpContext.Current.Session.SessionID;
                        int pgid = orderdata.ObjOrderDetails.PaymentGatewayTypeID;

                        TransactionLogInfo tinfo = new TransactionLogInfo();
                        TransactionLog Tlog = new TransactionLog();

                        tinfo.TransactionID = transID;
                        tinfo.AuthCode = authstatus;
                        tinfo.TotalAmount = decimal.Parse(txnamt);
                        tinfo.ResponseCode = errorstatus;
                        tinfo.ResponseReasonText = errordesc;
                        tinfo.OrderID = orderID;
                        tinfo.StoreID = storeID;
                        tinfo.PortalID = portalID;
                        tinfo.AddedBy = userName;
                        tinfo.CustomerID = customerID;
                        tinfo.SessionCode = sessionCode;
                        tinfo.PaymentGatewayID = pgid;
                        tinfo.PaymentStatus = paymentStatus;
                        tinfo.PayerEmail = payerEmail;
                        tinfo.CreditCard = "";
                        tinfo.RecieverEmail = receiverEmail;
                        Tlog.SaveTransactionLog(tinfo);


                        if (paymentStatus.StartsWith("Success"))
                        {
                            //Transaction Successful
                            try
                            {

                                TPSLHandler.ParseIPN(orderID, transID, paymentStatus, storeID, portalID, userName, customerID, sessionCode);
                                AspxOrderDetails orderUpdate = new AspxOrderDetails();
                                orderUpdate.UpdateItemQuantity(orderdata);
                                CartManageSQLProvider cms = new CartManageSQLProvider();
                                cms.ClearCartAfterPayment(customerID, sessionCode, storeID, portalID);

                                if (HttpContext.Current.Session["OrderCollection"] != null && HttpContext.Current.Session["OrderCollection"] != "")
                                {
                                    try
                                    {
                                        EmailTemplate.SendEmailForOrder(portalID, orderdata, addressPath, TemplateName, transID);

                                        ClearAllSession();
                                    }
                                    catch (Exception ex)
                                    {
                                        ProcessException(ex);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ProcessException(ex);
                            }

                            lblTransaction.Text = transID;
                            lblInvoice.Text = invoice;
                            lblPaymentMethod.Text = "TPSL India";
                            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy ");
                            lblerror.Text = paymentStatus;
                        }


                        ClearAllSession();

                    }
                    else
                    {
                        Response.Redirect(sageRedirectPath, false);
                    }
                    IncludeLanguageJS();
                }
                catch (Exception ex)
                {

                    ProcessException(ex);
                }


                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
                
        }
    }

    private void ClearAllSession()
    {
        Session.Remove("OrderCollection");

        if (Session["IsFreeShipping"] != null && Session["IsFreeShipping"] != "")
        {
            HttpContext.Current.Session.Remove("IsFreeShipping");

        }
        if (Session["OrderID"] != null && Session["OrderID"] != "")
        {
            HttpContext.Current.Session.Remove("OrderID");

        }
        if (Session["DiscountAmount"] != null && Session["DiscountAmount"] != "")
        {
            HttpContext.Current.Session.Remove("DiscountAmount");

        }
        if (Session["CouponCode"] != null && Session["CouponCode"] != "")
        {
            HttpContext.Current.Session.Remove("CouponCode");

        }
        if (Session["CouponApplied"] != null && Session["CouponApplied"] != "")
        {
            HttpContext.Current.Session.Remove("CouponApplied");
        }
        if (Session["DiscountAll"] != null && Session["DiscountAll"] != "")
        {
            HttpContext.Current.Session.Remove("DiscountAll");
        }
        if (Session["TaxAll"] != null && Session["TaxAll"] != "")
        {
            HttpContext.Current.Session.Remove("TaxAll");
        }
        if (Session["ShippingCostAll"] != null && Session["ShippingCostAll"] != "")
        {
            HttpContext.Current.Session.Remove("ShippingCostAll");
        }
        if (Session["GrandTotalAll"] != null && Session["GrandTotalAll"] != "")
        {
            HttpContext.Current.Session.Remove("GrandTotalAll");
        }
        if (Session["Gateway"] != null && Session["Gateway"] != "")
        {
            HttpContext.Current.Session.Remove("Gateway");
        }
        if (Session["transaction_id"] != null && Session["transaction_id"] != "")
        {
            HttpContext.Current.Session.Remove("transaction_id");
        }
    }
}
