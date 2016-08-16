using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PPI.Platform.Mvc.Extentions
{
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Core.Domain;
    using System.Linq.Expressions;
    public static class EFGenericRepositoryExtentions
    {
        public static SelectList GetGenericTypes(this IGenericRepository<GenericType> repostitory, int culture, string tableField, int? selectedValue)
        {
            var GenericList = repostitory.AsQueryable().Where(m => m.Name == tableField)
                .SelectMany(t => t.GenericTypeValues)
                .Select(n => new { Value = n.Id, Text = n.Resource.ResourceValues.FirstOrDefault(s => s.CultureId == culture).Value });
            var list = new SelectList(GenericList, "Value", "Text", selectedValue);
            return list;
        }        
    }
}
