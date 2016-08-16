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

namespace PPI.Platform.Mvc.HtmlHelpers
{
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    using PPI.Platform.Mvc.Properties;
    
    public static class HtmlHelpers
    {
        public static MvcHtmlString GetResourceImageFor(this HtmlHelper helper, Bitmap bitmap,string type, Object htmlAttributes = null)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);
            string base64 = Convert.ToBase64String(stream.ToArray());
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
            TagBuilder tag = new TagBuilder("img");
            tag.MergeAttribute("src", string.Format("data:" + type + ";base64,{0}", base64));                                    
            if (htmlAttributes != null)
                tag.MergeAttributes(attributes);

            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString CheckBoxListFor(this HtmlHelper html, string name, List<CheckBoxModel> checkBoxItems)
        {            
            StringBuilder result = new StringBuilder();
            if (checkBoxItems != null)
            {
                for (var i = 0; i < checkBoxItems.Count(); i++)
                {
                    var checkbox = CheckBoxBuildHtmlItem(checkBoxItems.ElementAt(i).Selected, name, i, checkBoxItems.ElementAt(i).Value, checkBoxItems.ElementAt(i).Name);
                    result.Append(checkbox);
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
      
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {            
            StringBuilder result = new StringBuilder();
            if (pagingInfo.LastPage == 0)
            {
                var calcPage = (pagingInfo.TotalRecords - 1) / pagingInfo.PageSize + 1;
                if (calcPage <= 0)
                    calcPage = 1;
                pagingInfo.LastPage = calcPage;
            }
            //First Page
            string firstPage = PagingBuildHtmlItem(pageUrl(pagingInfo.FirstPage), Resources_Mvc.View_PagingFirst, false);
            result.Append(firstPage);
            //previous link
            string prevLink = (pagingInfo.CurrentPage == 1) ? PagingBuildHtmlItem(pageUrl(pagingInfo.CurrentPage), Resources_Mvc.View_PagingPrev, false, true) : PagingBuildHtmlItem(pageUrl(pagingInfo.CurrentPage - 1), Resources_Mvc.View_PagingPrev);
            result.Append(prevLink);
            // only show up to 5 links to the left of the current page

            //var start = (pagingInfo.CurrentPage < pagingInfo.NavSize) ? pagingInfo.NavSize - (pagingInfo.NavSize - pagingInfo.CurrentPage) : (pagingInfo.CurrentPage);
            int start;
            int end;

            if (pagingInfo.CurrentPage + 2 > pagingInfo.NavSize)
            {
                start = (pagingInfo.CurrentPage + 2 > pagingInfo.LastPage) 
                    ? pagingInfo.CurrentPage - pagingInfo.NavSize + 1 + (pagingInfo.LastPage - pagingInfo.CurrentPage) 
                    : pagingInfo.CurrentPage - 2;
                end = (pagingInfo.CurrentPage + 2 > pagingInfo.LastPage )
                    ? pagingInfo.LastPage
                    : pagingInfo.CurrentPage + 2;
            }
            else
            {
                start = 1;
                end = pagingInfo.NavSize;
            }
            
          

            for (int i = start; i <= end; i++)
            {
                string pageHtml; 
                pageHtml = (i == pagingInfo.CurrentPage) 
                    ? PagingBuildHtmlItem(pageUrl(i), i.ToString(), true, true)
                    : (i > ((pagingInfo.TotalRecords - 1) / pagingInfo.PageSize + 1))
                        ? PagingBuildHtmlItem(pageUrl(i), i.ToString(),false,true)
                        : PagingBuildHtmlItem(pageUrl(i), i.ToString());
                result.Append(pageHtml);
            }
            // next link
            string nextLink = (pagingInfo.CurrentPage == end || (pagingInfo.CurrentPage + 1) * pagingInfo.PageSize >= pagingInfo.TotalRecords)
                ? PagingBuildHtmlItem(pageUrl(pagingInfo.CurrentPage + 1), Resources_Mvc.View_PagingNext, false, true)
                : (pagingInfo.CurrentPage == pagingInfo.LastPage)
                    ? PagingBuildHtmlItem(pageUrl(pagingInfo.CurrentPage + 1), Resources_Mvc.View_PagingNext, false, true)
                    : PagingBuildHtmlItem(pageUrl(pagingInfo.CurrentPage + 1), Resources_Mvc.View_PagingNext);
            result.Append(nextLink);
            // LastPage
            string lastPage = PagingBuildHtmlItem(pageUrl(pagingInfo.LastPage), Resources_Mvc.View_PagingLast, false);
            result.Append(lastPage);
            return MvcHtmlString.Create(result.ToString());
        }
        
        public static MvcHtmlString PageSizeList(this HtmlHelper html, List<string> sizeList,string currentSize, Func<int, string> sizeUrl)
        {
            StringBuilder result = new StringBuilder();
            var CurrentPageSize = currentSize;
            foreach (var item in sizeList)
            {                                                    
                var Display = item.ToString();
                int curItem;
                int.TryParse(item, out curItem);
                curItem = (curItem == 0) ? 10000 : curItem;
                Boolean isCurrent = (CurrentPageSize == curItem.ToString());
                result.Append(PageSizeBuildHtmlItem(sizeUrl(curItem), Display, isCurrent, isCurrent));

            }

            return MvcHtmlString.Create(result.ToString());
        }
        /// <summary>
        /// Turns a valid Json string object into valid routeValues for Url.Action
        /// </summary>
        /// <param name="html"></param>
        /// <param name="action"></param>
        /// <param name="controler"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static MvcHtmlString ActionJsonRoute(this UrlHelper html, string action, string controler, string routeValues)
        {
            var jsonObject = System.Web.Helpers.Json.Decode(routeValues);
            var RouteValues = new RouteValueDictionary(jsonObject);
            foreach (var item in jsonObject)
            {
                RouteValues.Add(item.Key, item.Value);
            }
            return MvcHtmlString.Create(html.Action(action, controler, RouteValues));
        }

        private static string CheckBoxBuildHtmlItem(bool selected, string postModelName, int id, string value, string name)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttribute("class", "checkbox-nice");
            TagBuilder tagInput = new TagBuilder("input");
            tagInput.MergeAttribute("name", postModelName);
            tagInput.MergeAttribute("id", postModelName + id.ToString());
            tagInput.MergeAttribute("value", value);
            tagInput.MergeAttribute("type", "checkbox");
            if (selected)
                tagInput.MergeAttribute("checked", string.Empty);
            TagBuilder tagLabel = new TagBuilder("label");
            tagLabel.MergeAttribute("for", postModelName + id.ToString());
            tagLabel.SetInnerText(name);
            tagInput.InnerHtml = tagLabel.ToString();
            tagBuilder.InnerHtml = tagInput.ToString();
            return tagBuilder.ToString();
        }

        private static string PagingBuildHtmlItem(string url, string display, bool active = false, bool disabled = false)
        {        // every item wrapped in LI tag 
            TagBuilder liTag = new TagBuilder("li");
            if (disabled && !active)
            {
                // add disabled class and use a SPAN tag inside
                liTag.MergeAttribute("class", "disabled");
                var spanTag = new TagBuilder("span");
                spanTag.InnerHtml = display;
                liTag.InnerHtml = spanTag.ToString();
            }
            else
            {
                if (active)
                {
                    liTag.MergeAttribute("class", "active");
                }
                // use inner A tag
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", url);
                tag.InnerHtml = display;
                liTag.InnerHtml = tag.ToString();
            }
            return liTag.ToString();
        }
        private static string PageSizeBuildHtmlItem(string url, string display, bool active = false, bool disabled = false)
        {        // every item wrapped in LI tag 
            TagBuilder liTag = new TagBuilder("li");
            if (disabled && !active)
            {
                // add disabled class and use a SPAN tag inside
                liTag.MergeAttribute("class", "disabled");
                var spanTag = new TagBuilder("span");
                spanTag.InnerHtml = display;
                liTag.InnerHtml = spanTag.ToString();
            }
            else
            {
                if (active)
                {
                    liTag.MergeAttribute("class", "list-group-item-info");
                    var spanTag = new TagBuilder("span");
                    spanTag.InnerHtml = display;
                    liTag.InnerHtml = spanTag.ToString();
                }
                else
                {
                    // use inner A tag
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", url);
                    tag.InnerHtml = display;
                    liTag.InnerHtml = tag.ToString();
                }                
            }
            return liTag.ToString();
        }
    }
}
