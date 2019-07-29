using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;

namespace JobFinder.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly JobDbContext _context;

        public CompanyService(JobDbContext context)
        {
            _context = context;
        }
        public void CreateCompany(CompanyInputViewModel model, PostJobInputModel postJobModel)
        {
            var companyModel = new CompanyInputViewModel()
            {
                Address = postJobModel.Location,
                Name = postJobModel.Company
            };

            var company = new Company
            {
                Address = companyModel.Address,
                Name = companyModel.Name,
            };

            this._context.Companies.Add(company);
            this._context.SaveChanges();
        }
    }
}
