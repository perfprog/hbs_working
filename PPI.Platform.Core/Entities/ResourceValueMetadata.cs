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

[MetadataType(typeof(ResourceValueMetadata))]
	public partial class ResourceValue
	{
		internal sealed class ResourceValueMetadata
		{
		
			[Required(ErrorMessageResourceName="ResourceValue_Id_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "ResourceValue_Id",ResourceType=typeof(Resources_Entities))]
			public Int32 Id { get; set; }

			[Required(ErrorMessageResourceName="ResourceValue_ResourceId_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "ResourceValue_ResourceId",ResourceType=typeof(Resources_Entities))]
			public Int32 ResourceId { get; set; }

			[Required(ErrorMessageResourceName="ResourceValue_CultureId_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "ResourceValue_CultureId",ResourceType=typeof(Resources_Entities))]
			public Int32 CultureId { get; set; }

			[Required(ErrorMessageResourceName="ResourceValue_Value_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "ResourceValue_Value",ResourceType=typeof(Resources_Entities))]
			public String Value { get; set; }

    		//public EntityCollection<Culture> Culture { get; set; }

    		//public EntityCollection<Resource> Resource { get; set; }

		}
	}
}
	
