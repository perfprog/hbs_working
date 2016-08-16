using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Web.Handlers;
using System.Web.Routing;

namespace PPI.Plugin.Survey.HtmlHelpers
{
    using PPI.Platform.Core.Entities;
    using System.Linq.Expressions;
    public static class SurveyHelpers
    {
        public static MvcHtmlString SurveyStatus<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> fieldExpression, string label)
        {            
            ModelMetadata fieldmetadata = ModelMetadata.FromLambdaExpression(fieldExpression, htmlHelper.ViewData);            
            var fieldName = ExpressionHelper.GetExpressionText(fieldExpression);            
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var value = fieldmetadata.Model.ToString();
            int StatusValue;

            if (!int.TryParse(value, out StatusValue))
            {
                throw new FormatException();
            }
            TagBuilder tag = new TagBuilder("span");
            string classvalue = "";
            switch (StatusValue)
            {
                case 1009: classvalue = "label label-success status";
                    break;
                case 1010: classvalue = "label label-danger status";
                    break;
                case 1011: classvalue = "label label-info status";
                    break;
                case 1012: classvalue = "label label-warning status";
                    break;
                default: classvalue = "label label-success status";
                    break;
            }
            tag.MergeAttribute("class", classvalue);
            tag.SetInnerText(label);

            return new MvcHtmlString(tag.ToString());
        }      
    }
}