using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Email.Models
{
    using PPI.Platform.Core.Entities;
    public class EmailEditPostViewModel 
    {        
        public Email email {get; set; }
        public int groupid { get; set; }
    }
}