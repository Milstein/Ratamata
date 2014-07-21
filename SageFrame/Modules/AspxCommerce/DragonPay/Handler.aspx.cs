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
using System.IO;
using System.Web;
using AspxCommerce.Core;
using AspxCommerce.DragonPay;
using SageFrame.Framework;

public partial class DragonPay_Handler : PageBase
{
    #region Variables
    private static string postURL, transID, invoice, addressPath, pgid, couponCode, paymentStatus, responseCode, selectedCurrency, MainCurrency;
    private static string ids, txnid, amount, email, refno, status, msg, digest, msgValue, itemsIds, couponcode, appPath, filePath, pStatus, rrText;
    private static bool IsUseFriendlyUrls = true, isVerified = false;
    private static int orderId;
    AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
    DragonPayHandler ph = new DragonPayHandler();
   
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["refno"]))
            {
                bool RefNoExists = ph.RefNumberExist(Request["refno"].ToString());
                if (!RefNoExists)
                {
                    SetValuesFromString();
                    if (Session["OrderCollection"] != null)
                       {
                           TransCommit();
                       }
                    //if (Session["IsAppointment"] != null)
                    //{
                    //    TransCommitForAppointment();
                    //}
                }
                else
                {
                    refno = Request["refno"].ToString();
                    OTCTransCommit();
                }
            }
          
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void SetValuesFromString()
    {
        try
        {
             //Assgin values retrieved from response server
            txnid = Request["txnid"].ToString();
            refno = Request["refno"].ToString();
            status = Request["status"].ToString();
            msg = Request["message"].ToString();
            digest = Request["digest"].ToString();
            transID = refno;
      
            //assigning server address path
            if (GetPortalID > 1)
            {
                addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/portal/" + GetPortalSEOName + "/";
            }
            else
            {
                addressPath = HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + "/";
            }

            //Get transaction currency code
            StoreSettingConfig ssc = new StoreSettingConfig();
            MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetStoreID, GetPortalID, GetCurrentCultureName);
            if (Session["SelectedCurrency"] != null && Session["SelectedCurrency"] != "")
            {
                selectedCurrency = Session["SelectedCurrency"].ToString();
            }
            else
            {
                selectedCurrency = MainCurrency;
            }

            //Assign Common values
            if (Session["OrderCollection"] != null)
            {
                var orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];

                orderId = orderdata2.ObjOrderDetails.OrderID;
                email = orderdata2.ObjBillingAddressInfo.EmailAddress;
                pgid = orderdata2.ObjOrderDetails.PaymentGatewayTypeID.ToString();
            
             

                aspxCommonObj.CustomerID = orderdata2.ObjOrderDetails.CustomerID;
                aspxCommonObj.SessionCode = orderdata2.ObjOrderDetails.SessionCode;
                aspxCommonObj.StoreID = orderdata2.ObjCommonInfo.StoreID;
                aspxCommonObj.PortalID = orderdata2.ObjCommonInfo.PortalID;
                aspxCommonObj.CultureName = orderdata2.ObjCommonInfo.CultureName;
                aspxCommonObj.UserName = orderdata2.ObjCommonInfo.AddedBy;

                foreach (var item in orderdata2.LstOrderItemsInfo)
                {
                    itemsIds += item.ItemID + "&" + item.Quantity + "&" + orderdata2.ObjOrderDetails.OrderID + "&" + item.Variants + ",";
                }
            }
            //if (Session["DragonPayData"] != null)
            //{
            //    string[] data = Session["DragonPayData"].ToString().Split('#');
            //    aspxCommonObj.StoreID = int.Parse(data[0].ToString());
            //    aspxCommonObj.PortalID = int.Parse(data[1].ToString());
            //    aspxCommonObj.UserName = data[2];
            //    aspxCommonObj.CustomerID = int.Parse(data[3].ToString());
            //    aspxCommonObj.SessionCode = data[4].ToString();
            //    aspxCommonObj.CultureName = data[5];
            //}
            //if (Session["AppointmentData"] != null)
            //{
            //    var scheduleObj = (SaveSeminarShedule) HttpContext.Current.Session["AppointmentData"];
            //    scheduleObj.TransactionID = refno;
            //    pgid = scheduleObj.PaymentMethodID.ToString();
            //    email = scheduleObj.Email;
            //    amount = scheduleObj.Amount.ToString();
            //    orderId = scheduleObj.SeminarBookedID;
            //}
            //Assign Coupon Values
            if (Session["AmountCoupon"] != null)
            {
                string idsFields = HttpContext.Current.Session["AmountCoupon"].ToString();
                string[] customFields = idsFields.Split('#');
                amount = customFields[0];
                couponcode = customFields[1];
            }

            //Extracting message code from response
            if (msg.Length > 0)
            {
                int start = msg.IndexOf("[") + 1;
                int end = msg.IndexOf("]");
                msgValue = msg.Substring(start, end - start);
            }

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }

    private void TransCommit()
    {
        if (!string.IsNullOrEmpty(status))
        {
            try
            {

                DragonPaySettingInfo sf = ph.GetDragonPaySettings(aspxCommonObj.StoreID, int.Parse(pgid), aspxCommonObj.PortalID);
                String message = ph.GetSHA1Digest(String.Format("{0}:{1}:{2}:{3}:{4}", txnid, refno, status, msg, sf.DragonPaySecretKey));
                pStatus = ph.PaymentStatusCodesDescription(status);
                rrText = ph.PaymentErrorCodesDescription(msgValue);

                if (message == digest)
                {
                    isVerified = true;
                    if (status.Equals("S")) //transaction success
                    {
                        UpdateCart();
                        SaveTransactionLog();

                        if (HttpContext.Current.Session["OrderCollection"] != null)
                        {
                            var orderdata = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];
                            EmailTemplate.SendEmailForOrder(GetPortalID, orderdata, addressPath, TemplateName, transID);
                        }

                        Response.Redirect("~/DragonPaySuccess.aspx?Status=" + pStatus, false);
                    }
                    else
                    {
                        // During Payment Error/Cancelled
                        SaveTransactionLog();
                        Response.Redirect("~/DragonPayError.aspx?Status=" + pStatus, false);

                    }
                }
                else
                {
                    // display some error message and abort processing
                    SaveTransactionLog();
                    Response.Redirect("~/DragonPayError.aspx?Status=" + pStatus, false);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

    }

    private void UpdateCart()
    {
        try
        {
            DragonPayHandler objHandler = new DragonPayHandler();
            CartManageSQLProvider cms = new CartManageSQLProvider();

            objHandler.UpdateOrderStatus(orderId, refno, pStatus, aspxCommonObj);
            objHandler.UpdateItemQuantity(itemsIds, couponcode, aspxCommonObj);
            cms.ClearCartAfterPayment(aspxCommonObj.CustomerID.Value, aspxCommonObj.SessionCode, aspxCommonObj.StoreID, aspxCommonObj.PortalID);

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void SaveTransactionLog()
    {
        try
        {
            var tinfo = new TransactionLogInfo();
            var tlog = new TransactionLog();

            tinfo.TransactionID = refno;
            tinfo.AuthCode = "";
            tinfo.TotalAmount = decimal.Parse(amount);
            tinfo.ResponseCode = msgValue;
            tinfo.ResponseReasonText = rrText;
            tinfo.OrderID = orderId;
            tinfo.StoreID = GetStoreID;
            tinfo.PortalID = GetPortalID;
            tinfo.AddedBy = GetUsername;
            tinfo.CustomerID = GetCustomerID.Value;
            tinfo.SessionCode = HttpContext.Current.Session.SessionID;
            tinfo.PaymentGatewayID = int.Parse(pgid);
            tinfo.PaymentStatus = isVerified == false ? pStatus + "verifcation Failed" : pStatus;
            tinfo.PayerEmail = email;
            tinfo.CreditCard = "";
            tinfo.RecieverEmail = "";
            //tinfo.CurrencyCode = selectedCurrency;
            tlog.SaveTransactionLog(tinfo);            
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }

    //private void UpdateTranscationID()
    //{
    //    try
    //    {
    //        StreamWriter ss = new StreamWriter(Server.MapPath("~/POSTUpdae.txt"));
            
    //        var tlog = new TransactionLog();
    //        if (Session["AppointmentData"] != null)
    //        {
    //            ss.WriteLine("AppointmentData=AppointmentData");
    //            var scheduleObj = (SaveSeminarShedule) HttpContext.Current.Session["AppointmentData"];
    //            tlog.UpdateTransactionID(scheduleObj.TransactionID, scheduleObj.InvoceNumber,
    //                                     scheduleObj.SeminarBookedID,
    //                                     GetStoreID, GetPortalID, GetCurrentCultureName, GetUsername);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
            
    //         ProcessException(ex);
    //    }
    //}

    private void OTCTransCommit()
    {
        try
        {
            ph.DragonPayOTCupdate(refno);
            Response.Redirect("~/DragonPaySuccess.aspx?refno=" + refno, false);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    //private void TransCommitForAppointment()
    //{
    //    if (!string.IsNullOrEmpty(status))
    //    {
    //        try
    //        {
    //            DragonPaySettingInfo sf = ph.GetDragonPaySettings(aspxCommonObj.StoreID, int.Parse(pgid), aspxCommonObj.PortalID);
    //            String message = ph.GetSHA1Digest(String.Format("{0}:{1}:{2}:{3}:{4}", txnid, refno, status, msg, sf.DragonPaySecretKey));
    //            pStatus = ph.PaymentStatusCodesDescription(status);
    //            rrText = ph.PaymentErrorCodesDescription(msgValue);
    //            refno = Request["refno"].ToString();
    //            if (message == digest)
    //            {
    //                isVerified = true;
    //                if (status.Equals("S")) //transaction success
    //                {
    //                    SaveTransactionLog();

    //                    Response.Redirect("~/DragonPaySuccess.aspx?Status=" + pStatus, false);
    //                }
    //                else
    //                {
    //                    SaveTransactionLog();

    //                    Response.Redirect("~/DragonPayError.aspx?Status=" + pStatus, false);
    //                }
    //            }
    //            else
    //            {
    //                SaveTransactionLog();
    //                Response.Redirect("~/DragonPayError.aspx?Status=" + pStatus, false);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ProcessException(ex);
    //        }
    //    }
    //}
}
