using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using COM;
using AspxCommerce.TPSL;
using AspxCommerce.Core;

public partial class ResponsePG : System.Web.UI.Page
{
    string paymentStatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            OrderDetailsCollection orderdata2 = new OrderDetailsCollection();
            orderdata2 = (OrderDetailsCollection)HttpContext.Current.Session["OrderCollection"];

            COM.CheckSumResponseBean objCheckSumResponseBean = new COM.CheckSumResponseBean();
            TPSLUtil1 objTPSLUtil1 = new TPSLUtil1();

            Response.Write("Originally Generated String===========>" + Convert.ToString(Session["myString"]) + "End String===========>");
            //String strResponseMsg = "01|9892381157|16462992|NA|5.00|10|NA|NA|INR|NA|NA|NA|NA|19-04-2011 12:31:40|0399|NA|12345|100000001|NA|NA|NA|NA|NA|NA|NA|777629931425";//Request["msg"] == null ? "" : Request["msg"].Trim();
            String strResponseMsg = Request["msg"] == null ? "" : Request["msg"].Trim();

            Response.Write("strResponseMsg===========>" + strResponseMsg);

            String[] token = strResponseMsg.Split('|');

            if (token.Length == 26)
            {
                txtmerid.Text = token[0].ToString();
                txtsubscriberid.Text = token[1].ToString();
                txttxnrefno.Text = token[2].ToString();
                txtbankrefno.Text = token[3].ToString();
                txttxnamt.Text = token[4].ToString();
                txtbankid.Text = token[5].ToString();
                txtbankmerid.Text = token[6].ToString();
                txttcntype.Text = token[7].ToString();
                txtcurrencyname.Text = token[8].ToString();
                txttemcode.Text = token[9].ToString();
                txtsecuritytype.Text = token[10].ToString();
                txtsecurityid.Text = token[11].ToString();
                txtsecuritypass.Text = token[12].ToString();
                txttxndate.Text = token[13].ToString();
                txtauthstatus.Text = token[14].ToString();
                txtsettlementtype.Text = token[15].ToString();
                txtaddtninfo1.Text = token[16].ToString();
                txtaddtninfo2.Text = token[17].ToString();  //Custome Fields
                txtaddtninfo3.Text = token[18].ToString();
                txtaddtninfo4.Text = token[19].ToString();
                txtaddtninfo5.Text = token[20].ToString();
                txtaddtninfo6.Text = token[21].ToString();
                txtaddtninfo7.Text = token[22].ToString();
                txterrorstatus.Text = token[23].ToString();
                txterrordesc.Text = token[24].ToString();
                txtchecksum.Text = token[25].ToString();
            }
            else
            {
                Response.Write("Inside ELSE of Response***********");
                return;
            }

            objCheckSumResponseBean.MSG = strResponseMsg;
            objCheckSumResponseBean.PropertyPath = Server.MapPath("Property\\" + "MerchantDetails_sharedhosting.txt");

            string strCheckSumValue = objTPSLUtil1.transactionResponseMessage(objCheckSumResponseBean);

            Response.Write("strCheckSumValue***********" + strCheckSumValue);

            if (txtauthstatus.Text == "0300")
            {
                paymentStatus = "Success";
            }
            else 
            {
                paymentStatus = txterrordesc.Text.ToString();
            }

            string payerEmail = orderdata2.ObjBillingAddressInfo.EmailAddress;
            string receiverEmail = "";
            string amount = txttxnamt.Text.ToString();
            string transID = txttxnrefno.Text.ToString();
            int orderID = (int)Session["OrderID"];
            int storeID = orderdata2.ObjCommonInfo.StoreID;
            int portalID = orderdata2.ObjCommonInfo.PortalID;
            string userName = orderdata2.ObjCommonInfo.AddedBy;
            int customerID = orderdata2.ObjOrderDetails.CustomerID;
            string sessionCode = HttpContext.Current.Session.SessionID;
            int pgid = orderdata2.ObjOrderDetails.PaymentGatewayTypeID;

            //txtaddtninfo1.Text = token[16].ToString();
           // txtaddtninfo2.Text = token[17].ToString();  //Custome Fields

            //string CouponFields = txtaddtninfo2.Text;
            //string couponCode = CouponFields.Split('#')[0];
            //string itemFields = txtaddtninfo2.Text;
            //string itemids = itemFields.Split('#')[0];
            

            TransactionLogInfo tinfo = new TransactionLogInfo();
            TransactionLog Tlog = new TransactionLog();

            tinfo.TransactionID = transID;
            tinfo.AuthCode = txtauthstatus.Text.ToString();
            tinfo.TotalAmount = decimal.Parse(amount);
            tinfo.ResponseCode = txterrorstatus.Text.ToString();
            tinfo.ResponseReasonText = txterrordesc.Text.ToString();
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

            if (!strCheckSumValue.Equals(""))
            {
                if (txtchecksum.Text.Equals(strCheckSumValue))
                {   

                    //Transaction Successful
                    TPSLHandler.ParseIPN(orderID, transID, paymentStatus, storeID, portalID, userName, customerID, sessionCode);
                    //TPSLHandler.UpdateItemQuantity(itemids, couponCode, storeID, portalID, userName);
                    CartManageSQLProvider cms = new CartManageSQLProvider();
                    cms.ClearCartAfterPayment(customerID, sessionCode, storeID, portalID);
                    AspxOrderDetails orderUpdate = new AspxOrderDetails();
                    orderUpdate.UpdateItemQuantity(orderdata2);                   
                    orderUpdate.ReduceCouponUsed(orderdata2.ObjOrderDetails.CouponCode, storeID,portalID, userName,orderID);  
                    Response.Redirect("TPSL-Success.aspx");
                    
                }

                if (!txtchecksum.Text.Equals(strCheckSumValue))
                {
                    txtauthstatus.Text = "0399";
                    txterrordesc.Text = "Transaction Failed due to checksum mismatch";
                }
            }
            else
            {
                txtauthstatus.Text = "0399";
                txterrordesc.Text = "Transaction Failed due to invalid parameters";
            }

        }

    }
}
