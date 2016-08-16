using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPI.Platform.Web.Models
{
    public class AccountCreateViewModel
    {
        public string AspNetUsersId { get; set; }
        public ModelStateDictionary ModelErrors { get; set; }
    }
}