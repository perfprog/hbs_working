using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Platform.Core.Pdf.Interface
{
    public interface IHtmlToPdf
    {
        PageSize PageSize {set; }
        PageOrientation PageOrientation {set; }
        float TopMargin { get; set; }
        float BottomMargin { get; set; }
        float LeftMargin { get; set; }
        float RightMargin { get; set; }
        void SetAllMargins(float margin);
        MemoryStream ConvertUrlToMemory (string url);
        byte[] ConvertHtmlToMemory(string html,string baseUrl);

        byte[] ConvertUrlToByteArray(string url);
        

    }
}
