using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;

namespace PPI.Plugin.Survey.Controllers
{
    using PPI.Plugin.Survey.Core.Entities;
    using PPI.Plugin.Survey.Models;
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Attributes;
    using System.Data.Entity.Validation;
    using PPI.Platform.Core.Attributes;
    using System.Net;
    using PPI.Platform.Core.Attributes;
       
    [ExportController("NetworkRelationship"), PartCreationPolicy(CreationPolicy.NonShared)]
    public class NetworkRelationshipController : PluginBaseController
    {
        
        // GET: NetworkRelationship
        [HttpGet]
         
         [MvcSiteMapNodeAttribute(Title = "Relationship", ParentKey = "Survey", Attributes = @"{ ""visibility"": ""SiteMapPathHelper,!*""}")]
        public ActionResult Index(int? id, string admin)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            NwrkRelationship Relationships = new NwrkRelationship();
            
            int localid = id.GetValueOrDefault(0);
            Relationships.Relationship = _ISurveyUnitOfWork.INetworkRelationshipRepository.AsQueryable().Where(m => m.ParticipantId == localid).ToList();
            if (Relationships.Relationship.Count() == 0)
            {
                Relationships.Contacts = _ISurveyUnitOfWork.INetworkContacts_VRepository.AsQueryable().Where(m => m.ParticipantId == localid).ToList();

                if (Relationships.Contacts.Count() > 0)
                {
                    List<NetworkRelationship> listRelationship = new List<NetworkRelationship>();
                    foreach (var contact in Relationships.Contacts.ToList())
                    {
                        NetworkRelationship Relationship = new NetworkRelationship();
                        Relationship.Id = 0;
                        Relationship.ParticipantId = contact.ParticipantId;
                        Relationship.NetworkContactID = contact.ID;
                        Relationship.ContactName = contact.ContactName;
                        listRelationship.Add(Relationship);

                    }
                    Relationships.Relationship = listRelationship;

                }
            }
            else 
            {
            // Check for new Contacts
                var newContacts = _ISurveyUnitOfWork.INetworkContacts_VRepository.AsQueryable().Where(m => m.ParticipantId == localid).ToList();
                foreach (var item in newContacts)
                {                    
                    var RelItem = Relationships.Relationship.FirstOrDefault(m => m.ContactName == item.ContactName);
                    if (RelItem == null)
                    { 
                        //add the contact
                        NetworkRelationship Relationship = new NetworkRelationship();
                        Relationship.Id = 0;
                        Relationship.ParticipantId = item.ParticipantId;
                        Relationship.NetworkContactID = item.ID;
                        Relationship.ContactName = item.ContactName;
                        _ISurveyUnitOfWork.INetworkRelationshipRepository.Add(Relationship);
                        
                    }
                    
                }
                _ISurveyUnitOfWork.Commit();
                Relationships.Relationship = _ISurveyUnitOfWork.INetworkRelationshipRepository.AsQueryable().Where(m => m.ParticipantId == localid).ToList();
            }
            Relationships.SurveyReadOnly = bool.Parse((admin != null ? admin.ToString() : "false"));
            Relationships.ParticipantID = localid;
            return View(Relationships);
        }


        // POST: NetworkRelationship/Save
        [HttpPost]
         public ActionResult Save(FormCollection Collection, string direction)
        {
            int ParticipantID = Convert.ToInt32(Collection["ParticipantID"]);
            try
            {
                int count = Convert.ToInt32(Collection["RowCount"]);

                for (int i = 1; i <= count-1; i++)
                {
                    
                    int ID = Convert.ToInt32(Collection["Relationship["+i+"].ID"]);

                    var _NetworkRelationships = _ISurveyUnitOfWork.INetworkRelationshipRepository.AsQueryable().FirstOrDefault(m => m.Id ==  ID);
                    
                    if (_NetworkRelationships == null)
                        _NetworkRelationships = new NetworkRelationship();

                   // _NetworkRelationships.Id = Convert.ToInt32(Collection["Relationship["+i+"].ID"]);
                    _NetworkRelationships.ParticipantId = Convert.ToInt32(Collection["Relationship[" + i + "].UserID"]);
                    _NetworkRelationships.NetworkContactID = Convert.ToInt32(Collection["Relationship[" + i + "].NetworkContactID"]);
                    _NetworkRelationships.ContactName = Collection["Relationship[" + i + "].ContactName"];
                    if(Collection["Relationship[" + i + "].Relation"] == "VeryClose") 
                    {
                      _NetworkRelationships.VeryClose = true; 
                    }
                    else 
                    {
                      _NetworkRelationships.VeryClose = false;
                    };
                    if (Collection["Relationship[" + i + "].Relation"] == "Close")
                    {
                        _NetworkRelationships.Close = true;
                    }
                    else
                    {
                        _NetworkRelationships.Close = false;
                    };
                    if (Collection["Relationship[" + i + "].Relation"] == "NotVeryClose")
                    {
                        _NetworkRelationships.NotVeryClose = true;
                    }
                    else
                    {
                        _NetworkRelationships.NotVeryClose = false;
                    };
                    if (Collection["Relationship[" + i + "].Relation"] == "Distant")
                    {
                        _NetworkRelationships.Distant = true;
                    }
                    else
                    {
                        _NetworkRelationships.Distant = false;
                    };

                    if (_NetworkRelationships.Id == 0)
                    {
                        _NetworkRelationships.CreatedOn = System.DateTime.Now;
                        _NetworkRelationships.CreaterBy = Convert.ToInt32(Collection["Relationship[" + i + "].UserID"]);
                        _ISurveyUnitOfWork.INetworkRelationshipRepository.Add(_NetworkRelationships);
                    }
                    else
                    {
                        _NetworkRelationships.CreatedOn = Convert.ToDateTime(Collection["Relationship[" + i + "].CreatedOn"]);
                        _NetworkRelationships.CreaterBy = Convert.ToInt32(Collection["Relationship[" + i + "].CreaterBy"]);
                        _NetworkRelationships.ModifiedOn = System.DateTime.Now;
                        _NetworkRelationships.ModifiedBy = Convert.ToInt32(Collection["Relationship[" + i + "].UserID"]);
                        _ISurveyUnitOfWork.INetworkRelationshipRepository.Update(_NetworkRelationships);
                    }
                }
                _ISurveyUnitOfWork.Commit();

                if (direction == "buttonNext")
                    return RedirectToAction("Index", "RelationshipDemo", new { id = ParticipantID });
                else if (direction == "buttonPrev")
                    return RedirectToAction("Index", "Network", new { participantId = ParticipantID });
                else
                    return RedirectToAction("Index", "RelationshipDemo", new { id = ParticipantID });
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
    }
}
