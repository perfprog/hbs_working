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
    using System.Data.Entity.Validation;
    using PPI.Platform.Core.Attributes;
        
    [ExportController("RelDensity"), PartCreationPolicy(CreationPolicy.NonShared)]
    public class RelDensityController : PluginBaseController
    {
        // GET: RelDensity
        public ActionResult Index(int? id,string admin)
        {
            NwkRelDensity RelDen = new NwkRelDensity();
            int IndexCount = 0;

            RelDen.RelDensity = _ISurveyUnitOfWork.IRelationshipDensityRepository.AsQueryable().Where(m => m.ParticipantId == id).ToList().OrderBy(r => r.ContactName);
            RelDen.Contacts = _ISurveyUnitOfWork.INetworkContacts_VRepository.AsQueryable().Where(m => m.ParticipantId == id).ToList().OrderBy(r => r.ContactName);
            if (RelDen.RelDensity.Count() == 0)
            {
                //new contact was add or none exist in db; purge the list and start again                
                if (RelDen.Contacts.Count() > 0)
                {

                    List<RelationshipDensity> listRelationshipDensity = new List<RelationshipDensity>();

                    List<string> ListCnt = new List<string>();
                    foreach (var contact in RelDen.Contacts)
                    {
                        RelationshipDensity RelationshipDensity = new RelationshipDensity();
                        RelationshipDensity.Id = 0;
                        RelationshipDensity.ParticipantId = contact.ParticipantId;
                        RelationshipDensity.NetworkContactID = contact.ID;
                        RelationshipDensity.ContactName = contact.ContactName;
                        int i = 1;
                        ListCnt.Add(contact.ContactName);
                        foreach (var Mapcontact in RelDen.Contacts)
                        {
                            //int pos = ListCnt.IndexOf(Mapcontact.ContactName);
                            //if (pos == -1)
                            //{
                                switch (i)
                                {

                                    case 2:
                                        RelationshipDensity.MapContactName2 = Mapcontact.ContactName;
                                        break;
                                    case 3:
                                        RelationshipDensity.MapContactName3 = Mapcontact.ContactName;
                                        break;
                                    case 4:
                                        RelationshipDensity.MapContactName4 = Mapcontact.ContactName;
                                        break;
                                    case 5:
                                        RelationshipDensity.MapContactName5 = Mapcontact.ContactName;
                                        break;
                                    case 6:
                                        RelationshipDensity.MapContactName6 = Mapcontact.ContactName;
                                        break;
                                    case 7:
                                        RelationshipDensity.MapContactName7 = Mapcontact.ContactName;
                                        break;
                                    case 8:
                                        RelationshipDensity.MapContactName8 = Mapcontact.ContactName;
                                        break;
                                    case 9:
                                        RelationshipDensity.MapContactName9 = Mapcontact.ContactName;
                                        break;
                                    case 10:
                                        RelationshipDensity.MapContactName10 = Mapcontact.ContactName;
                                        break;
                                    case 11:
                                        RelationshipDensity.MapContactName11 = Mapcontact.ContactName;
                                        break;
                                    case 12:
                                        RelationshipDensity.MapContactName12 = Mapcontact.ContactName;
                                        break;
                                    case 13:
                                        RelationshipDensity.MapContactName13 = Mapcontact.ContactName;
                                        break;
                                    case 14:
                                        RelationshipDensity.MapContactName14 = Mapcontact.ContactName;
                                        break;
                                    case 15:
                                        RelationshipDensity.MapContactName15 = Mapcontact.ContactName;
                                        break;
                                    case 16:
                                        RelationshipDensity.MapContactName16 = Mapcontact.ContactName;
                                        break;
                                    case 17:
                                        RelationshipDensity.MapContactName17 = Mapcontact.ContactName;
                                        break;
                                    case 18:
                                        RelationshipDensity.MapContactName18 = Mapcontact.ContactName;
                                        break;
                                    case 19:
                                        RelationshipDensity.MapContactName19 = Mapcontact.ContactName;
                                        break;
                                    case 20:
                                        RelationshipDensity.MapContactName20 = Mapcontact.ContactName;
                                        break;
                                    case 21:
                                        RelationshipDensity.MapContactName21 = Mapcontact.ContactName;
                                        break;
                                    case 22:
                                        RelationshipDensity.MapContactName22 = Mapcontact.ContactName;
                                        break;
                                    case 23:
                                        RelationshipDensity.MapContactName23 = Mapcontact.ContactName;
                                        break;
                                }
                                // ArrContact[i] = Mapcontact.ContactName;
                           // }
                            i++;
                        }
                        //RelationshipDensity.IndexCount = i;
                        listRelationshipDensity.Add(RelationshipDensity);
                    }

                    RelDen.RelDensity = listRelationshipDensity;
                }

            }
            RelDen.ParticipantID = Convert.ToInt32(id);
            RelDen.SurveyReadOnly = bool.Parse((admin != null ? admin.ToString() : "false"));
            return View(RelDen);
        }


     // POST: Relationship/Next
        [HttpPost]
        public ActionResult Save(FormCollection Collection, string direction)
        {
            int ParticipantID = Convert.ToInt32(Collection["RelDen[0].ParticipantID"]);
            try
            {
                 int count = Convert.ToInt32(Collection["RowCount"]);

                 for (int i = 0; i <= count - 1; i++)
                 {
                     int ID = Convert.ToInt32(Collection["RelDen[" + i + "].ID"]);

                     var _RelDen = _ISurveyUnitOfWork.IRelationshipDensityRepository.AsQueryable().FirstOrDefault(m => m.Id == ID);

                     if (_RelDen == null)
                         _RelDen = new RelationshipDensity();

                     _RelDen.ParticipantId = Convert.ToInt32(Collection["RelDen[" + i + "].ParticipantID"]);
                     _RelDen.NetworkContactID = Convert.ToInt32(Collection["RelDen[" + i + "].NetworkContactID"]);
                     _RelDen.ContactName = Collection["RelDen[" + i + "].ContactName"];
                     _RelDen.MapContactName2 = Collection["RelDen.MapContactName2"];
                     _RelDen.MapContactName3 = Collection["RelDen.MapContactName3"];
                     _RelDen.MapContactName4 = Collection["RelDen.MapContactName4"];
                     _RelDen.MapContactName5 = Collection["RelDen.MapContactName5"];
                     _RelDen.MapContactName6 = Collection["RelDen.MapContactName6"];
                     _RelDen.MapContactName7 = Collection["RelDen.MapContactName7"];
                     _RelDen.MapContactName8 = Collection["RelDen.MapContactName8"];
                     _RelDen.MapContactName9 = Collection["RelDen.MapContactName9"];
                     _RelDen.MapContactName10 = Collection["RelDen.MapContactName10"];
                     _RelDen.MapContactName11 = Collection["RelDen.MapContactName11"];
                     _RelDen.MapContactName12 = Collection["RelDen.MapContactName12"];
                     _RelDen.MapContactName13 = Collection["RelDen.MapContactName13"];
                     _RelDen.MapContactName14 = Collection["RelDen.MapContactName14"];
                     _RelDen.MapContactName15 = Collection["RelDen.MapContactName15"];
                     _RelDen.MapContactName16 = Collection["RelDen.MapContactName16"];
                     _RelDen.MapContactName17 = Collection["RelDen.MapContactName17"];
                     _RelDen.MapContactName18 = Collection["RelDen.MapContactName18"];
                     _RelDen.MapContactName19 = Collection["RelDen.MapContactName19"];
                     _RelDen.MapContactName20 = Collection["RelDen.MapContactName20"];
                     _RelDen.MapContactName21 = Collection["RelDen.MapContactName21"];
                     _RelDen.MapContactName22 = Collection["RelDen.MapContactName22"];
                     _RelDen.MapContactName23 = Collection["RelDen.MapContactName23"];

                     if (Collection["RelDen[" + i + "].DensitySelected2"] == "on")
                     {
                         _RelDen.DensitySelected2 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected2 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected3"] == "on")
                     {
                         _RelDen.DensitySelected3 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected3 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected4"] == "on")
                     {
                         _RelDen.DensitySelected4 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected4 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected5"] == "on")
                     {
                         _RelDen.DensitySelected5 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected5 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected6"] == "on")
                     {
                         _RelDen.DensitySelected6 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected6 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected7"] == "on")
                     {
                         _RelDen.DensitySelected7 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected7 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected8"] == "on")
                     {
                         _RelDen.DensitySelected8 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected8 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected9"] == "on")
                     {
                         _RelDen.DensitySelected9 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected9 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected10"] == "on")
                     {
                         _RelDen.DensitySelected10 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected10 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected11"] == "on")
                     {
                         _RelDen.DensitySelected11 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected11 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected12"] == "on")
                     {
                         _RelDen.DensitySelected12 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected12 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected13"] == "on")
                     {
                         _RelDen.DensitySelected13 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected13 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected14"] == "on")
                     {
                         _RelDen.DensitySelected14 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected14 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected15"] == "on")
                     {
                         _RelDen.DensitySelected15 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected15 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected16"] == "on")
                     {
                         _RelDen.DensitySelected16 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected16 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected17"] == "on")
                     {
                         _RelDen.DensitySelected17 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected17 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected18"] == "on")
                     {
                         _RelDen.DensitySelected18 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected18 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected19"] == "on")
                     {
                         _RelDen.DensitySelected19 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected19 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected20"] == "on")
                     {
                         _RelDen.DensitySelected20 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected20 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected21"] == "on")
                     {
                         _RelDen.DensitySelected21 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected21 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected22"] == "on")
                     {
                         _RelDen.DensitySelected22 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected22 = false;
                     };
                     if (Collection["RelDen[" + i + "].DensitySelected23"] == "on")
                     {
                         _RelDen.DensitySelected23 = true;
                     }
                     else
                     {
                         _RelDen.DensitySelected23 = false;
                     };
                     if (_RelDen.Id == 0)
                     {
                         _RelDen.CreatedOn = System.DateTime.Now;
                         _RelDen.CreaterBy = Convert.ToInt32(Collection["RelDen[" + i + "].ParticipantID"]);
                         _ISurveyUnitOfWork.IRelationshipDensityRepository.Add(_RelDen);
                     }
                     else
                     {
                         _RelDen.CreatedOn = Convert.ToDateTime(Collection["RelDen[" + i + "].CreatedOn"]);
                         _RelDen.CreaterBy = Convert.ToInt32(Collection["RelDen[" + i + "].CreaterBy"]);
                         _RelDen.ModifiedOn = System.DateTime.Now;
                         _RelDen.ModifiedBy = Convert.ToInt32(Collection["RelDen[" + i + "].ParticipantID"]);
                         _ISurveyUnitOfWork.IRelationshipDensityRepository.Update(_RelDen);
                     }
                 }
                 _ISurveyUnitOfWork.Commit();

                 if (direction == "buttonNext")
                     return RedirectToAction("Index", "Profile", new { id = ParticipantID });                     
                 else if (direction == "buttonPrev")
                     return RedirectToAction("Index", "RelationshipDemo", new { id = ParticipantID });
                 else
                     return RedirectToAction("Index", "Profile", new { id = ParticipantID });
                
                
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
        //    return RedirectToAction("Index", "RelationshipDemo", new { id = id });
        //}

    }
}