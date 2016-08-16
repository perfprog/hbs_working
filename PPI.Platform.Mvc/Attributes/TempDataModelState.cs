using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PPI.Platform.Mvc.Attributes
{     
 
        public class SetTempDataModelStateAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                base.OnActionExecuted(filterContext);
                filterContext.Controller.TempData["ModelState"] =
                   filterContext.Controller.ViewData.ModelState;
            }
        }
        public class RestoreModelStateFromTempDataAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                base.OnActionExecuting(filterContext);
                if (filterContext.Controller.TempData.ContainsKey("ModelState"))
                {
                    filterContext.Controller.ViewData.ModelState.Merge(
                        (ModelStateDictionary)filterContext.Controller.TempData["ModelState"]);
                }
            }
        }
 
}
