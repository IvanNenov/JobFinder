using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JobFinder.Tests.Service
{
    public class CompanyServiceTests
    {
        private readonly ICompanyService _companyService;
        private readonly JobDbContext _context;

        public CompanyServiceTests()
        {
            var options = new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new JobDbContext(options);

            this._companyService = new CompanyService(_context);
        }

        [Fact]
        public void TestCreateCompany_ShouldCreateCorrectCompany()
        {
            var companyModel = new CompanyInputViewModel()
            {
                Address = "NYC",
                Name = "FB"
            };

            var jobModel = new PostJobInputModel()
            {
                Description = "job",
                Company = "FB",
                Location = "NYC",
                JobTitle = "job",
                JobType = JobType.FullTime
            };

            this._companyService.CreateCompany(companyModel,jobModel);
            this._context.SaveChanges();

            var expected = this._context.Companies.FirstOrDefault(x => x.Name == "FB");

            var actualMappedCompany = new Company()
            {
                Address = companyModel.Address,
                Name = companyModel.Name
            };

            Assert.Equal(expected.Address,actualMappedCompany.Address);
            Assert.Equal(expected.Name, actualMappedCompany.Name);

        }
    }
}
