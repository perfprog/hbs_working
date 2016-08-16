using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Report.Models
{
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    using PPI.Platform.Core.Entities;
    public class ReportIndexViewModel : ModelBase
    {
        public IEnumerable<SurveyGroupParticipant> SurveyGroupParticipants { get; set; }
        public bool LockExportButton { get; set; }
    }
}