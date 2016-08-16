using System;
using System.ComponentModel.Composition;

namespace PPI.Plugin.Administration.Core.Domain
{
    using PPI.Plugin.Administration.Core.Entities;
    using PPI.Platform.Core.Domain;
    [Export(typeof(IAdministrationUnitOfWork))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EFUnitOfWork : IAdministrationUnitOfWork
    {
        private readonly EfGenericRepository<VersionControl2> _VersionControl2Repository;

        private AdministrationEntities context = new AdministrationEntities();
        
        public EFUnitOfWork()
        {                        
            _VersionControl2Repository = new EfGenericRepository<VersionControl2>(context);        
        }
        
        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<VersionControl2> IVersionControl2Repository
        {
            get { return _VersionControl2Repository; }
        }
    }
}
