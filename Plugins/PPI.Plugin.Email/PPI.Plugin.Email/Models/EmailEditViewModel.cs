using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Email.Models
{
    using PPI.Platform.Core.Entities;
    public class EmailEditViewModel : ModelBase
    {        
        public IEnumerable<Email> Emails {get; set; }
        public Group Group { get; set; }
    }
}