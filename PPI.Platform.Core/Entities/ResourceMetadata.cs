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

[MetadataType(typeof(ResourceMetadata))]
	public partial class Resource
	{
		internal sealed class ResourceMetadata
		{
		
			[Required(ErrorMessageResourceName="Resource_Id_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "Resource_Id",ResourceType=typeof(Resources_Entities))]
			public Int32 Id { get; set; }

			[Required(ErrorMessageResourceName="Resource_TableName_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
			[StringLength(50)]
    		[Display(Name = "Resource_TableName",ResourceType=typeof(Resources_Entities))]
			public String TableName { get; set; }

    		//public EntityCollection<GenericTypeValue> GenericTypeValues { get; set; }

    		//public EntityCollection<ResourceValue> ResourceValues { get; set; }

		}
	}
}
	
