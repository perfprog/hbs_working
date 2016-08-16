using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPI.Plugin.Survey.Core.Entities;
namespace PPI.Plugin.Survey.Models
{
    public class PcptProfile
    {
        public ParticipantProfile PartProfile { get; set; }
        public bool SurveyCompleted { get; set; }

        public bool SurveyReadOnly { get; set; }
    }
}