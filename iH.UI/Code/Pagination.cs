namespace iH.UI.Code
{
    using System.Collections.Generic;

    using iH.ViewModels;

    public static class Pagination
    {
        public static PagingViewModel GetPaging(int currentPage, int noRecords, int totalRecords, int displayRecords)
        {
            PagingViewModel model = new PagingViewModel();
            model.Paging = new List<int>();

            int pageStart = currentPage - ((int)(displayRecords / 2));
            int availPages = (int)decimal.Round(totalRecords / noRecords);

            pageStart = pageStart < 1 ? 1 : pageStart;
            int orginalPageStart = pageStart;

            if (currentPage > 1)
            {
                model.HasPrevious = true;
                model.Previous = currentPage - 1;
            }

            if (currentPage < availPages)
            {
                model.HasNext = true;
                model.Next = currentPage + 1;
            }

            model.Paging.Add(pageStart);

            for (int i = 1; displayRecords > i && (orginalPageStart + i) <= availPages; ++i)
            {
                model.Paging.Add(++pageStart);
            }

            return model;
        }
    }
}