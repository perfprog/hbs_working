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
    public partial class PersonGroupEmail
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int GroupEmailId { get; set; }
        public System.DateTime SendDate { get; set; }
        public Nullable<int> SendStatusGenericTypeValueId { get; set; }
        public string OrginalToAddress { get; set; }
        public string Body { get; set; }
        public Nullable<int> RetryCount { get; set; }
        public string ErrorMessage { get; set; }
    
        public virtual GenericTypeValue SendGenericTypeValue { get; set; }
        public virtual GroupEmail GroupEmail { get; set; }
        public virtual Person Person { get; set; }
    }
}
