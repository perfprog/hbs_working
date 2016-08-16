using System;

namespace PPI.Plugin.Email.Core.Domain
{
        using PPI.Platform.Core.Domain;   
        using PPI.Plugin.Email.Core.Entities;
        public interface IEmailUnitOfWork : IDisposable
        {
            // IGenericRepository<EventPracticeReport> IEventPracticeReportRepository { get; } 
            
            void Commit();
        }
    }
