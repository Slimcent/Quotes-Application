using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuotes.Models
{
    public class Pager
    {
        public int TotalItems { get; private set; }  // Total number of records
        public int CurrentPage { get; private set; }  // Active page
        public int PageSize { get; private set; }  // Number of records to be displaye in a page

        public int TotalPages { get; private set; }  // Gives the total number of pages
        public int StartPage { get; private set; }  // First page
        public int EndPage { get; private set; }  //Last page

        //Add values to page
        public Pager()
        {

        }

        public Pager(int totalItems, int page, int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}
