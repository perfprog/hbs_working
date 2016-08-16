using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Email.Models
{
    public class EmailSendEmailPartialViewModel
    {
        public int Sent { get; set; }
        public int Failed { get; set; }        
    }
}