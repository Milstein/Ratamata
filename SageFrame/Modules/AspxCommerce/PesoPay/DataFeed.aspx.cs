using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AspxCommerce.Core;
using AspxCommerce.PayPal;
using Com.Asiapay.Secure;
using SageFrame.Framework;
using SageFrame.Web;
using AspxCommerce.PesoPay;

public partial class PesoPay_DataFeed : PageBase 
{

    protected void Page_Load(object sender, EventArgs e)
    {


        string successCode = Request.Form["successcode"];
        string payRef = Request.Form["PayRef"];
        string orderRef = Request.Form["ref"];
        string currencyCode = Request.Form["Cur"];
        string amount = Request.Form["Amt"];
        string ids = Request.Form["remark"];
        string alertcode = Request.Form["AlertCode"];
        string holder = Request.Form["Holder"];
        string src = Request.Form["src"];
        string prc = Request.Form["prc"];
        string payerAuth = Request.Form["payerAuth"];
        string secureHashSecret = "";
        string secureHashcode = Request.Form["secureHash"];
        Response.Write("OK");
        bool isVerified = false;
       

        int orderId = 0, storeId = 0, portalId = 0, customerId = 0;
        string userName = "", sessionCode = "", pgid = "", paymentStatus = "";
        if (!string.IsNullOrEmpty(successCode))
        {

            if (successCode.Equals("0") && prc == "0" && src == "0") //transaction success
            {
                var objHandler = new PesoPayHandler();
                bool isSecureHash = ids.Split('#')[10].Trim() == "true" ? true : false;
                if (isSecureHash)
                {
                    if (ids != "")
                    {
                        string[] customFields = ids.Split('#');
                        orderId = int.Parse(customFields[0]);
                        storeId = int.Parse(customFields[1]);
                        portalId = int.Parse(customFields[2]);
                        userName = customFields[3];
                        customerId = int.Parse(customFields[4]);
                        sessionCode = customFields[5];
                        pgid = customFields[7];
                        string itemsIds = customFields[8];
                        string couponcode = customFields[9];
                        secureHashSecret = objHandler.GetSecretKey(pgid, storeId, portalId);

                        if (VerifyHash(src, prc, successCode, orderRef, payRef, currencyCode, amount, payerAuth,
                                       secureHashSecret, secureHashcode))
                        {
                            isVerified = true;
                            string appPath = Request.PhysicalApplicationPath;
                            string filePath = appPath + "pids.txt";

                            //StreamWriter w = File.CreateText(filePath);
                            //w.WriteLine("POST");
                            //w.WriteLine("Total=" + ids);

                            ////foreach (var formvalue in Request.Form)
                            ////{
                            ////    w.Write(formvalue);
                            ////    w.Write("=");
                            ////    w.WriteLine(Request.Form[formvalue.ToString()]);

                            ////}
                            //w.Flush();
                            //w.Close();

                            try
                            {
                                objHandler.UpdateOrderStatus(orderId, payRef, "Transaction Completed", storeId, portalId,
                                                             userName,
                                                             customerId, sessionCode);
                                objHandler.UpdateItemQuantity(itemsIds, couponcode, storeId, portalId, userName);
                                objHandler.ClearCartAfterPayment(customerId, sessionCode, storeId, portalId);
                            }
                            catch (Exception ex)
                            {
                                ProcessException(ex);
                            }
                        }else
                        {
                            isVerified = false;

                        }
                    }
                }
                else
                {
                    if (ids != "")
                    {
                        string[] customFields = ids.Split('#');
                        orderId = int.Parse(customFields[0]);
                        storeId = int.Parse(customFields[1]);
                        portalId = int.Parse(customFields[2]);
                        userName = customFields[3];
                        customerId = int.Parse(customFields[4]);
                        sessionCode = customFields[5];
                        pgid = customFields[7];
                        string itemsIds = customFields[8];
                        string couponcode = customFields[9];
                        try
                        {
                            objHandler.UpdateOrderStatus(orderId, payRef, "Transaction Completed", storeId, portalId,
                                                         userName,
                                                         customerId, sessionCode);
                            objHandler.UpdateItemQuantity(itemsIds, couponcode, storeId, portalId, userName);
                            objHandler.ClearCartAfterPayment(customerId, sessionCode, storeId, portalId);
                       
                        }
                        catch (Exception ex)
                        {
                            ProcessException(ex);
                        }
                    }

                }

            }
            else if (successCode.Equals("-1")) //Rejected due to Input Parameters Incorrect
            {
                paymentStatus = "Rejected due to Input Parameters Incorrect";
            }
            else if (successCode.Equals("1")) //  Rejected by Payment Bank
            {
                paymentStatus = "Rejected by Payment Bank";
            }
            else if (successCode.Equals("3")) // Rejected due to Payer Authentication Failure (3D)
            {
                paymentStatus = "Rejected due to Payer Authentication Failure (3D)";
            }
            else if (successCode.Equals("-2")) // Rejected due to Server Access Error
            {
                paymentStatus = "Rejected due to Server Access Error";
            }
            else if (successCode.Equals("-8")) // Rejected due to PesoPay Internal/Fraud Prevention Checking
            {
                paymentStatus = "Rejected due to PesoPay Internal/Fraud Prevention Checking";
            }
            else if (successCode.Equals("-9")) // Rejected by Host Access Error
            {
                paymentStatus = "Rejected by Host Access Error";
            }
            else if (successCode.Equals("-10")) // System Error
            {
                paymentStatus = "System Error";
            }
            try
            {
                var tinfo = new TransactionLogInfo();
                var tlog = new TransactionLog();

                tinfo.TransactionID = payRef;
                tinfo.AuthCode = "";
                tinfo.TotalAmount = decimal.Parse(amount);
                tinfo.ResponseCode = successCode;
                tinfo.ResponseReasonText = alertcode;
                tinfo.OrderID = orderId;
                tinfo.StoreID = storeId;
                tinfo.PortalID = portalId;
                tinfo.AddedBy = userName;
                tinfo.CustomerID = customerId;
                tinfo.SessionCode = sessionCode;
                tinfo.PaymentGatewayID = int.Parse(pgid);
                tinfo.PaymentStatus = isVerified == false
                                          ? paymentStatus + " SecureHash verifcation Failed"
                                          : paymentStatus;
                tinfo.PayerEmail = holder;
                tinfo.CreditCard = "";
                tinfo.RecieverEmail = "";
                tlog.SaveTransactionLog(tinfo);
            }
            catch (Exception ex)
            {

                ProcessException(ex);
            }


        }
    }

    private bool VerifyHash(string src, string prc, string successCode, string orderRef, string payRef, string currencyCode, string amount, string payerAuth, string secureHashSecret, string secureHashCode)
    {
        bool verifyResult = false;

        //if secureHash is used
        SHAPaydollarSecure paydollarSecure = new SHAPaydollarSecure();
        string secureHashStr = secureHashCode;

        if (secureHashStr != null && !"".Equals(secureHashStr))
        {
            if (secureHashStr.IndexOf(",") > 0)
            {
                string[] secureHash = secureHashStr.Split(',');
                for (int i = 0; i < secureHash.Length; i++)
                {
                    verifyResult = paydollarSecure.verifyPaymentDatafeed(
                        src, prc, successCode, orderRef, payRef, currencyCode, amount, payerAuth, secureHashSecret,
                        secureHash[i]);
                    if (verifyResult)
                    {
                        break;
                    }
                }
            }
            else
            {
                verifyResult = paydollarSecure.verifyPaymentDatafeed(
                    src, prc, successCode, orderRef, payRef, currencyCode, amount, payerAuth, secureHashSecret,
                    secureHashStr);
            }
        }

        return verifyResult;

    }

}
