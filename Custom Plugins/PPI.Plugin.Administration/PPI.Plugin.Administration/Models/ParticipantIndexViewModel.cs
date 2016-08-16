using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Administration.Models
{
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    public class ParticipantIndexViewModel : ModelBase
    {
        public IEnumerable<GroupParticipant> Participants { get; set; }
        public int GroupId { get; set; }
        public IEnumerable<AspNetUser> AspNetUsers { get; set; }
        
        
    }
    
}