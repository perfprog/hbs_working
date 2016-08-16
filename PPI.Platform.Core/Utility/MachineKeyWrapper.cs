using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Data.Entity;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace PPI.Platform.Core.Utility
{
    using PPI.Platform.Core.Attributes;    
    public static class MachineKeyWrapper
    {
        public static byte[] Protect(byte[] userData, params string[] purposes)
        {
            return MachineKey.Protect(userData, purposes);
        }

        public static byte[] Unprotect(byte[] protectedData, params string[] purposes)
        {
            return MachineKey.Unprotect(protectedData, purposes);
        }        
        public static string UnprotectUrlSafeString(string protectedData, params string[] purposes)
        {
            //decode ; decrypt ; Convert to string
            return System.Text.Encoding.UTF8.GetString(MachineKey.Unprotect(HttpServerUtility.UrlTokenDecode(protectedData), purposes));                   
        }        
        public static string ProtectUrlSafeString(string userData, params string[] purposes)
        {
            //Convert to byte ; Encrypt ; Encode
            return HttpServerUtility.UrlTokenEncode(MachineKey.Protect(userData.ToByteArray(), purposes));           
        }
       
    }
}
