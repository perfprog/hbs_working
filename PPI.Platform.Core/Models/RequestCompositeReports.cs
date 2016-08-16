using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Platform.Core.Models
{
    public class RequestCompositeReports
    {
        public string[] ParticipantIds { get; set; }
        public string ReportActionRoot { get; set; }

        public string EmailAddress { get; set; }

    }
}
