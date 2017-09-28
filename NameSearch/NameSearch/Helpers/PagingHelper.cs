using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;

namespace NameSearch.Helpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString AddPageLinks(this HtmlHelper _html, PageInfo _pageInfo, string _function, string _param)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= _pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("onClick", _function + "('" +_param + "'," + i.ToString() + ")");
                tag.InnerHtml = i.ToString();
                if (i == _pageInfo.CurrPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                sb.Append(tag.ToString());
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}