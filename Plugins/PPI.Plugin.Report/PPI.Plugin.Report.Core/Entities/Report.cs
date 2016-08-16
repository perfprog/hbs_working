namespace PPI.Plugin.Report.Core.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ReportEntities : DbContext
    {
        public ReportEntities()
            : base("name=ReportEntities")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
