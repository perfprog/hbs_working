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

[MetadataType(typeof(SurveyGroupParticipantMetadata))]
	public partial class SurveyGroupParticipant
	{
		internal sealed class SurveyGroupParticipantMetadata
		{
		
			[Required(ErrorMessageResourceName="SurveyGroupParticipant_Id_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "SurveyGroupParticipant_Id",ResourceType=typeof(Resources_Entities))]
			public Int32 Id { get; set; }

			[Required(ErrorMessageResourceName="SurveyGroupParticipant_SurveyId_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "SurveyGroupParticipant_SurveyId",ResourceType=typeof(Resources_Entities))]
			public Int32 SurveyId { get; set; }

			[Required(ErrorMessageResourceName="SurveyGroupParticipant_GroupParticipantId_Error",ErrorMessageResourceType=typeof(Resources_Entities))]			
    		[Display(Name = "SurveyGroupParticipant_GroupParticipantId",ResourceType=typeof(Resources_Entities))]
			public Int32 GroupParticipantId { get; set; }

    		[Display(Name = "SurveyGroupParticipant_StatusGenericTypeValueId",ResourceType=typeof(Resources_Entities))]
			public Int32 StatusGenericTypeValueId { get; set; }

    		//public EntityCollection<GenericTypeValue> StatusGenericTypeValue { get; set; }

    		//public EntityCollection<GroupParticipant> GroupParticipant { get; set; }

    		//public EntityCollection<Survey> Survey { get; set; }

		}
	}
}
	
