using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;

namespace JobFinder.Services.Contracts
{
    public interface ICvService
    {
        void CreateCv(CvInputViewModel model);

        bool HasCv();

        CvOutputViewModel GetMyCv();

        UpdateCvViewModel Update(string id);

        void EditedModel(UpdateCvViewModel model, string id);
    }
}
