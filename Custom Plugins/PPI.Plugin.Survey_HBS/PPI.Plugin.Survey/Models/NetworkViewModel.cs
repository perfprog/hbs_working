using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Survey.Models
{
    using PPI.Plugin.Survey.Core.Entities;
    public class NetworkViewModel
    {
        public NetworkInfoContact NetworkInfoContact { get; set; }
        public bool SurveyReadOnly { get; set; }
    }
}