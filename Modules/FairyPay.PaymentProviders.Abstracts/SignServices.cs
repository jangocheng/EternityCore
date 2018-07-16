using System;
using System.Text;

namespace FairyPay.PaymentProviders
{
    public static class SignServices
    {
        public static string MD5(string text, bool lcase = true)
        {

            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(text));
                var strResult = BitConverter.ToString(result).Replace("-", "");

                return lcase ? strResult.ToLowerInvariant() : strResult;
            }
        }

    }
}