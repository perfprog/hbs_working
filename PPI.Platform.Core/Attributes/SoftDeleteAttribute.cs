using System;
using System.Linq;
using System.Data.Entity.Core.Metadata.Edm;


namespace PPI.Platform.Core.Attributes
{
    public class SoftDeleteAttribute : Attribute
    {
        public SoftDeleteAttribute(string column)
        {
            ColumnName = column;
        }
        public string ColumnName {get; set; }
        public static string GetSoftDeleteColumnName(EdmType type)
        {
            MetadataProperty annotation = type.MetadataProperties
                .Where(m => m.Name.EndsWith("customannotation:SoftDeleteColumnName"))
                .SingleOrDefault();
            return annotation == null ? null : (string)annotation.Value;
        }
    }
}
