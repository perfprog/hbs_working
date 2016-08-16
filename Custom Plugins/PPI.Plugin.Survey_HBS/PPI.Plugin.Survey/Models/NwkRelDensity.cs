﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPI.Plugin.Survey.Core.Entities;
namespace PPI.Plugin.Survey.Models
{
    public class NwkRelDensity
    {
        public int ParticipantID {get; set;}
        public bool SurveyCompleted { get; set; }
        public IEnumerable<NetworkContacts_V> Contacts { get; set; }
        public IEnumerable<RelationshipDensity> RelDensity { get; set; }
        public bool SurveyReadOnly { get; set; }
    }
}