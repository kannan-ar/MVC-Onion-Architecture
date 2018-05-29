namespace iH.ViewModels
{
    using System.Collections.Generic;

    public class PagingViewModel
    {
        public bool HasPrevious { get; set; }
        public int Previous { get; set; }
        public bool HasNext { get; set; }
        public int Next { get; set; }
        public List<int> Paging { get; set; }
    }
}