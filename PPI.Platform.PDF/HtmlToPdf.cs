using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Platform.Pdf
{
    using PPI.Platform.Core.Pdf.Interface;
    using PPI.Platform.Core.Pdf;
    using HiQPdf;
    using System.ComponentModel.Composition;
    using System.IO;
    using PPI.Platform.Core.Logging.Interface;
    using PPI.Platform.Core.Attributes;
    [Logging(AttributeExclude = true)]      
    [Export(typeof(IHtmlToPdf))]
    [PartCreationPolicy(CreationPolicy.Shared)]    
    public class HtmlToPdf : IHtmlToPdf
    {
        private PdfMargins _Margins = new PdfMargins();
        private PdfPageSize _PageSize;
        private PdfPageOrientation _PageOrientation;
        private HiQPdf.HtmlToPdf _HtmlToPdfConverter;
        public HtmlToPdf()
        {
            _HtmlToPdfConverter = new HiQPdf.HtmlToPdf();
            _HtmlToPdfConverter.SerialNumber = @"35e2jo+7-uZO2va2+-rabn8e//-7v/s/+bq-6P/s7vHu-7fHm5ubm";
        }

        public PageOrientation PageOrientation
        {         
            set
            {
                switch (value)
                {
                    case PageOrientation.Landscape:
                        _PageOrientation = PdfPageOrientation.Landscape;
                        break;
                    case PageOrientation.Portrait:
                        _PageOrientation = PdfPageOrientation.Portrait;
                        break;                    
                }
            }
        }
        public PageSize PageSize
        {            
            set
            {
                switch (value)
                { 
                    case PageSize.Letter:
                        _PageSize = new PdfPageSize(612.0F,792.0F);                        
                        break;
                    case PageSize.Legal:
                        _PageSize = new PdfPageSize(612.0F,1008.0F);
                        break;
                    case PageSize.A4:
                        _PageSize = new PdfPageSize(595.0F, 842.0F);
                        break;                
                }
            }
        }
        
        public void SetAllMargins(float margin)
        {
            _Margins = new PdfMargins(margin);
        }


        public float TopMargin
        {
            get
            {
                return _Margins.Top;
            }
            set
            {
                _Margins.Top = value;
            }
        }

        public float BottomMargin
        {
            get
            {
                return _Margins.Bottom;
            }
            set
            {
                _Margins.Bottom = value;
            }
        }

        public float LeftMargin
        {
            get
            {
                return _Margins.Left;
            }
            set
            {
                _Margins.Left = value;
            }
        }

        public float RightMargin
        {
            get
            {
                return _Margins.Right;
            }
            set
            {
                _Margins.Right = value;
            }
        }


        public byte[] ConvertUrlToByteArray(string url)
        {
            _HtmlToPdfConverter.Document.PageSize = _PageSize;
            _HtmlToPdfConverter.Document.PageOrientation = _PageOrientation;
            _HtmlToPdfConverter.Document.Margins = _Margins;
            return _HtmlToPdfConverter.ConvertUrlToMemory(url);
        }

        public byte[] ConvertHtmlToMemory(string html,string baseUrl)
        {
            
            _HtmlToPdfConverter.Document.PageSize = _PageSize;
            _HtmlToPdfConverter.Document.PageOrientation = _PageOrientation;
            _HtmlToPdfConverter.Document.Margins = _Margins;
            return _HtmlToPdfConverter.ConvertHtmlToMemory(html, baseUrl);
                        
        }
        
        System.IO.MemoryStream IHtmlToPdf.ConvertUrlToMemory(string url)
        {
            _HtmlToPdfConverter.Document.PageSize = _PageSize;
            _HtmlToPdfConverter.Document.PageOrientation = _PageOrientation;
            _HtmlToPdfConverter.Document.Margins = _Margins;
            MemoryStream output = new MemoryStream();
            _HtmlToPdfConverter.ConvertUrlToStream(url, output);
            return output;
        }
    }
}
