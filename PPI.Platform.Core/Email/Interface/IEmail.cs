using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Platform.Core.Email.Interface
{
    using PPI.Platform.Core.Email.Model;
    public interface IEmail
    {        
        void Send(MailMessage message, EmailConfiguration configuration);
        Task SendAsync(MailMessage message, EmailConfiguration configuration);
        bool HasErrors { get; set; }
        string ErrorMessage { get; set; }
    }    
}
