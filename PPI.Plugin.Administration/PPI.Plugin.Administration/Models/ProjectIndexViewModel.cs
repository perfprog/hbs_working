using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Administration.Models
{
    using PPI.Platform.Core.Entities;
    public class ProjectIndexViewModel : ModelBase
    {
        public IEnumerable<Group> Projects { get; set; }
    }
}