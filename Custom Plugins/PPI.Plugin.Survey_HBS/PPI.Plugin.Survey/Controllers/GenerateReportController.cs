using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System;
using Microsoft.AspNet.Identity;
using System.IO;

namespace PPI.Plugin.Survey.Controllers
{
    using MvcSiteMapProvider;
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Mvc.Attributes;    
    using Platform.Mvc.HtmlHelpers.Model;
    using PPI.Platform.Mvc.Extentions;
    using PPI.Platform.Core.Attributes;
    using System;
    using System.IO;
    using System.Web.Routing;
    using System.Configuration;
    using System.Text;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using PPI.Plugin.Survey.Models;
    using PPI.Plugin.Survey.Core.Graph;
    using System.Net;
    

    [ExportController("GenerateReport"), PartCreationPolicy(CreationPolicy.NonShared)]
    [AllowAnonymous]
    public class GenerateReportController : PluginBaseController
    {
        
        public ActionResult NetworkSegmentReport(int participantId)
        {
            var model = new NetworkSegmentReportViewModel();
            //Try a Graph
            var Graph = new PPI.Plugin.Survey.Core.Graph.NetworkGraph();
            Graph.GraphHeight = 370;
            Graph.GraphWidth = 1090;

            var Graph2 = new PPI.Plugin.Survey.Core.Graph.NetworkGraph();
            Graph2.GraphHeight = 370;
            Graph2.GraphWidth = 1090;

            var Graph3 = new PPI.Plugin.Survey.Core.Graph.NetworkGraph();
            Graph3.GraphHeight = 370;
            Graph3.GraphWidth = 1090;

            var Graph4 = new PPI.Plugin.Survey.Core.Graph.NetworkGraph();
            Graph4.GraphHeight = 370;
            Graph4.GraphWidth = 1090;

            var Graph5 = new PPI.Plugin.Survey.Core.Graph.NetworkGraph();
            Graph5.GraphHeight = 370;
            Graph5.GraphWidth = 1090;

            var Snetwork = _ISurveyUnitOfWork.IVWhoKnowWhoRepository.AsQueryable().Where(m => m.Networks.Contains("S") && m.ParticipantId == participantId && m.SameOrganization != null).OrderBy(m => m.Order); //exclude connection from people other than You -> SomeoneElse
            var Onetwork = _ISurveyUnitOfWork.IVWhoKnowWhoRepository.AsQueryable().Where(m => m.Networks.Contains("O") && m.ParticipantId == participantId && m.SameOrganization != null).OrderBy(m => m.Order); //exclude connection from people other than You -> SomeoneElse
            var Dnetwork = _ISurveyUnitOfWork.IVWhoKnowWhoRepository.AsQueryable().Where(m => m.Networks.Contains("D") && m.ParticipantId == participantId && m.SameOrganization != null).OrderBy(m => m.Order); //exclude connection from people other than You -> SomeoneElse
            var Cnetwork = _ISurveyUnitOfWork.IVWhoKnowWhoRepository.AsQueryable().Where(m => m.ParticipantId == participantId).OrderBy(m => m.Order);            
            model.SourcePath = "..//Plugins//PPI.Plugin.Survey//Reports//";
            var physicalPath = HttpContext.Server.MapPath(model.SourcePath);
            Directory.CreateDirectory(physicalPath + "//" + participantId);
            model.SourcePath += participantId;
            
            //Graph.SetEdges("Nathan", "Phil");
            foreach (var item in Snetwork)
            {
                if (item.SameOrganization == null)
                    Graph.SetEdges(item.ContactA,NetworkGraph.VertexColors.Default, item.ContactB,NetworkGraph.VertexColors.Default);
                else if (item.SameOrganization == false)
                    Graph.SetEdges(item.ContactA, NetworkGraph.VertexColors.OutsideNetwork, item.ContactB, NetworkGraph.VertexColors.OutsideNetwork);
                else if (item.SameOrganization == true)
                    Graph.SetEdges(item.ContactA, NetworkGraph.VertexColors.InsideNetwork, item.ContactB, NetworkGraph.VertexColors.InsideNetwork);
            }

            Graph.GenerateImage(physicalPath + "//" + participantId + "//" + "tempimage.bmp");

            //Graph.SetEdges("Nathan", "Phil");
            foreach (var item in Onetwork)
            {
                if (item.SameOrganization == null)
                    Graph2.SetEdges(item.ContactA, NetworkGraph.VertexColors.Default, item.ContactB, NetworkGraph.VertexColors.Default);
                else if (item.SameOrganization == false)
                    Graph2.SetEdges(item.ContactA, NetworkGraph.VertexColors.OutsideNetwork, item.ContactB, NetworkGraph.VertexColors.OutsideNetwork);
                else if (item.SameOrganization == true)
                    Graph2.SetEdges(item.ContactA, NetworkGraph.VertexColors.InsideNetwork, item.ContactB, NetworkGraph.VertexColors.InsideNetwork);
            }

            Graph2.GenerateImage(physicalPath + "//" + participantId + "//" + "tempimage2.bmp");

            //Graph.SetEdges("Nathan", "Phil");
            foreach (var item in Dnetwork)
            {
                if (item.SameOrganization == null)
                    Graph3.SetEdges(item.ContactA, NetworkGraph.VertexColors.Default, item.ContactB, NetworkGraph.VertexColors.Default);
                else if (item.SameOrganization == false)
                    Graph3.SetEdges(item.ContactA, NetworkGraph.VertexColors.OutsideNetwork, item.ContactB, NetworkGraph.VertexColors.OutsideNetwork);
                else if (item.SameOrganization == true)
                    Graph3.SetEdges(item.ContactA, NetworkGraph.VertexColors.InsideNetwork, item.ContactB, NetworkGraph.VertexColors.InsideNetwork);
            }

            Graph3.GenerateImage(physicalPath + "//" + participantId + "//" + "tempimage3.bmp");

            //Graph.SetEdges("Nathan", "Phil");
            foreach (var item in Cnetwork)
            {
                if (item.RelationshipID == null)
                    Graph4.SetEdges(item.ContactA, NetworkGraph.VertexColors.Default, item.ContactB, NetworkGraph.VertexColors.Default);
                else if (item.RelationshipID == 1209) // senior
                    Graph4.SetEdges(item.ContactA, NetworkGraph.VertexColors.Senior, item.ContactB, NetworkGraph.VertexColors.Senior);
                else if (item.RelationshipID == 1210) //peer
                    Graph4.SetEdges(item.ContactA, NetworkGraph.VertexColors.Peer, item.ContactB, NetworkGraph.VertexColors.Peer);
                else if (item.RelationshipID == 1211) //peer
                    Graph4.SetEdges(item.ContactA, NetworkGraph.VertexColors.Junior, item.ContactB, NetworkGraph.VertexColors.Junior);
                else if (item.RelationshipID == 1212) //peer
                    Graph4.SetEdges(item.ContactA, NetworkGraph.VertexColors.Other, item.ContactB, NetworkGraph.VertexColors.Other);
            }

            Graph4.GenerateImage(physicalPath + "//" + participantId + "//" + "tempimage4.bmp");

            //Graph.SetEdges("Nathan", "Phil");
            foreach (var item in Cnetwork)
            {
                if (item.SameOrganization == null)
                    Graph5.SetEdges(item.ContactA, NetworkGraph.VertexColors.Default, item.ContactB, NetworkGraph.VertexColors.Default);
                else if (item.SameOrganization == false)
                    Graph5.SetEdges(item.ContactA, NetworkGraph.VertexColors.OutsideNetwork, item.ContactB, NetworkGraph.VertexColors.OutsideNetwork);
                else if (item.SameOrganization == true)
                    Graph5.SetEdges(item.ContactA, NetworkGraph.VertexColors.InsideNetwork, item.ContactB, NetworkGraph.VertexColors.InsideNetwork);
            }

            Graph5.GenerateImage(physicalPath + "//" + participantId + "//" + "tempimage5.bmp");

            var Participant = _IPlatformUnitOfWork.IParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == participantId).Person;
            if (Participant != null)
            {
                model.FullName = Participant.First_Name + " " + Participant.Last_Name;
                model.CreateDate = DateTime.Now.ToShortDateString();
            }
            else
            {
                model.FullName = "Invalid User";
                model.CreateDate = "Today";
            }
            float TotalPeopleNetwork = Cnetwork.Count(m => m.Order == -1);
            float MaxDensity = TotalPeopleNetwork * (TotalPeopleNetwork - 1) / 2;
            float TotalChecks = Cnetwork.Count(m => m.Order == 1);
            float Total = (TotalChecks / MaxDensity);
            model.NetworkDensity = Total.ToString("0.00");

            //Doughnut

            model.VeryClosePercent = ((_ISurveyUnitOfWork.INetworkRelationshipRepository.AsQueryable().Count(m => m.VeryClose == true && m.ParticipantId == participantId) / TotalPeopleNetwork) * 100).ToString("0.00");
            model.NotVeryClosePercent = ((_ISurveyUnitOfWork.INetworkRelationshipRepository.AsQueryable().Count(m => m.NotVeryClose == true && m.ParticipantId == participantId) / TotalPeopleNetwork)* 100).ToString("0.00");
            model.DistantPercent = ((_ISurveyUnitOfWork.INetworkRelationshipRepository.AsQueryable().Count(m => m.Distant == true && m.ParticipantId == participantId) / TotalPeopleNetwork)* 100).ToString("0.00");
            model.ClosePercent = ((_ISurveyUnitOfWork.INetworkRelationshipRepository.AsQueryable().Count(m => m.Close == true && m.ParticipantId == participantId) / TotalPeopleNetwork)* 100).ToString("0.00");

            //Bars
            var NetworkDemo = _ISurveyUnitOfWork.INetworkRelationshipDemoRepository.AsQueryable().Where(m => m.ParticipantId == participantId);
            var TotalCount = NetworkDemo.Count();
            var Profile = _ISurveyUnitOfWork.IParticipantProfileRepository.AsQueryable().FirstOrDefault(m => m.ParticipantID == participantId);
            
            var Company = new BarScales(){Same = true, Different = false };
            Company.SamePercent = NetworkDemo.Count(m => m.DiffFirm == true);
            Company.SamePercent = (Company.SamePercent / TotalCount) * 100 + 3;
            Company.DifferentPercent = NetworkDemo.Count(m => m.DiffFirm == false);
            Company.DifferentPercent = (Company.DifferentPercent / TotalCount) * 100 + 3;
            
            var BusinessUnit = new BarScales() { Same = true, Different = false };
            BusinessUnit.SamePercent = NetworkDemo.Count(m => m.DiffBsnUnit == true);
            BusinessUnit.SamePercent = (BusinessUnit.SamePercent / TotalCount) * 100 + 3;
            BusinessUnit.DifferentPercent = NetworkDemo.Count(m => m.DiffBsnUnit == false);
            BusinessUnit.DifferentPercent = (BusinessUnit.DifferentPercent / TotalCount) * 100 + 3;
            
            var FunctionArea = new BarScales() {Same = true, Different = false };
            FunctionArea.SamePercent = NetworkDemo.Count(m => m.DiffFuncArea == true);
            FunctionArea.SamePercent = (FunctionArea.SamePercent / TotalCount) * 100 + 3;
            FunctionArea.DifferentPercent = NetworkDemo.Count(m => m.DiffFuncArea == false);
            FunctionArea.DifferentPercent = (FunctionArea.DifferentPercent / TotalCount) * 100 + 3;

            var Language = new BarScales() {Same = true, Different = false };
            Language.SamePercent = NetworkDemo.Count(m => m.SameNativeLang == true) ;
            Language.SamePercent = (Language.SamePercent / TotalCount) * 100 + 3;
            Language.DifferentPercent = NetworkDemo.Count(m => m.SameNativeLang == false);
            Language.DifferentPercent = (Language.DifferentPercent / TotalCount) * 100 + 3;

            model.Company = Company;
            model.FunctionArea = FunctionArea;
            model.NativeLanguage = Language;
            model.BusinessUnit = BusinessUnit;
                        
            SelectListItem GenderSelectedItem = null;
            SelectListItem AgeGroupSelectedItem = null;
            SelectListItem RegionSelectedItem = null;

            //Relationship
            SelectList RelationshipSelect =  _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "NetwrokRelationshipDemo_RelationshipIDType", null);
            model.RelationShipType = new List<BarScaleTypes>();            
            foreach (SelectListItem item in RelationshipSelect.ToList())
            {
                
                int orginalValue = int.Parse(item.Value);
                float newValue = NetworkDemo.Count(m => m.RelationshipID == orginalValue);
                newValue = (newValue / TotalCount) * 100 + 3;                
                var newItem = new BarScaleTypes {Text = item.Text, Value = newValue.ToString("0.00"),Selected=false};
                model.RelationShipType.Add(newItem);
            }
            //Gender
            SelectList GenderSelect = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "ParticipantProfile_GenderType", null);
            model.Gender = new List<BarScaleTypes>();

            foreach (SelectListItem item in GenderSelect.ToList())
            {

                int orginalValue = int.Parse(item.Value);
                float newValue = NetworkDemo.Count(m => m.Gender == orginalValue);
                newValue = (newValue / TotalCount) * 100 + 3;
                var newItem = new BarScaleTypes() { Text = item.Text, Value = newValue.ToString("0.00") };
                if (Profile.GenderId == orginalValue)
                    newItem.Selected = true;
                model.Gender.Add(newItem);
            }
            // Region
            SelectList RegionSelect = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "NetworkRelationshipDemo_RegionType", null);
            model.Regions = new List<BarScaleTypes>();

            foreach (SelectListItem item in RegionSelect.ToList())
            {

                int orginalValue = int.Parse(item.Value);
                float newValue = NetworkDemo.Count(m => m.Region == orginalValue);
                newValue = (newValue / TotalCount) * 100 + 3;
                var newItem = new BarScaleTypes { Text = item.Text, Value = newValue.ToString("0.00") };
                if (Profile.RegionId == orginalValue)
                    newItem.Selected = true;
                model.Regions.Add(newItem);
            }
            // Age Group
            // Region
            SelectList AgeGroupSelect = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "ParticipantProfile_AgeGroupType", null);
            model.AgeGroup = new List<BarScaleTypes>();

            foreach (SelectListItem item in AgeGroupSelect.ToList())
            {

                int orginalValue = int.Parse(item.Value);
                float newValue = NetworkDemo.Count(m => m.AgeGroup == orginalValue);
                newValue = (newValue / TotalCount) * 100 + 3;
                var newItem = new BarScaleTypes() { Text = item.Text, Value = newValue.ToString("0.00") };
                if (Profile.AgeGroupId == orginalValue)
                    newItem.Selected = true;
                model.AgeGroup.Add(newItem);
            }
            return PartialView(model);
        }
        [HttpGet]
        public async Task<ActionResult> CompositeReport()
        {

            try
            {
                var baseAddress = ConfigurationManager.AppSettings["PPI.Platform.ReportServices"] + "CompositeReports";
                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";
                var ReportRequest = new PPI.Platform.Core.Models.RequestCompositeReports();
                var user = User.Identity.GetUserId();
                var emailaddress = _IPlatformUnitOfWork.IAspNetUserRepository.AsQueryable().FirstOrDefault(m => m.Id == user);
                ReportRequest.EmailAddress = emailaddress.Email;
                ReportRequest.ParticipantIds = TempData["participants"] as string[];                
                ReportRequest.ReportActionRoot = Request.Url.GetLeftPart(UriPartial.Authority);
                string parsedContent = JsonConvert.SerializeObject(ReportRequest);
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);
                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();
                Task.Run(() => http.GetResponse());
                return RedirectToAction("Index", "Report");

            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            
        }
    }
}
