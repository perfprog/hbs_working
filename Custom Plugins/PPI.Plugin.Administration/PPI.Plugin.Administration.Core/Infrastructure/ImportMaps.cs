using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPI.Plugin.Administration.Core.Infrastructure
{
    using PPI.Platform.Core.Entities;
    using CsvHelper;
    using CsvHelper.Configuration;
    
    public class ImportMaps
    {
        public sealed class Participant_Map : CsvClassMap<Person>
        {
            public override void CreateMap()
            {
                Map(m => m.First_Name).Index(0);
                Map(m => m.Last_Name).Index(1);
                Map(m => m.Email).Index(2);
            }
        }
    }
}
