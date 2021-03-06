//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPI.Platform.Core.Entities
{
    using System;
    using System.Collections.Generic;
    
    using PPI.Platform.Core.Attributes;
    public partial class Person
    {
        public Person()
        {
            this.Participants = new HashSet<Participant>();
            this.PersonGroupEmails = new HashSet<PersonGroupEmail>();
            this.Raters = new HashSet<Rater>();
        }
    
        public int Id { get; set; }
        public string Prefix { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Suffix { get; set; }
        public string Email { get; set; }
        public string AspNetUsersId { get; set; }
    
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<PersonGroupEmail> PersonGroupEmails { get; set; }
        public virtual ICollection<Rater> Raters { get; set; }
    }
}
