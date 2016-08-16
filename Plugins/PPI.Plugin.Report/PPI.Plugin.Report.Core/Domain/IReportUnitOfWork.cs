using System;

namespace PPI.Plugin.Report.Core.Domain
{
        using PPI.Platform.Core.Domain;
        using PPI.Plugin.Report.Core.Entities;
    public interface IReportUnitOfWork : IDisposable
        {
            // IGenericRepository<EventPracticeReport> IEventPracticeReportRepository { get; } 
            
            void Commit();
        }
    }
