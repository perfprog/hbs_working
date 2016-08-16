using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Plugin.Email.Infrastructure
{
    using PPI.Platform.Core;
    using PPI.Platform.Core.Domain;
    using PPI.Platform.Core.Entities;
    public static class RecordEmail
    {
        public static void Update(MailMessage message,Person participant, int groupEmailId, int status,IPlatformUnitOfWork platformUnitOfWork,string error )
        {
            var Existing = platformUnitOfWork.IPersonGroupEmailRepository.FirstOrDefault(m => m.PersonId == participant.Id && m.GroupEmailId == groupEmailId);

            if (Existing == null)
            {
                var UpdateEmail = new PersonGroupEmail();
                UpdateEmail.Body = message.Body;
                UpdateEmail.GroupEmailId = groupEmailId;
                UpdateEmail.PersonId = participant.Id;
                UpdateEmail.SendStatusGenericTypeValueId = status;
                UpdateEmail.SendDate = DateTime.Now;
                UpdateEmail.OrginalToAddress = message.To.ToString();
                UpdateEmail.ErrorMessage = error;
                platformUnitOfWork.IPersonGroupEmailRepository.Add(UpdateEmail);
            }
            else
            {
                Existing.SendStatusGenericTypeValueId = status;
                Existing.SendDate = DateTime.Now;
                Existing.ErrorMessage = error;
                platformUnitOfWork.IPersonGroupEmailRepository.Update(Existing);
            }

            platformUnitOfWork.Commit();
                        
        }
    }
}
