using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Data.Entity.Validation;

namespace PPI.Plugin.Survey.Controllers
{
    using PPI.Plugin.Survey.Core.Entities;
    using PPI.Plugin.Survey.Models;
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Mvc.Extentions;
    using PPI.Platform.Core.Attributes;
    using PPI.Platform.Core.Attributes;
    

    [ExportController("Profile"), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProfileController : PluginBaseController
    {
        // GET: Profile
        
        public ActionResult Index(int? id,string admin)
        {
            PcptProfile PProfile = new PcptProfile();
            int localid = id.GetValueOrDefault(0);
            int? selectAgeGroup = null;            
            int? selectGender = null;
            int? selectNetworkPercent = null;
            int? selectRegion = null;
            int? selectYesNo = null;
            int? selectWorkRegion = null;
            PProfile.PartProfile = _ISurveyUnitOfWork.IParticipantProfileRepository.AsQueryable().FirstOrDefault(m => m.ParticipantID == localid);
            if (PProfile.PartProfile == null && localid != 0)
            {              
                //Lookup the networkContact ID and put it with the profile also
                var contactId = _ISurveyUnitOfWork.INetworkInfoContactRepository.AsQueryable().FirstOrDefault(m => m.ParticipantId == localid);
                ParticipantProfile Partipant = new ParticipantProfile();
                Partipant.NetworkContactID = contactId.Id;
                Partipant.ParticipantID = localid;
                PProfile.PartProfile = Partipant;
            }
            else
            {
                selectAgeGroup = PProfile.PartProfile.AgeGroupId;
                selectNetworkPercent = PProfile.PartProfile.NetworkPercentId;                
                selectGender = PProfile.PartProfile.GenderId;
                selectRegion = PProfile.PartProfile.RegionId;
                selectWorkRegion = PProfile.PartProfile.WorkRegionId;
                selectYesNo = PProfile.PartProfile.NativeLang;
            }


            ViewBag.YesNo = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "Generic_YesNoType", selectYesNo);
            ViewBag.Gender = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "ParticipantProfile_GenderType", selectGender);
            ViewBag.AgeGroup = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "ParticipantProfile_AgeGroupType", selectAgeGroup);
            ViewBag.Region = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "NetworkRelationshipDemo_RegionType", selectRegion);
            ViewBag.WorkRegion = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "NetworkRelationshipDemo_RegionType", selectWorkRegion);
            
            ViewBag.NetworkPercent = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "ParticipantProfile_NetworkPercentType", selectNetworkPercent);
            PProfile.SurveyReadOnly = bool.Parse((admin != null ? admin.ToString() : "false"));
            return View(PProfile);
        }



        // POST: Profile/Save
        [HttpPost]
        public ActionResult Save(FormCollection Collection)
        {
            
            try
            {                
                int ParticipantID = Convert.ToInt32(Collection["ParticipantID"]);
                
                var _ParticipantProfile = _ISurveyUnitOfWork.IParticipantProfileRepository.AsQueryable().FirstOrDefault(m => m.ParticipantID == ParticipantID);

                if (_ParticipantProfile == null)
                    _ParticipantProfile = new ParticipantProfile();

                _ParticipantProfile.NetworkContactID = Convert.ToInt32(Collection["PartProfile.NetworkContactID"]);
                _ParticipantProfile.ParticipantID = ParticipantID;
                //Changed to not required

                int? blankint = null;
                _ParticipantProfile.GenderId = String.IsNullOrWhiteSpace(Collection["Gender"]) ? blankint : Convert.ToInt32(Collection["Gender"]);
                _ParticipantProfile.AgeGroupId = String.IsNullOrWhiteSpace(Collection["AgeGroupId"]) ? blankint : Convert.ToInt32(Collection["AgeGroupId"]);
                _ParticipantProfile.RegionId = String.IsNullOrWhiteSpace(Collection["RegionId"]) ? blankint : Convert.ToInt32(Collection["RegionId"]);
                _ParticipantProfile.NetworkPercentId = String.IsNullOrWhiteSpace(Collection["NetworkPercentId"]) ? blankint : Convert.ToInt32(Collection["NetworkPercentId"]);
                _ParticipantProfile.NativeLang = String.IsNullOrWhiteSpace(Collection["NativeLang"]) ? blankint : Convert.ToInt32(Collection["NativeLang"]);
                _ParticipantProfile.WorkRegionId = String.IsNullOrWhiteSpace(Collection["WorkRegionId"]) ? blankint : Convert.ToInt32(Collection["WorkRegionId"]);
                
                
                if (_ParticipantProfile.id == 0)
                {
                    _ParticipantProfile.CreatedOn = System.DateTime.Now;
                    _ParticipantProfile.CreaterBy = ParticipantID;
                    _ISurveyUnitOfWork.IParticipantProfileRepository.Add(_ParticipantProfile);
                }
                else
                {
                    _ParticipantProfile.CreatedOn =_ParticipantProfile.CreatedOn;
                    _ParticipantProfile.CreaterBy = _ParticipantProfile.CreaterBy;
                    _ParticipantProfile.ModifiedOn = System.DateTime.Now;
                    _ParticipantProfile.ModifiedBy = ParticipantID;
                    _ISurveyUnitOfWork.IParticipantProfileRepository.Update(_ParticipantProfile);
                }

                _ISurveyUnitOfWork.Commit();
                return RedirectToAction("SurveyComplete", "Survey", new { participantId = ParticipantID, statusId = 1009 });
                
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        //public ActionResult Back(int? id)
        //{
        //    return RedirectToAction("Index", "RelDensity", new { id = id });
        //}


    }
}