using System;
using System.Collections.Generic;
using AspxCommerce.Core;
using SageFrame.Web.Utilities;

namespace AspxCommerce.DragonPay
{
    public class DragonPayHandler
    {      

        public DragonPaySettingInfo GetDragonPaySettings(int storeId, int paymentGatewayId, int portalId)
        {
            try
            {
                var param = new List<KeyValuePair<string, object>>();
                param.Add(new KeyValuePair<string, object>("@PaymentGateWayTypeID", paymentGatewayId));
                param.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                param.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                var sqLH = new SQLHandler();
                return sqLH.ExecuteAsObject<DragonPaySettingInfo>("usp_aspx_GetDragonPaySettingAll", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrderStatus(int orderId, string transId, string status, AspxCommonInfo aspxCommonObj)
        {
            try
            {

                OrderDetailsCollection ot = new OrderDetailsCollection();
                OrderDetailsInfo odinfo = new OrderDetailsInfo();
                CartManageSQLProvider cms = new CartManageSQLProvider();
                CommonInfo cf = new CommonInfo();
                cf.StoreID = aspxCommonObj.StoreID;
                cf.PortalID = aspxCommonObj.PortalID;
                cf.AddedBy = aspxCommonObj.UserName;
                // UpdateOrderDetails
                AspxOrderDetails objad = new AspxOrderDetails();
                SQLHandler sqlH = new SQLHandler();
                // use split to split array we already have using "=" as delimiter
                // WcfSession ws = new WcfSession();
                odinfo.OrderID = orderId; //ws.GetSessionVariable("OrderID");
                odinfo.ResponseReasonText = status;
                odinfo.TransactionID = transId;
                ot.ObjOrderDetails = odinfo;
                ot.ObjCommonInfo = cf;
                odinfo.OrderStatusID = 8;
                objad.UpdateOrderDetails(ot);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateItemQuantity(string itemIds, string coupon, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                string[] ids = itemIds.Split(',');
                //id,quantity,isdownloadable
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i].Contains("&"))
                    {
                        string[] itemdetails = ids[i].Split('&');
                        string[] coupondetails = coupon.Split('&');
                        if (itemdetails[0] != null)
                        {
                            var paraMeter = new List<KeyValuePair<string, object>>();
                            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                            paraMeter.Add(new KeyValuePair<string, object>("@AddedBy", aspxCommonObj.UserName));
                            paraMeter.Add(new KeyValuePair<string, object>("@ItemID", itemdetails[0]));
                            paraMeter.Add(new KeyValuePair<string, object>("@Quantity", itemdetails[1]));
                            var sqlH = new SQLHandler();
                            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_UpdateItemQuantitybyOrder]", paraMeter);
                           
                        }
                        if (coupondetails[0] != null && coupondetails[1] != null)
                        {
                            var paraMeter = new List<KeyValuePair<string, object>>();
                            paraMeter.Add(new KeyValuePair<string, object>("@CouponCode", coupondetails[0]));
                            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                            paraMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
                            paraMeter.Add(new KeyValuePair<string, object>("@CouponUsedCount", coupondetails[1]));
                            paraMeter.Add(new KeyValuePair<string, object>("@OrderID", itemdetails[2]));
                            var sqlH = new SQLHandler();
                            sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUserRecord", paraMeter);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GetTransactionId(string invoice, int storeId, int portalId)
        {
            var paraMeter = new List<KeyValuePair<string, object>>();
            paraMeter.Add(new KeyValuePair<string, object>("@Invoice", invoice));
            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));

            var sqlH = new SQLHandler();
            return sqlH.ExecuteAsScalar<string>("usp_Aspx_GetTransactionIDByInvoice", paraMeter);
        }

        public string GetSecretKey(string paymentGatewayId, int storeId, int portalId)
        {
            var param = new List<KeyValuePair<string, object>>();
            param.Add(new KeyValuePair<string, object>("@PaymentGateWayTypeID", paymentGatewayId));
            param.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            param.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            var sqLH = new SQLHandler();
            return sqLH.ExecuteAsScalar<string>("usp_aspx_GetDragonPaySecretKey", param);
        }
       
        public string GetSHA1Digest(string message)
        {
            try
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                System.Security.Cryptography.SHA1 sha1 = new
                System.Security.Cryptography.SHA1CryptoServiceProvider();
                byte[] result = sha1.ComputeHash(data);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < result.Length; i++)
                    sb.Append(result[i].ToString("X2"));
                return sb.ToString().ToLower();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PaymentStatusCodesDescription(string statusCode)
        {
            string paymentStatus = string.Empty;
            try
            {
                if (statusCode.Equals("S"))
                {
                    paymentStatus = "Success";
                }
                else if (statusCode.Equals("F"))
                {
                    paymentStatus = "Failed";
                }
                else if (statusCode.Equals("P"))
                {
                    paymentStatus = "Pending";
                }
                else if (statusCode.Equals("U"))
                {
                    paymentStatus = "Unknown";
                }
                else if (statusCode.Equals("R"))
                {
                    paymentStatus = "Refund";
                }
                else if (statusCode.Equals("K"))
                {
                    paymentStatus = "Chargeback";
                }
                else if (statusCode.Equals("V"))
                {
                    paymentStatus = "Void";
                }
                else if (statusCode.Equals("A"))
                {
                    paymentStatus = "Authorized";
                }
                return paymentStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string PaymentErrorCodesDescription(string msgCode)
        {
            string msgDescription = string.Empty;
            try
            {
                if (msgCode.Equals("000"))
                {
                    msgDescription = "Success";
                }
                else if (msgCode.Equals("101"))
                {
                    msgDescription = "Invalid payment gateway id";
                }
                else if (msgCode.Equals("102"))
                {
                    msgDescription = "Incorrect secret key";
                }
                else if (msgCode.Equals("103"))
                {
                    msgDescription = "Invalid reference number";
                }
                else if (msgCode.Equals("104"))
                {
                    msgDescription = "Unauthorized access";
                }
                else if (msgCode.Equals("105"))
                {
                    msgDescription = "Invalid token";
                }
                else if (msgCode.Equals("106"))
                {
                    msgDescription = "Currency not supported";
                }
                else if (msgCode.Equals("107"))
                {
                    msgDescription = "Transaction cancelled";
                }
                else if (msgCode.Equals("108"))
                {
                    msgDescription = "Insufficient funds";
                }
                else if (msgCode.Equals("109"))
                {
                    msgDescription = "Transaction limit exceeded";
                }
                else if (msgCode.Equals("110"))
                {
                    msgDescription = "Error in operation";
                }
                else if (msgCode.Equals("111"))
                {
                    msgDescription = "Invalid parameters";
                }
                else if (msgCode.Equals("201"))
                {
                    msgDescription = "Invalid Merchant Id";
                }
                else if (msgCode.Equals("202"))
                {
                    msgDescription = "Invalid Merchant Password";
                } 

                return msgDescription;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RefNumberExist(string refNumber)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> Parameter = new List<KeyValuePair<string, object>>();
                Parameter.Add(new KeyValuePair<string, object>("@ReferanceNumber", refNumber));
                return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_DragonPayReferanceExists]", Parameter, "@IsExists");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DragonPayOTCupdate(string refNumber)
        {
            try
            {
                var paraMeter = new List<KeyValuePair<string, object>>();
                paraMeter.Add(new KeyValuePair<string, object>("@ReferanceNumber", refNumber));                
                var sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DragonPayOTCupdate]", paraMeter);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
