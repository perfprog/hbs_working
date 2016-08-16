using System;
using System.ComponentModel.Composition;

namespace PPI.Plugin.Survey.Core.Domain
{
    using PPI.Plugin.Survey.Core.Entities;
    using PPI.Platform.Core.Domain;
    [Export(typeof(ISurveyUnitOfWork))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EFUnitOfWork : ISurveyUnitOfWork
    {
        //private readonly EfGenericRepository<VersionControl2> _VersionControl2Repository;
        
        private readonly EfGenericRepository<NetworkInfoContact> _NetworkInfoContactRepository;
        private readonly EfGenericRepository<NetworkRelationship> _NetworkRelationshipRepository;
        private readonly EfGenericRepository<NetworkRelationshipDemo> _NetworkRelationshipDemoRepository;
        private readonly EfGenericRepository<NetworkContacts_V> _NetworkContacts_VRepository;        
        private readonly EfGenericRepository<RelationshipDensity> _RelationshipDensityRepository;
        private readonly EfGenericRepository<ParticipantProfile> _ParticipantProfileRepository;
        private readonly EfGenericRepository<vWhoKnowWho> _VWhoKnowWhoRepository;
        private readonly EfGenericRepository<vCompositeReport> _vCompositeReportRepository;
        private SurveyEntities context = new SurveyEntities();
        
        public EFUnitOfWork()
        {                        
            //_VersionControl2Repository = new EfGenericRepository<VersionControl2>(context);                    
            _NetworkInfoContactRepository = new EfGenericRepository<NetworkInfoContact>(context);
            _NetworkRelationshipRepository = new EfGenericRepository<NetworkRelationship>(context);
            _NetworkRelationshipDemoRepository = new EfGenericRepository<NetworkRelationshipDemo>(context);
            _NetworkContacts_VRepository = new EfGenericRepository<NetworkContacts_V>(context);            
            _RelationshipDensityRepository = new EfGenericRepository<RelationshipDensity>(context);
            _ParticipantProfileRepository = new EfGenericRepository<ParticipantProfile>(context);
            _VWhoKnowWhoRepository = new EfGenericRepository<vWhoKnowWho>(context);
            _vCompositeReportRepository = new EfGenericRepository<vCompositeReport>(context);

            context.Database.CommandTimeout = 600;
        }
        
        public void Commit()
        {
            try
            {
                context.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<vWhoKnowWho> IVWhoKnowWhoRepository
        {
            get { return _VWhoKnowWhoRepository; }
        }

        public IGenericRepository<vCompositeReport> IvCompositeReportRepository
        {
            get { return _vCompositeReportRepository; }
        }


        public IGenericRepository<NetworkInfoContact> INetworkInfoContactRepository
        {
            get { return _NetworkInfoContactRepository; }
        }

        public IGenericRepository<NetworkRelationship> INetworkRelationshipRepository
        {
            get { return _NetworkRelationshipRepository; }
        }

        public IGenericRepository<NetworkRelationshipDemo> INetworkRelationshipDemoRepository
        {
            get { return _NetworkRelationshipDemoRepository; }
        }

        public IGenericRepository<NetworkContacts_V> INetworkContacts_VRepository
        {
            get { return _NetworkContacts_VRepository; }
        }
        public IGenericRepository<RelationshipDensity> IRelationshipDensityRepository
        {
            get { return _RelationshipDensityRepository; }
        }

        public IGenericRepository<ParticipantProfile> IParticipantProfileRepository
        {
            get { return _ParticipantProfileRepository; }
        }
    }
}
