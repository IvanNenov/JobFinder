using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;

namespace JobFinder.Services.Contracts
{
    public interface IFormEntryService
    {
        void CreateFormEntry(FormEntryInputViewModel model);

        IEnumerable<FormEntryOutputViewModel> GetAll();

        void DeleteFormEntry(string id);
    }
}
