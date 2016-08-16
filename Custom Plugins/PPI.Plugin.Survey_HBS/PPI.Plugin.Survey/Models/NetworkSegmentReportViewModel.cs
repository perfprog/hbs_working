using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Survey.Models
{
    using PPI.Plugin.Survey.Core.Entities;
    public class NetworkSegmentReportViewModel
    {
        public string CreateDate { get; set; }
        public string FullName { get; set; }
        public string SourcePath { get; set; }
        public string NetworkDensity { get; set; }

        public bool SurveyCompleted { get; set; }
        public string VeryClosePercent { get; set; }
        public string ClosePercent { get; set; }
        public string NotVeryClosePercent { get; set; }
        public string DistantPercent { get; set; }

        public BarScales Company { get; set; }
        public BarScales BusinessUnit { get; set; }
        public BarScales FunctionArea { get; set; }
        public BarScales NativeLanguage { get; set; }
        public List<BarScaleTypes> Regions { get; set; }
        public List<BarScaleTypes> AgeGroup { get; set; }
        public List<BarScaleTypes> RelationShipType { get; set; }
        public List<BarScaleTypes> Gender { get; set; }


    }
    public class BarScales
    {
        public float SamePercent { get; set; }
        public float DifferentPercent { get; set; }

        public bool Same { get; set; }
        public bool Different { get; set; }
    
    }
    public class BarScaleTypes
    {
        public string Text { get; set; }
        public bool Selected { get; set; }
        public string Value { get; set; }
    }
}