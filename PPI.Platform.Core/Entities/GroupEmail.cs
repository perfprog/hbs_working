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
    public partial class GroupEmail
    {
        public GroupEmail()
        {
            this.PersonGroupEmails = new HashSet<PersonGroupEmail>();
        }
    
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int EmailId { get; set; }
    
        public virtual Email Email { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<PersonGroupEmail> PersonGroupEmails { get; set; }
    }
}