using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Survey.Models
{
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    using PPI.Platform.Core.Entities;
    public class SurveyIndexViewModel : ModelBase
    {
        public IEnumerable<SurveyGroupParticipant> SurveyGroupParticipants { get; set; }
        public bool SurveyCompleted { get; set; }
        public bool LockSendAllButtons { get; set; }
    }
 
}