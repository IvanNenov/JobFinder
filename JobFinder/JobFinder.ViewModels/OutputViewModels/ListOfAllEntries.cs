using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.ViewModels.OutputViewModels
{
   public class ListOfAllEntries
    {
        public IEnumerable<FormEntryOutputViewModel> FormEntryOutput { get; set; }

        public double TotalPagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

    }
}
