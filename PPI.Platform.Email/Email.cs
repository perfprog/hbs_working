using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.Composition;
using System.Net.Mail;
using System.Net;
namespace PPI.Platform.Email
{
    using PPI.Platform.Core.Email.Interface;    
    using PPI.Platform.Core.Email.Model;
    using PPI.Platform.Core.Attributes;

    [Logging(AttributeExclude = true)]      
    [Export(typeof(IEmail))]
    [PartCreationPolicy(CreationPolicy.Shared)]    
    public class SendGridEmail : IEmail
    {
        
        public SendGridEmail()
        {
        
        }
        private SendGrid.SendGridMessage GridMessage { get; set; }
        
        public void Send(MailMessage message, EmailConfiguration configuration)
        {
            ConvertToSendGridMessage(message);
            var Sender = TransportWeb(configuration);
            if (Sender != null)
            {
                try
                {
                    Sender.Deliver(GridMessage);
                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    ErrorMessage = ExposeErrors(ex);
                }
            }
        }

        
        public Task SendAsync(MailMessage message, EmailConfiguration configuration)
        {
            ConvertToSendGridMessage(message);
            var Sender = TransportWeb(configuration);
            if (Sender != null)
            {
                try                
                {
                    
                    return Sender.DeliverAsync(GridMessage);
                }
                catch (Exception ex)
                {
                    HasErrors = true;
                    ErrorMessage = ExposeErrors(ex);
                    return Task.FromResult(0);
                }
            }
            return Task.FromResult(0);
        }
        
        private void ConvertToSendGridMessage(MailMessage message)
        {
            GridMessage = new SendGrid.SendGridMessage();
            var ToMailAddresses = new List<string>();
            var BccMailAddress = new List<string>();
            var CcMailAddress = new List<string>();
            foreach (var item in message.To)
	        {
                ToMailAddresses.Add(item.Address);                
	        }
            foreach (var item in message.Bcc)
            {
                GridMessage.AddBcc(item);
            }

            foreach (var item in message.CC)
            {
                GridMessage.AddCc(item);
            }

            foreach (var item in message.Attachments)
            {
                GridMessage.AddAttachment(item.ContentStream, item.Name);
            }

            GridMessage.AddTo(ToMailAddresses);
            
            if (message.IsBodyHtml)
                GridMessage.Html = message.Body;
            else
                GridMessage.Text = message.Body;
            GridMessage.Subject = String.IsNullOrEmpty(message.Subject) ? "System Message" : message.Subject;
            GridMessage.From = message.From;
            GridMessage.SetCategory("hbs-prod");
        }
        
        private SendGrid.Web TransportWeb(EmailConfiguration configuration)
        {
            return new SendGrid.Web(configuration.Credentials);
        }
        public bool HasErrors { get; set; }
        public string ErrorMessage { get; set; }
        
        private string ExposeErrors(Exception exception)
        {
            string error = "";
            if (exception.GetType() == typeof(Exceptions.InvalidApiRequestException))
            {
                HttpStatusCode code = ((Exceptions.InvalidApiRequestException)exception).ResponseStatusCode;
                string strCode = ((byte)code).ToString();
                error += "\nSTATUS CODE: " + strCode + " " + code.ToString();
                error += "\n\nERRORS: ";
                foreach (string err in ((Exceptions.InvalidApiRequestException)exception).Errors)
                {
                    error += "\n" + err;
                }
            }
            else
            {
                error += "\n" + exception.GetBaseException().Message;
            }
            return error;
        }
        
    }
}
