using System;
using System.ComponentModel.Composition;

namespace PPI.Platform.Plugin.Default.Domain
{
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Core.Domain;
    [Export(typeof(IPlatformUnitOfWork))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EFUnitOfWork : IPlatformUnitOfWork
    {
        private readonly EfGenericRepository<VersionControl> _VersionControlRepository;

        private PlatformEntities context = new PlatformEntities();
        public EFUnitOfWork()
        {
            _VersionControlRepository = new EfGenericRepository<VersionControl>(context);        
        }
        
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            context.SaveChanges();
        }

        public IGenericRepository<VersionControl> IVersionControlRepository
        {
            get { return _VersionControlRepository; }
        }
    }
}
