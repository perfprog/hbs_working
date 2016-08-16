using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPI.Plugin.Administration.Models
{
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    using PPI.Platform.Core.Entities;
    public class ModelBase
    {
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<PluginAction> PluggableActions { get; set; }
    }
}