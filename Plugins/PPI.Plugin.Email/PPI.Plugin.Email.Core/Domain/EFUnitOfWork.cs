using System;
using System.ComponentModel.Composition;

namespace PPI.Plugin.Email.Core.Domain
{
    using PPI.Plugin.Email.Core.Entities;
    using PPI.Platform.Core.Domain;
    [Export(typeof(IEmailUnitOfWork))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EFUnitOfWork : IEmailUnitOfWork
    {


        private EmailsEntities context = new EmailsEntities();
        
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
