using System;

namespace PPI.Plugin.Administration.Core.Domain
{
        using PPI.Platform.Core.Domain;   
        using PPI.Plugin.Administration.Core.Entities;
        public interface IAdministrationUnitOfWork : IDisposable
        {
            // IGenericRepository<EventPracticeReport> IEventPracticeReportRepository { get; } 
            IGenericRepository<VersionControl2> IVersionControl2Repository { get; }
            void Commit();
        }
    }
