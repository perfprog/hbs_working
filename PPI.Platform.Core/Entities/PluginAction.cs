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
    public partial class PluginAction
    {
        public int Id { get; set; }
        public int RegisteredPluginsId { get; set; }
        public string ForView { get; set; }
        public int ActionGenericTypeIValueId { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string RoutingData { get; set; }
    
        public virtual GenericTypeValue ActionGenericTypeValue { get; set; }
        public virtual RegisteredPlugin RegisteredPlugin { get; set; }
    }
}