//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPI.Plugin.Survey.Core.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class vWhoKnowWho
    {
        public string Pkey { get; set; }
        public string ContactA { get; set; }
        public string ContactB { get; set; }
        public int ParticipantId { get; set; }
        public string Networks { get; set; }
        public Nullable<bool> SameOrganization { get; set; }
        public Nullable<int> RelationshipID { get; set; }
        public int Order { get; set; }
    }
}