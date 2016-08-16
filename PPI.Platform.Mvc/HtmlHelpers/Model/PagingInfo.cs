using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Platform.Mvc.HtmlHelpers.Model
{
    public class PagingInfo
    {
        public PagingInfo()
        {            
            NavSize = 5;
            FirstPage = 1;
            LastPage = 0;
        }
        public int? NextPage { get; set; }
        public int? PrevPage { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }

        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int NavSize { get; set; }

        public string search { get; set; }
    }
}
