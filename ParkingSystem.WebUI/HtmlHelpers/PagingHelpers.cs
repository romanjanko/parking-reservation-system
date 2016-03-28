using System;
using System.Text;
using System.Web.Mvc;
using ParkingSystem.Core.Pagination;

namespace ParkingSystem.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
                                              PagingInfo pagingInfo,
                                              Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            // previous link
            TagBuilder tagA = new TagBuilder("a");
            int previousPage = pagingInfo.CurrentPage - 1;

            if (previousPage < 1)
                previousPage = 1;

            tagA.MergeAttribute("href", pageUrl(previousPage));
            tagA.MergeAttribute("aria-label", "Previous");
            tagA.InnerHtml = "<span>&laquo;</span>";

            TagBuilder tagLi = new TagBuilder("li");
            tagLi.InnerHtml = tagA.ToString();
            result.Append(tagLi.ToString());
            
            // page links
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                tagA = new TagBuilder("a");
                tagA.MergeAttribute("href", pageUrl(i));
                tagA.InnerHtml = i.ToString();

                tagLi = new TagBuilder("li");
                tagLi.InnerHtml = tagA.ToString();
                if (i == pagingInfo.CurrentPage)
                    tagLi.AddCssClass("active");
                result.Append(tagLi.ToString());
            }

            // next link
            tagA = new TagBuilder("a");
            int nextPage = pagingInfo.CurrentPage + 1;

            if (nextPage > pagingInfo.TotalPages)
                nextPage = pagingInfo.TotalPages;

            tagA.MergeAttribute("href", pageUrl(nextPage));
            tagA.MergeAttribute("aria-label", "Next");
            tagA.InnerHtml = "<span>&raquo;</span>";

            tagLi = new TagBuilder("li");
            tagLi.InnerHtml = tagA.ToString();
            result.Append(tagLi.ToString());

            return MvcHtmlString.Create(result.ToString());
        }
    }
}