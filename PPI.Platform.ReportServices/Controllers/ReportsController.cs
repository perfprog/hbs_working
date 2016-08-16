using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PPI.Platform.ReportServices.Controllers
{
    using PPI.Platform.Core.Domain;
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Core.Logging.Interface;
    using PPI.Platform.Core.Utility;
    using PPI.Platform.Logging;
    using PPI.Platform.Core.Logging.Interface;
    using PPI.Platform.Core.Attributes;
    using System.IO;
    using PPI.Platform.Core.Logging.Interface;
    using PPI.Platform.Core.Attributes;
    using PPI.Platform.ReportServices.Models;
    using PPI.Platform.Email;
    using System.Web;
    using System.Configuration;
    using System.Net.Mail;
    using System.Net.Mime;
    using PPI.Platform.Core.Models;
    using System.Net.Http.Headers;
    [Logging(AttributeExclude = true)]      
    [AllowAnonymous]
    public class ReportsController : ApiController
    {
        public IPlatformUnitOfWork _IPlatformUnitOfWork { get; set; }
        public Platform.Core.Pdf.Interface.IHtmlToPdf _HtmlToPdf {get;set;}

        public Platform.Core.Zip.Interface.IZip _Zip { get; set; }
        
        public ILogging _Logging {get;set;}

        public ReportsController()
        {
            _Logging = Logging.Instance;
            _IPlatformUnitOfWork = new PPI.Platform.Core.Domain.EFUnitOfWork();
            _HtmlToPdf = new PPI.Platform.Pdf.HtmlToPdf();
            _Zip = new PPI.Platform.Zip.Zip();
        }
        public void Post(RequestReports ReportRequest)
        {
            if (ReportRequest != null)
            { 
                _HtmlToPdf.SetAllMargins(0);
                _HtmlToPdf.PageOrientation = Platform.Core.Pdf.PageOrientation.Landscape;
                _HtmlToPdf.PageSize = Platform.Core.Pdf.PageSize.Letter;
                //var Root = this.Url.Request.RequestUri.GetLeftPart(UriPartial.Authority);
                var httpcontext = this.Request.Properties["MS_HttpContext"] as HttpContextWrapper;
                var FilePath = httpcontext.Server.MapPath("~/Reports");
                Guid fileName = Guid.NewGuid();
                FilePath = System.IO.Path.Combine(FilePath, fileName.ToString() + ".zip");
                _Zip.New(FilePath);
                foreach (var item in ReportRequest.ParticipantIds)
                {

                    var ReportPath = ReportRequest.ReportActionRoot + "/GenerateReport/NetworkSegmentReport?participantId=" + item;
                    int id = int.Parse(item);
                    var GroupName = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.ParticipantId == id);
                    string Group = "";
                    if (GroupName != null)
                        Group = GroupName.Group.Name.Replace(" ", "_");
                    var Participant = _IPlatformUnitOfWork.IParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == id);
                    _Zip.AddFiles(_HtmlToPdf.ConvertUrlToMemory(ReportPath), Participant.Person.Last_Name + "_" + Participant.Person.First_Name + "_" + Group + "_Network_Diagnostic_Survey.pdf");
                }
                _Zip.Save();
                FileInfo ZipFile = new FileInfo(FilePath);
               //Send email

                var email = new PPI.Platform.Email.SendGridEmail();
                var emailmessage = new System.Net.Mail.MailMessage();

                var Credentials = new NetworkCredential(
                    PPI.Platform.Core.Utility.MachineKeyWrapper.UnprotectUrlSafeString(ConfigurationManager.AppSettings["mailAccount"], "configuration"),
                    PPI.Platform.Core.Utility.MachineKeyWrapper.UnprotectUrlSafeString(ConfigurationManager.AppSettings["mailPassword"], "configuration")
                     );

                emailmessage.To.Add(new MailAddress(ReportRequest.EmailAddress));
                emailmessage.From = new MailAddress(ConfigurationManager.AppSettings["OWINAccountFromAddress"]);
                emailmessage.Subject = "Your HBS Diagnostic Survey Reports";                
                emailmessage.IsBodyHtml = true;
                var filename = fileName.ToString();
                emailmessage.Body = "<p>Your HBS Diagnostic Survey Reports have been generated successfully!</p>";
                emailmessage.Body += "<p>Please click the 'Requested Reports' link to view and download the reports.</p>";
                emailmessage.Body += "<p><a href='" + this.Request.RequestUri.ToString() + "/" + filename + "'>Requested Report</a></p>";
                           
                email.Send(emailmessage, new Core.Email.Model.EmailConfiguration() { Credentials = Credentials });
                //file.Close();
            }
        }        
        public HttpResponseMessage Get(string id)
        {
            try
            {

                var httpcontext = this.Request.Properties["MS_HttpContext"] as HttpContextWrapper;
                var FilePath = httpcontext.Server.MapPath("~/Reports");
                FilePath = System.IO.Path.Combine(FilePath, id + ".zip");
                var path = FilePath;
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(path, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = id + ".zip"
                };
                return result;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            

        }

    }
}
