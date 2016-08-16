using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Plugin.Survey.Core.Extentions
{
    using PPI.Plugin.Survey.Core.Domain;
    using PPI.Platform.Core.Domain;
    using PPI.Platform.Core.Entities;
    public static class EFGenericRepositoryExtentions
    {
        public static void SurveySetStatus(this IGenericRepository<SurveyGroupParticipant> repostitory, List<int> participantId, int statusId)
        {
            foreach (var item in participantId)
            {
                var surveyStatus = repostitory.AsQueryable().FirstOrDefault(m => m.GroupParticipant.ParticipantId == item && m.SurveyId == 1); // hard coded one survey
                surveyStatus.StatusGenericTypeValueId = statusId;
            }
        }

    }
}

