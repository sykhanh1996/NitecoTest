using System;
using System.Collections.Generic;
using System.Text;

namespace NitecoTest.ViewModels
{
    public class PaginationBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int PageCount
        {
            get
            {
                if (PageSize == 0) PageSize = 1;
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
