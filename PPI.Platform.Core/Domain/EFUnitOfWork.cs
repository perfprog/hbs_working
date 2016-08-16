using System;
using System.ComponentModel.Composition;

namespace PPI.Platform.Core.Domain
{
    using PPI.Platform.Core.Attributes;
    using PPI.Platform.Core.Entities;
    [Export(typeof(IPlatformUnitOfWork))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EFUnitOfWork : IPlatformUnitOfWork
    {        
        private readonly EfGenericRepository<RegisteredPlugin> _RegisteredPluginRepository;
        private readonly EfGenericRepository<Culture> _CultureRepository;
        private readonly EfGenericRepository<Dashboard> _DashboardRepository;
        private readonly EfGenericRepository<Program> _ProgramRepository;
        private readonly EfGenericRepository<Participant> _ParticipantRepository;
        private readonly EfGenericRepository<Group> _GroupRepository;
        private readonly EfGenericRepository<GroupParticipant> _GroupParticipantRepository;
        private readonly EfGenericRepository<Person> _PersonRepository;
        private readonly EfGenericRepository<PersonGroupEmail> _PersonGroupEmailRepository;
        private readonly EfGenericRepository<PluginAction> _PluginActionRepository;
        private readonly EfGenericRepository<Email> _EmailRepository;
        private readonly EfGenericRepository<GroupEmail> _GroupEmailRepository;
        private readonly EfGenericRepository<Survey> _SurveyRepository;
        private readonly EfGenericRepository<SurveyGroupParticipant> _SurveyGroupParticipantRepository;
        private readonly EfGenericRepository<GenericType> _GenericTypeRepository;
        private readonly EfGenericRepository<AspNetUser> _AspNetUserRepository;

        private PlatformEntities context = new PlatformEntities();
        public EFUnitOfWork()
        {            
            _RegisteredPluginRepository = new EfGenericRepository<RegisteredPlugin>(context);
            _CultureRepository = new EfGenericRepository<Culture>(context);
            _DashboardRepository = new EfGenericRepository<Dashboard>(context);
            _ProgramRepository = new EfGenericRepository<Program>(context);
            _ParticipantRepository = new EfGenericRepository<Participant>(context);
            _GroupRepository = new EfGenericRepository<Group>(context);
            _GroupParticipantRepository = new EfGenericRepository<GroupParticipant>(context);
            _PersonRepository = new EfGenericRepository<Person>(context);
            _PluginActionRepository = new EfGenericRepository<PluginAction>(context);
            _EmailRepository = new EfGenericRepository<Email>(context);
            _GroupEmailRepository = new EfGenericRepository<GroupEmail>(context);
            _SurveyRepository = new EfGenericRepository<Survey>(context);
            _SurveyGroupParticipantRepository = new EfGenericRepository<SurveyGroupParticipant>(context);
            _GenericTypeRepository = new EfGenericRepository<GenericType>(context);
            _PersonGroupEmailRepository = new EfGenericRepository<PersonGroupEmail>(context);
            _AspNetUserRepository = new EfGenericRepository<AspNetUser>(context);
            
        }                
        public void Commit()
        {
            try
            {
                context.SaveChanges();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<RegisteredPlugin> IRegisteredPluginRepository
        {
            get { return _RegisteredPluginRepository; }
        }


        public IGenericRepository<Culture> ICultureRepository
        {
            get { return _CultureRepository; }
        }

        public IGenericRepository<Dashboard> IDashboardRepository
        {
            get { return _DashboardRepository; }
        }


        public IGenericRepository<Program> IProgramRepository
        {
            get { return _ProgramRepository; }
        }

        public IGenericRepository<Participant> IParticipantRepository
        {
            get { return _ParticipantRepository; }
        }


        public IGenericRepository<Group> IGroupRepository
        {
            get { return _GroupRepository; }
        }


        public IGenericRepository<GroupParticipant> IGroupParticipantRepository
        {
            get { return _GroupParticipantRepository; }
        }


        public IGenericRepository<Person> IPersonRepository
        {
            get { return _PersonRepository; }
        }


        public IGenericRepository<PluginAction> IPluginActionRepository
        {
            get { return _PluginActionRepository; }
        }


        public IGenericRepository<Email> IEmailRepository
        {
            get { return _EmailRepository; }
        }

        public IGenericRepository<GroupEmail> IGroupEmailRepository
        {
            get { return _GroupEmailRepository; }
        }


        public IGenericRepository<Survey> ISurveyRepository
        {
            get { return _SurveyRepository; }
        }


        public IGenericRepository<SurveyGroupParticipant> ISurveyGroupParticipantRepository
        {
            get { return _SurveyGroupParticipantRepository; }
        }


        public IGenericRepository<GenericType> IGenericTypeRepository
        {
            get { return _GenericTypeRepository; }
        }


        public IGenericRepository<PersonGroupEmail> IPersonGroupEmailRepository
        {
            get { return _PersonGroupEmailRepository; }
        }


        public IGenericRepository<AspNetUser> IAspNetUserRepository
        {
            get { return _AspNetUserRepository; }
        }
    }
}

