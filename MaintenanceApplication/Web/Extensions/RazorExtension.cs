using Maintenance.Application.Dto_s.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

public static class RazorExtensions
{
    public static IHtmlContent PagedResultPager<T>(this IHtmlHelper helper, PagedResult<T> pagedResult, Func<int, string> generatePageUrl)
    {
        var ulClasses = "pagination justify-content-center";
        var liClasses = "page-item paginate_button";
        var linkClasses = "page-link";

        var ul = new TagBuilder("ul");
        ul.AddCssClass(ulClasses);

        // Previous Button
        var previousLi = new TagBuilder("li");
        previousLi.AddCssClass(liClasses);
        if (pagedResult.PageNumber > 1)
        {
            var previousAnchor = new TagBuilder("a");
            previousAnchor.Attributes.Add("href", generatePageUrl(pagedResult.PageNumber - 1));
            previousAnchor.InnerHtml.Append("Previous");
            previousAnchor.AddCssClass(linkClasses);
            previousLi.InnerHtml.AppendHtml(previousAnchor);
        }
        else
        {
            previousLi.AddCssClass("disabled");
            var previousSpan = new TagBuilder("span");
            previousSpan.InnerHtml.Append("Previous");
            previousSpan.AddCssClass(linkClasses);
            previousLi.InnerHtml.AppendHtml(previousSpan);
        }
        ul.InnerHtml.AppendHtml(previousLi);

        // Page Numbers
        for (int i = 1; i <= pagedResult.PageCount; i++)
        {
            var pageLi = new TagBuilder("li");
            pageLi.AddCssClass(liClasses);
            if (i == pagedResult.PageNumber)
            {
                pageLi.AddCssClass("active");
                var span = new TagBuilder("span");
                span.InnerHtml.Append(i.ToString());
                span.AddCssClass(linkClasses);
                pageLi.InnerHtml.AppendHtml(span);
            }
            else
            {
                var anchor = new TagBuilder("a");
                anchor.Attributes.Add("href", generatePageUrl(i));
                anchor.InnerHtml.Append(i.ToString());
                anchor.AddCssClass(linkClasses);
                pageLi.InnerHtml.AppendHtml(anchor);
            }
            ul.InnerHtml.AppendHtml(pageLi);
        }

        // Next Button
        var nextLi = new TagBuilder("li");
        nextLi.AddCssClass(liClasses);
        if (pagedResult.PageNumber < pagedResult.PageCount)
        {
            var nextAnchor = new TagBuilder("a");
            nextAnchor.Attributes.Add("href", generatePageUrl(pagedResult.PageNumber + 1));
            nextAnchor.InnerHtml.Append("Next");
            nextAnchor.AddCssClass(linkClasses);
            nextLi.InnerHtml.AppendHtml(nextAnchor);
        }
        else
        {
            nextLi.AddCssClass("disabled");
            var nextSpan = new TagBuilder("span");
            nextSpan.InnerHtml.Append("Next");
            nextSpan.AddCssClass(linkClasses);
            nextLi.InnerHtml.AppendHtml(nextSpan);
        }
        ul.InnerHtml.AppendHtml(nextLi);

        return ul;
    }
}
