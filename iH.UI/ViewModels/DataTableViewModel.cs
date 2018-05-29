namespace iH.UI.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class DataTableViewModel
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<List<string>> data;

        public static DataTableViewModel GetViewModel(Func<List<List<string>>> getData)
        {
            DataTableViewModel vm = new DataTableViewModel();

            vm.data = getData();
            vm.recordsTotal = vm.recordsFiltered = vm.data.Count;

            return vm;
        }
    }
}