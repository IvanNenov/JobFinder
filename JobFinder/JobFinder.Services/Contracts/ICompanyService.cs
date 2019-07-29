using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.ViewModels.InputViewModels;

namespace JobFinder.Services.Contracts
{
    public interface ICompanyService
    {
        void CreateCompany(CompanyInputViewModel model, PostJobInputModel postJobModel);
    }
}
