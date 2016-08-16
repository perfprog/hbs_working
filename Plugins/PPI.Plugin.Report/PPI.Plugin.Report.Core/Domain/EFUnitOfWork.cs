using System;
using System.ComponentModel.Composition;

namespace PPI.Plugin.Report.Core.Domain
{
    using PPI.Plugin.Report.Core.Entities;
    using PPI.Platform.Core.Domain;
    [Export(typeof(IReportUnitOfWork))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EFUnitOfWork : IReportUnitOfWork
    {


        private ReportEntities context = new ReportEntities();
        
        public EFUnitOfWork()
        {                        
            
        }
        
        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

       
    }
}
