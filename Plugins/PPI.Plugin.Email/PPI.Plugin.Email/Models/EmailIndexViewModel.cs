using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Email.Models
{
    using PPI.Platform.Core.Entities;
    public class EmailIndexViewModel : ModelBase
    {

        public IEnumerable<EmailGrouping> GroupEmails { get; set; }
    }
    public class EmailGrouping
    {
        public Group Group { get; set; }
        public string EmailTypes { get; set; }
    }
}