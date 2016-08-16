using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace PPI.Platform.Core.Utility
{
    public static class StringExtention
    {
        public static byte[] ToByteArray(this string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }
        public static byte[] ToByteArrayFromBase64(this string str)
        {
            return Convert.FromBase64String(str);
        }
    }
}
