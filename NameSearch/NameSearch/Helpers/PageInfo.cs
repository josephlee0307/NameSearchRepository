using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NameSearch.Helpers
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrPage { get; set; }
        public int TotalPages
        {
            get { return ItemsPerPage == 0 ? 0 : (int) Math.Ceiling((decimal) TotalItems / ItemsPerPage); }
        }
    }
}