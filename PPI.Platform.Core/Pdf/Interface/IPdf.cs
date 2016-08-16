using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Platform.Core.Pdf.Interface
{
    public interface IPdf
    {        
        byte[] ConvertUrlToPdf(string url,string baseUrl);
        void ConvertUrlToFile(string url, string baseUrl, string outputFile);
    
    }
}
