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

[MetadataType(typeof(EmailMetadata))]
	public partial class Email
	{
		internal sealed class EmailMetadata
		{
		
			[Required(ErrorMessageResourceName="Email_Id_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "Email_Id",ResourceType=typeof(Resources_Entities))]
			public Int32 Id { get; set; }

			[Required(ErrorMessageResourceName="Email_EmailGenericTypeValueId_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
			[DataType(DataType.EmailAddress)]
    		[Display(Name = "Email_EmailGenericTypeValueId",ResourceType=typeof(Resources_Entities))]
			public Int32 EmailGenericTypeValueId { get; set; }

			[Required(ErrorMessageResourceName="Email_DefaultFrom_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
			[StringLength(250)]
    		[Display(Name = "Email_DefaultFrom",ResourceType=typeof(Resources_Entities))]
			public String DefaultFrom { get; set; }

			[Required(ErrorMessageResourceName="Email_Subject_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
			[StringLength(250)]
    		[Display(Name = "Email_Subject",ResourceType=typeof(Resources_Entities))]
			public String Subject { get; set; }

			[Required(ErrorMessageResourceName="Email_Introduction_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "Email_Introduction",ResourceType=typeof(Resources_Entities))]
			public String Introduction { get; set; }

			[Required(ErrorMessageResourceName="Email_Closing_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "Email_Closing",ResourceType=typeof(Resources_Entities))]
			public String Closing { get; set; }

			[Required(ErrorMessageResourceName="Email_Template_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "Email_Template",ResourceType=typeof(Resources_Entities))]
			public Boolean Template { get; set; }

			[StringLength(150)]
    		[Display(Name = "Email_TemplateName",ResourceType=typeof(Resources_Entities))]
			public String TemplateName { get; set; }

    		//public EntityCollection<GenericTypeValue> EmailGenericTypeValue { get; set; }

    		//public EntityCollection<GroupEmail> GroupEmails { get; set; }

		}
	}
}
	