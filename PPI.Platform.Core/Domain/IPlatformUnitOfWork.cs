using System;

namespace PPI.Platform.Core.Domain
{
    using PPI.Platform.Core.Entities;
    public interface IPlatformUnitOfWork : IDisposable
    {
       // IGenericRepository<EventPracticeReport> IEventPracticeReportRepository { get; }         
        IGenericRepository<RegisteredPlugin> IRegisteredPluginRepository { get; }
        IGenericRepository<Culture> ICultureRepository { get; }
        IGenericRepository<Dashboard> IDashboardRepository { get; }
        IGenericRepository<Program> IProgramRepository { get; }
        IGenericRepository<Participant> IParticipantRepository { get; }
        IGenericRepository<Group> IGroupRepository { get; }
        IGenericRepository<GroupParticipant> IGroupParticipantRepository { get; }
        IGenericRepository<Person> IPersonRepository { get; }
        IGenericRepository<PersonGroupEmail> IPersonGroupEmailRepository { get; }
        IGenericRepository<PluginAction> IPluginActionRepository { get; }
        IGenericRepository<Email> IEmailRepository { get; }
        IGenericRepository<GroupEmail> IGroupEmailRepository { get; }
        IGenericRepository<Survey> ISurveyRepository { get; }
        IGenericRepository<SurveyGroupParticipant> ISurveyGroupParticipantRepository { get; }
        IGenericRepository<GenericType> IGenericTypeRepository { get; }

        IGenericRepository<AspNetUser> IAspNetUserRepository { get; }
        void Commit();
    }
}

