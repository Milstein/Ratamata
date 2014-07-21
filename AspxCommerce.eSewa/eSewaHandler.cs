using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using SageFrame.Web.Utilities;

namespace AspxCommerce.Core
{
    public class eSewaHandler
    {
        public static bool VerifyTransaction(bool isTest, decimal tAmt, string pid, string rid, string merchantID)
        {
            bool successful = false;
            string response = string.Empty;
            string postUrl = string.Empty;

            if (isTest)
            {
                postUrl = "http://dev.esewa.com.np/epay/transrec";

            }
            else
            {
                postUrl = "https://esewa.com.np/epay/transrec";
            }

            try
            {
                RemotePost req = new RemotePost(postUrl);
                req.Timeout = 3;
                req.Add("amt", tAmt.ToString());
                req.Add("scd", merchantID);
                req.Add("pid", pid);
                req.Add("rid", rid);
                response = req.Get();
                successful = Regex.IsMatch(response, @"\bSuccess\b");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return successful;
        }

        public class RemotePost
        {
            private NameValueCollection Inputs = new NameValueCollection();
            private string Url = string.Empty;
            public int Timeout = 0;

            public RemotePost(string url)
            {
                Url = url;
            }

            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }


            public string Get()
            {
                string postData = String.Empty;

                int keyscount = Inputs.Keys.Count;

                for (int i = 0; i < keyscount; i++)
                    postData += "&" + Inputs.Keys[i] + "=" + HttpUtility.UrlEncode(Inputs[Inputs.Keys[i]]);

                if (postData.Length > 0)
                    postData = postData.Substring(1);

                string postUrl = Url.Contains("?") ? (Url + "&" + postData) : (Url + "?" + postData);

                HttpWebRequest wr = WebRequest.Create(postUrl) as HttpWebRequest;
                wr.Method = "GET";

                return GetResponse(wr);
            }

            private string GetResponse(HttpWebRequest wr)
            {
                wr.Timeout = Timeout * 1000; //millisecond timeout

                HttpWebResponse response = (HttpWebResponse)wr.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                    return String.Empty;

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader readerStream = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                    {
                        return readerStream.ReadToEnd();
                    }
                }
            }
        }


        public static void ParseIPN(int orderID, string transID, string status, int storeID, int portalID, string userName, int customerID, string sessionCode)
        {
            eSewaHandler ph = new eSewaHandler();
            try
            {
                OrderDetailsCollection ot = new OrderDetailsCollection();
                OrderDetailsInfo odinfo = new OrderDetailsInfo();
                CartManageSQLProvider cms = new CartManageSQLProvider();
                CommonInfo cf = new CommonInfo();
                cf.StoreID = storeID;
                cf.PortalID = portalID;
                cf.AddedBy = userName;
                AspxOrderDetails objad = new AspxOrderDetails();
                SQLHandler sqlH = new SQLHandler();
                // use split to split array we already have using "=" as delimiter
                // WcfSession ws = new WcfSession();
                odinfo.OrderID = orderID;//ws.GetSessionVariable("OrderID");
                odinfo.ResponseReasonText = status;
                odinfo.TransactionID = transID;
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

        public static void UpdateItemQuantity(string itemIds, string coupon, int storeId, int portalId, string userName)
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
                            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                            paraMeter.Add(new KeyValuePair<string, object>("@AddedBy", userName));
                            paraMeter.Add(new KeyValuePair<string, object>("@ItemID", itemdetails[0]));
                            paraMeter.Add(new KeyValuePair<string, object>("@Quantity", itemdetails[1]));
                            paraMeter.Add(new KeyValuePair<string, object>("@OrderID", itemdetails[2]));
                            paraMeter.Add(new KeyValuePair<string, object>("@CostVariantsIDs", itemdetails[3]));
                            var sqlH = new SQLHandler();
                            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_UpdateItemQuantitybyOrder]", paraMeter);
                        }
                        if (coupondetails[0] != null && coupondetails[1] != null)
                        {
                            var paraMeter = new List<KeyValuePair<string, object>>();
                            paraMeter.Add(new KeyValuePair<string, object>("@CouponCode", coupondetails[0]));
                            paraMeter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
                            paraMeter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                            paraMeter.Add(new KeyValuePair<string, object>("@UserName", userName));
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

    }
}

