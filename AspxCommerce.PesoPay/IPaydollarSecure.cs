using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Asiapay.Secure
{
    public interface IPaydollarSecure
    {
        string generatePaymentSecureHash(string merchantId,
            string merchantReferenceNumber, string currencyCode, string amount,
            string paymentType, string secureHashSecret);



        bool verifyPaymentDatafeed(string src, string prc, string successCode,
           string merchantReferenceNumber, string paydollarReferenceNumber,
           string currencyCode, string amount,
           string payerAuthenticationStatus, string secureHashSecret,
           string secureHash);
    }
}