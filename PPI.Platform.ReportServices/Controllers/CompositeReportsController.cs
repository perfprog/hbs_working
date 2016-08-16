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
    using CsvHelper;
    using System.Text;
    using PPI.Plugin.Survey.Core.Entities;
    [Logging(AttributeExclude = true)]      
    [AllowAnonymous]
    public class CompositeReportsController : ApiController
    {
        public IPlatformUnitOfWork _IPlatformUnitOfWork { get; set; }

        public PPI.Plugin.Survey.Core.Domain.ISurveyUnitOfWork _ISurveyUnitOfWork { get; set; }
        public Platform.Core.Pdf.Interface.IHtmlToPdf _HtmlToPdf {get;set;}

        public Platform.Core.Zip.Interface.IZip _Zip { get; set; }
        
        public ILogging _Logging {get;set;}

        public CompositeReportsController()
        {
            _Logging = Logging.Instance;
            _ISurveyUnitOfWork = new PPI.Plugin.Survey.Core.Domain.EFUnitOfWork();
            _HtmlToPdf = new PPI.Platform.Pdf.HtmlToPdf();
            _Zip = new PPI.Platform.Zip.Zip();
        }
        public void Post(RequestCompositeReports ReportRequest)
        {

            try
            {
                List<int> toList = new List<int>();
                string[] participants = ReportRequest.ParticipantIds;
                foreach (var item in participants)
                {
                    toList.Add(int.Parse(item));
                }
                int[] list = toList.ToArray<int>();

                var model = _ISurveyUnitOfWork.IvCompositeReportRepository.AsQueryable().Where(m => list.Contains(m.Id));                
                

                string exportName = Guid.NewGuid().ToString() + "_CompositeReport.csv";
                CsvWriter writer;
                var streamoutput = new MemoryStream();
                var sw = new StreamWriter(streamoutput);

                if (model.Count() > 0)
                {
                    var CsvConfig = new CsvHelper.Configuration.CsvConfiguration();
                    CsvConfig.Encoding = new UTF8Encoding(true);
                    writer = new CsvWriter(sw, CsvConfig);
                    IEnumerable<vCompositeReport> records = model.ToList();
                    writer.WriteRecords(records);
                    sw.Flush();
                }
                var httpcontext = this.Request.Properties["MS_HttpContext"] as HttpContextWrapper;
                var FilePath = httpcontext.Server.MapPath("~/Reports");
                var FileName = FilePath + "\\" + exportName;
                streamoutput.Seek(0, SeekOrigin.Begin);
                var outputfile = new FileStream(FileName, FileMode.CreateNew);
                streamoutput.WriteTo(outputfile);
                streamoutput.Close();
                outputfile.Close();
                outputfile.Dispose();

                //Send email

                var email = new PPI.Platform.Email.SendGridEmail();
                var emailmessage = new System.Net.Mail.MailMessage();

                var Credentials = new NetworkCredential(
                    PPI.Platform.Core.Utility.MachineKeyWrapper.UnprotectUrlSafeString(ConfigurationManager.AppSettings["mailAccount"], "configuration"),
                    PPI.Platform.Core.Utility.MachineKeyWrapper.UnprotectUrlSafeString(ConfigurationManager.AppSettings["mailPassword"], "configuration")
                     );

                emailmessage.To.Add(new MailAddress(ReportRequest.EmailAddress));
                emailmessage.From = new MailAddress(ConfigurationManager.AppSettings["OWINAccountFromAddress"]);
                emailmessage.Subject = "Your HBS Diagnostic Survey Composite Report";
                emailmessage.IsBodyHtml = true;
                emailmessage.Body = "<p>Your HBS Diagnostic Survey Composite Report has been generated successfully!</p>";
                emailmessage.Body += "<p>Please click the \"Requested Report\" link to view and download the report.</p>";
                emailmessage.Body += "<p><a href='" + this.Request.RequestUri.ToString() + "/" + exportName.Remove(exportName.IndexOf(".csv")) + "'>Requested Report</a></p>";

                email.Send(emailmessage, new Core.Email.Model.EmailConfiguration() { Credentials = Credentials });
                //file.Close();
             
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }            
        public HttpResponseMessage Get(string id)
        {
            try
            {

                var httpcontext = this.Request.Properties["MS_HttpContext"] as HttpContextWrapper;
                var FilePath = httpcontext.Server.MapPath("~/Reports");
                FilePath = System.IO.Path.Combine(FilePath, id + ".csv");
                var path = FilePath;
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(path, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = id + ".csv"
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
