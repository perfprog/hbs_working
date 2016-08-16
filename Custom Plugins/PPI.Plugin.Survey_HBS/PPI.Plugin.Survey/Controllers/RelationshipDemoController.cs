using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace PPI.Plugin.Survey.Controllers
{
    using PPI.Plugin.Survey.Core.Entities;
    using PPI.Plugin.Survey.Models;
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Mvc.Extentions;
    using System.Data.Entity.Validation;
    using PPI.Platform.Core.Attributes;
    
    [ExportController("RelationshipDemo"), PartCreationPolicy(CreationPolicy.NonShared)]
    public class RelationshipDemoController : PluginBaseController
    {
        // GET: RelationshipDemo
        public ActionResult Index(int? id, string admin)
        {
            NwkRelationshipDemo RelDemo = new NwkRelationshipDemo();
            RelDemo.SurveyReadOnly = bool.Parse((admin != null ? admin.ToString() : "false"));
            RelDemo.RelationshipDemo = _ISurveyUnitOfWork.INetworkRelationshipDemoRepository.AsQueryable().Where(m => m.ParticipantId == id).ToList();
            
            if (RelDemo.RelationshipDemo.Count() == 0)
            {
                RelDemo.Contacts = _ISurveyUnitOfWork.INetworkContacts_VRepository.AsQueryable().Where(m => m.ParticipantId == id).ToList();


                if (RelDemo.Contacts.Count() > 0)
                {
                    List<NetworkRelationshipDemo> ListRelDemo = new List<NetworkRelationshipDemo>();
                    foreach (var contact in RelDemo.Contacts.ToList())
                    {
                        NetworkRelationshipDemo RelationshipDemo = new NetworkRelationshipDemo();
                        RelationshipDemo.Id = 0;
                        RelationshipDemo.ParticipantId = contact.ParticipantId;
                        RelationshipDemo.NetworkContactID = contact.ID;
                        RelationshipDemo.ContactName = contact.ContactName;
                        ListRelDemo.Add(RelationshipDemo);

                    }
                    RelDemo.RelationshipDemo = ListRelDemo;

                }
            }
            else
            {
                // Check for new Contacts
                var newContacts = _ISurveyUnitOfWork.INetworkContacts_VRepository.AsQueryable().Where(m => m.ParticipantId == id.Value).ToList();
                foreach (var item in newContacts)
                {
                    var RelItem = RelDemo.RelationshipDemo.FirstOrDefault(m => m.ContactName == item.ContactName);
                    if (RelItem == null)
                    {
                        //add the contact
                        NetworkRelationshipDemo Relationship = new NetworkRelationshipDemo();
                        Relationship.Id = 0;
                        Relationship.ParticipantId = item.ParticipantId;
                        Relationship.NetworkContactID = item.ID;
                        Relationship.ContactName = item.ContactName;
                        _ISurveyUnitOfWork.INetworkRelationshipDemoRepository.Add(Relationship);

                    }

                }
                _ISurveyUnitOfWork.Commit();
                RelDemo.RelationshipDemo = _ISurveyUnitOfWork.INetworkRelationshipDemoRepository.AsQueryable().Where(m => m.ParticipantId == id).ToList();
            }
            ViewBag.RelationshipType = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "NetwrokRelationshipDemo_RelationshipIDType", null);
            ViewBag.Gender = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "ParticipantProfile_GenderType",null);
            ViewBag.Region = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "NetworkRelationshipDemo_RegionType", null);
            ViewBag.AgeGroup = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "ParticipantProfile_AgeGroupType",null);                        
            RelDemo.ParticipantID = Convert.ToInt32(id);
            return View(RelDemo);
        }

        // POST: Relationship/Next
        [HttpPost]
        public ActionResult Save(FormCollection Collection,string direction)
        {
            int ParticipantID = Convert.ToInt32(Collection["ParticipantID"]);
            try
            {
                int count = Convert.ToInt32(Collection["RowCount"]);

                for (int i = 0; i <= count - 1; i++)
                {

                    int ID = Convert.ToInt32(Collection["RelDemo[" + i + "].ID"]);

                    var _NetworkRelDemo = _ISurveyUnitOfWork.INetworkRelationshipDemoRepository.AsQueryable().FirstOrDefault(m => m.Id == ID);

                    if (_NetworkRelDemo == null)
                        _NetworkRelDemo = new NetworkRelationshipDemo();

                    // _NetworkRelationships.Id = Convert.ToInt32(Collection["Relationship["+i+"].ID"]);
                    _NetworkRelDemo.ParticipantId = Convert.ToInt32(Collection["RelDemo[" + i + "].UserID"]);
                    _NetworkRelDemo.NetworkContactID = Convert.ToInt32(Collection["RelDemo[" + i + "].NetworkContactID"]);
                    _NetworkRelDemo.ContactName = Collection["RelDemo[" + i + "].ContactName"];
                    if (Collection["RelDemo[" + i + "].relation"] != "")
                    {
                        _NetworkRelDemo.RelationshipID = Convert.ToInt32(Collection["RelDemo[" + i + "].relation"]);
                    }
                    if (Collection["RelDemo[" + i + "].DiffFuncArea"] == "on")
                    {
                        _NetworkRelDemo.DiffFuncArea = true;
                    }
                    else
                    {
                        _NetworkRelDemo.DiffFuncArea = false;
                    };
                    if (Collection["RelDemo[" + i + "].DiffBsnUnit"] == "on")
                    {
                        _NetworkRelDemo.DiffBsnUnit = true;
                    }
                    else
                    {
                        _NetworkRelDemo.DiffBsnUnit = false;
                    };
                    if (Collection["RelDemo[" + i + "].DiffFirm"] == "on")
                    {
                        _NetworkRelDemo.DiffFirm = true;
                    }
                    else
                    {
                        _NetworkRelDemo.DiffFirm = false;
                    };

                    if (Collection["RelDemo[" + i + "].Gender"] != "")
                    {
                        _NetworkRelDemo.Gender = Convert.ToInt32(Collection["RelDemo[" + i + "].Gender"]);
                    }

                    if (Collection["RelDemo[" + i + "].Region"] != "")
                    {
                        _NetworkRelDemo.Region = Convert.ToInt32(Collection["RelDemo[" + i + "].Region"]);
                    }
                  
                    if (Collection["RelDemo[" + i + "].SameNativeLang"] == "on")
                    {
                        _NetworkRelDemo.SameNativeLang = true;
                    }
                    else
                    {
                        _NetworkRelDemo.SameNativeLang = false;
                    };

                    if (Collection["RelDemo[" + i + "].AgeGroup"] != "")
                    {
                        _NetworkRelDemo.AgeGroup = Convert.ToInt32(Collection["RelDemo[" + i + "].AgeGroup"]);
                    }


                    if (_NetworkRelDemo.Id == 0)
                    {
                        _NetworkRelDemo.CreatedOn = System.DateTime.Now;
                        _NetworkRelDemo.CreaterBy = Convert.ToInt32(Collection["RelDemo[" + i + "].UserID"]);
                        _ISurveyUnitOfWork.INetworkRelationshipDemoRepository.Add(_NetworkRelDemo);
                    }
                    else
                    {
                        _NetworkRelDemo.CreatedOn = Convert.ToDateTime(Collection["RelDemo[" + i + "].CreatedOn"]);
                        _NetworkRelDemo.CreaterBy = Convert.ToInt32(Collection["RelDemo[" + i + "].CreaterBy"]);
                        _NetworkRelDemo.ModifiedOn = System.DateTime.Now;
                        _NetworkRelDemo.ModifiedBy = Convert.ToInt32(Collection["RelDemo[" + i + "].UserID"]);
                        _ISurveyUnitOfWork.INetworkRelationshipDemoRepository.Update(_NetworkRelDemo);
                    }
                }
                //Also shut off contacts
                var networkInfo = _ISurveyUnitOfWork.INetworkInfoContactRepository.FirstOrDefault(m => m.ParticipantId == ParticipantID);
                networkInfo.AllowNewContacts = false;
                _ISurveyUnitOfWork.INetworkInfoContactRepository.Update(networkInfo);
                _ISurveyUnitOfWork.Commit();
                if (direction == "buttonNext")
                    return RedirectToAction("Index", "RelDensity", new { id = ParticipantID });
                else if (direction == "buttonPrev")
                    return RedirectToAction("Index", "NetworkRelationship", new { id = ParticipantID });
                else
                    return RedirectToAction("Index", "RelDensity", new { id = ParticipantID });
                
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