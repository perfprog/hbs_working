using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Net;
using System;    

namespace PPI.Plugin.Survey.Controllers
{
    using PPI.Plugin.Survey.Core.Entities;
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Core.Attributes;
    using PPI.Plugin.Survey.Models;
    

    [ExportController("Network"), PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize(Roles = "Participant,Admin,GroupAdmin")]
    public class NetworkController : PluginBaseController
    {

        // GET: Administration        

        // [MvcSiteMapNodeAttribute(Title = "Network Contacts", ParentKey = "Survey", Attributes = @"{ ""visibility"": ""SiteMapPathHelper,!*""}")]
        public ActionResult Index(int? participantId,string admin)
        {
            if (participantId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            var model = new PPI.Plugin.Survey.Models.NetworkViewModel();
            model.NetworkInfoContact = _ISurveyUnitOfWork.INetworkInfoContactRepository.AsQueryable().FirstOrDefault(m => m.ParticipantId == participantId);
            model.SurveyReadOnly = bool.Parse((admin != null ? admin.ToString() : "false"));
            if (model.NetworkInfoContact == null)
            {
                model.NetworkInfoContact = new NetworkInfoContact();
                model.NetworkInfoContact.ParticipantId = participantId.GetValueOrDefault();
                model.NetworkInfoContact.CreatedOn = DateTime.Now;
                model.NetworkInfoContact.CreaterBy = participantId.GetValueOrDefault();
                model.NetworkInfoContact.AllowNewContacts = true;
                model.NetworkInfoContact.Id = 0;
            }
            else
            {
                //set the modified by
                model.NetworkInfoContact.ModifiedBy = participantId.GetValueOrDefault();
                model.NetworkInfoContact.ModifiedOn = DateTime.Now;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Next(NetworkViewModel NetworkContacts)
        {
            if (ModelState.IsValid)
            {

                if (NetworkContacts.NetworkInfoContact.Id != 0)
                {
                    _ISurveyUnitOfWork.INetworkInfoContactRepository.Update(NetworkContacts.NetworkInfoContact);
                }
                else
                {
                    _ISurveyUnitOfWork.INetworkInfoContactRepository.Add(NetworkContacts.NetworkInfoContact);
                }
                var surveyStatus = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.GroupParticipant.ParticipantId == NetworkContacts.NetworkInfoContact.ParticipantId && m.SurveyId == 1); // hard coded one survey
                surveyStatus.StatusGenericTypeValueId = 1011;
                _IPlatformUnitOfWork.Commit();
                _ISurveyUnitOfWork.Commit();
            }
            return RedirectToAction("Index", "NetworkRelationship", new { id = NetworkContacts.NetworkInfoContact.ParticipantId });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clear(int participantId)
        {
            var DeleteContacts = _ISurveyUnitOfWork.INetworkInfoContactRepository.AsQueryable().Where(m => m.ParticipantId == participantId);
            _ISurveyUnitOfWork.INetworkInfoContactRepository.Delete(DeleteContacts);
            _ISurveyUnitOfWork.Commit();
            return RedirectToAction("Index", new { @participantId = participantId });
        }
    }
}