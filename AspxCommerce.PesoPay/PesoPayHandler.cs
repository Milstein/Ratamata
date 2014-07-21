using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using AspxCommerce.PesoPay;
using SageFrame.Web.Utilities;

namespace AspxCommerce.PesoPay
{
    public class PesoPayHandler
    {
        public List<CartInfoPesoPay> GetCartDetails(int storeID, int portalID, int customerID, string userName,
                                                    string cultureName, string sessionCode)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", storeID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", customerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
            SQLHandler sqLH = new SQLHandler();
            return sqLH.ExecuteAsList<CartInfoPesoPay>("usp_Aspx_GetCartDetails", ParaMeter);
        }

        public PesoPaySettingInfo GetPesoPaySettings(int storeId, int paymentGatewayId, int portalId)
        {
            try
            {
                var param = new List<KeyValuePair<string, object>>();
                param.Add(new KeyValuePair<string, object>("@PaymentGateWayTypeID", paymentGatewayId));
                param.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                param.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                var sqLH = new SQLHandler();
                return sqLH.ExecuteAsObject<PesoPaySettingInfo>("usp_aspx_GetPesoPaySettingAll", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrderStatus(int orderId, string transId, string status, int storeId, int portalId,
                                      string userName, int customerId, string sessionCode)
        {
            try
            {

                OrderDetailsCollection ot = new OrderDetailsCollection();
                OrderDetailsInfo odinfo = new OrderDetailsInfo();
                CartManageSQLProvider cms = new CartManageSQLProvider();
                CommonInfo cf = new CommonInfo();
                cf.StoreID = storeId;
                cf.PortalID = portalId;
                cf.AddedBy = userName;
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

        public void UpdateItemQuantity(string itemIds, string coupon, int storeId, int portalId, string userName)
        {
            try
            {
                string[] coupondetails = coupon.Split('N');
                if (coupondetails[0] != null && coupondetails[1] != null)
                {
                    var paraMeter = new List<KeyValuePair<string, object>>();
                    paraMeter.Add(new KeyValuePair<string, object>("@CouponCode", coupondetails[0]));
                    paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                    paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                    paraMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
                    paraMeter.Add(new KeyValuePair<string, object>("@CouponUsedCount", coupondetails[1]));
                    var sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery("usp_Aspx_UpdateCouponUserRecord", paraMeter);
                }
                string[] ids = itemIds.Split(',');
                //id,quantity,isdownloadable
                for (int i = 0; i < ids.Length; i++)
                {
                    string[] itemdetails = ids[i].Split('N');
                    if (itemdetails[0] != "" && itemdetails[1] != "")
                    {
                        var paraMeter = new List<KeyValuePair<string, object>>();
                        paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                        paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                        paraMeter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                        paraMeter.Add(new KeyValuePair<string, object>("@ItemID", itemdetails[0]));
                        paraMeter.Add(new KeyValuePair<string, object>("@Quantity", itemdetails[1]));
                        var sqlH = new SQLHandler();
                        sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_UpdateItemQuantitybyOrder]", paraMeter);
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
            return sqLH.ExecuteAsScalar<string>("usp_aspx_GetPesoPaySecretKey", param);


        }

        public void ClearCartAfterPayment(int customerId, string sessionCode, int storeId, int portalId)
        {
            try
            {
                var parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("@CustomerID", customerId));
                parameter.Add(new KeyValuePair<string, object>("@SessionCode", sessionCode));
                parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                var sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("usp_Aspx_ClearCartAfterPayment", parameter);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
