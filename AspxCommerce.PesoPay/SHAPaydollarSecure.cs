using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Asiapay.Secure
{
    public class SHAPaydollarSecure : IPaydollarSecure
    {
        public string generatePaymentSecureHash(string merchantId,
             string merchantReferenceNumber, string currencyCode, string amount,
             string paymentType, string secureHashSecret)
        {

            StringBuilder buffer = new StringBuilder();

            buffer.Append(merchantId).Append("|").Append(merchantReferenceNumber)
                    .Append("|").Append(currencyCode).Append("|").Append(amount)
                    .Append("|").Append(paymentType).Append("|").Append(
                            secureHashSecret);

            SHAAlgorithmUtil algorithmUtil = new SHAAlgorithmUtil();

            return algorithmUtil.operationAlgorithm(buffer.ToString());

        }

        public bool verifyPaymentDatafeed(string src, string prc, string successCode,
           string merchantReferenceNumber, string paydollarReferenceNumber,
           string currencyCode, string amount,
           string payerAuthenticationStatus, string secureHashSecret,
           string secureHash)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(src).Append("|").Append(prc).Append("|").Append(
                    successCode).Append("|").Append(merchantReferenceNumber)
                    .Append("|").Append(paydollarReferenceNumber).Append("|")
                    .Append(currencyCode).Append("|").Append(amount).Append("|")
                    .Append(payerAuthenticationStatus).Append("|").Append(
                            secureHashSecret);

            SHAAlgorithmUtil algorithmUtil = new SHAAlgorithmUtil();

            try
            {

                string verifyData = algorithmUtil.operationAlgorithm(buffer
                        .ToString());
                //Console.WriteLine("****************verify Begin**************");
                //Console.WriteLine("secureHash=[" + secureHash + "]");
                //Console.WriteLine("ExpectedSecureHash=[" + verifyData + "]");
                //Console.WriteLine("****************verify end**************");
                if (secureHash.Equals(verifyData))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                buffer.Remove(0, buffer.Length);
            }
        }
    }
}