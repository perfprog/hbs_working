//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
	  
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PPI.Platform.Core.Properties;
using PPI.Platform.Core.Attributes;

namespace PPI.Platform.Core.Entities
{

[MetadataType(typeof(DashboardMetadata))]
	public partial class Dashboard
	{
		internal sealed class DashboardMetadata
		{
		
			[Required(ErrorMessageResourceName="Dashboard_Id_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "Dashboard_Id",ResourceType=typeof(Resources_Entities))]
			public Int32 Id { get; set; }

			[Required(ErrorMessageResourceName="Dashboard_RegisteredPluginsId_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "Dashboard_RegisteredPluginsId",ResourceType=typeof(Resources_Entities))]
			public Int32 RegisteredPluginsId { get; set; }

			[Required(ErrorMessageResourceName="Dashboard_PartialViewName_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
			[StringLength(150)]
    		[Display(Name = "Dashboard_PartialViewName",ResourceType=typeof(Resources_Entities))]
			public String PartialViewName { get; set; }

			[Required(ErrorMessageResourceName="Dashboard_Role_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
			[StringLength(50)]
    		[Display(Name = "Dashboard_Role",ResourceType=typeof(Resources_Entities))]
			public String Role { get; set; }

    		//public EntityCollection<RegisteredPlugin> RegisteredPlugin { get; set; }

		}
	}
}
	
