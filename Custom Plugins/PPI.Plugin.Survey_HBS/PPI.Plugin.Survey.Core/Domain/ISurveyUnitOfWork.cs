using System;

namespace PPI.Plugin.Survey.Core.Domain
{
        using PPI.Platform.Core.Domain;   
        using PPI.Plugin.Survey.Core.Entities;
        public interface ISurveyUnitOfWork : IDisposable
        {
            // IGenericRepository<EventPracticeReport> IEventPracticeReportRepository { get; }                          
             IGenericRepository<NetworkInfoContact> INetworkInfoContactRepository { get; }
             IGenericRepository<NetworkRelationship> INetworkRelationshipRepository { get; }
             IGenericRepository<NetworkRelationshipDemo> INetworkRelationshipDemoRepository { get; }
             IGenericRepository<NetworkContacts_V> INetworkContacts_VRepository { get; }             
             IGenericRepository<RelationshipDensity> IRelationshipDensityRepository { get; }             
             IGenericRepository<ParticipantProfile> IParticipantProfileRepository { get; }
             IGenericRepository<vWhoKnowWho> IVWhoKnowWhoRepository { get; }

             IGenericRepository<vCompositeReport> IvCompositeReportRepository { get; }
                         
            void Commit();
        }
    }
