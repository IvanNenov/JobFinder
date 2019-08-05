using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;

namespace JobFinder.Services.Contracts
{
    public interface IAdminService
    {
        UpdateViewModel Update(string id);
        void Delete(string jobAddId);

        void EditedModel(UpdateViewModel model, string id);

    }
}
