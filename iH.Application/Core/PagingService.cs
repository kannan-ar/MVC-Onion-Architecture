namespace iH.Application.Core
{
    using System.Collections.Generic;

    public class PagingService
    {
        private readonly int visibleCount;

        public PagingService(int visibleCount)
        {
            this.visibleCount = visibleCount;
        }

        public List<int> GetPaging(int currentPage, int count)
        {
            List<int> paging = new List<int>();

            return paging;
        }
    }
}
