using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Com.Asiapay.Secure
{
    class SHAAlgorithmUtil
    {

        public string operationAlgorithm(string secureData)
        {

            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(secureData);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string hexResult = BitConverter.ToString(bytes_sha1_out).Replace("-", string.Empty).ToLower();

            //Console.WriteLine("hexResult=[" + hexResult + "]");
            return hexResult;
        }
    }
}
