using System;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;

namespace PPI.Platform.Mvc.Utility
{
    using PPI.Platform.Core.Utility;
    public static class Cookies
    {
        
        public static void SetCookie(string key, string value, TimeSpan expires)
        {           

            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                var existingCookie = HttpContext.Current.Request.Cookies[key];
                existingCookie.Expires = DateTime.Now.Add(expires);
                existingCookie.Value = MachineKeyWrapper.ProtectUrlSafeString(value, "Cookies");
                HttpContext.Current.Response.Cookies.Add(existingCookie);
            }
            else
            {
                HttpCookie cookie = new HttpCookie(key, MachineKeyWrapper.ProtectUrlSafeString(value, "Cookies"));
                cookie.Expires = DateTime.Now.Add(expires);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }        
        public static string GetCookie(string key)
        {
            try
            {
                string retVal = string.Empty;
                HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
                if (cookie != null)
                {
                    retVal = MachineKeyWrapper.UnprotectUrlSafeString(cookie.Value, "Cookies");
                }
                return retVal;
            }
            catch (CryptographicException)
            {                 
                return string.Empty;
            }                        
        }
    }
}
