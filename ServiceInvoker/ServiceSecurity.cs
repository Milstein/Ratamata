using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ServiceInvoker
{
    public class ServiceSecurity
    {

        private static string _tokenFormat = "__token:";

        public static string CreateTokenKey(string value)
        {

            return string.Format(_tokenFormat + "{0}", value);
        }
        public static string CreateToken()
        {

            return Guid.NewGuid().ToString();
        }
        private void tokenPattern() { }

        public static bool ValidateToken()
        {


            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                string headerTicket = context.Request.Headers["ASPX-TOKEN"];
                if (string.IsNullOrEmpty(headerTicket))
                    throw new System.Security.SecurityException("Security token must be present.");

                string Key = _tokenFormat + context.Session.SessionID;
                string ServerTicket = Convert.ToString(context.Session[Key]);

                if (string.Compare(headerTicket, ServerTicket, false) != 0)
                {
                    throw new System.Security.SecurityException("Invalid Security token.");
                }
                else return true;
            }
            else
                throw new System.Security.SecurityException("Authurization Failed.");
        }

    }
}
