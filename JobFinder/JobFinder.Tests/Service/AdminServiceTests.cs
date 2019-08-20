using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.OutputViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JobFinder.Tests.Service
{
    public class AdminServiceTests
    {
        private readonly IAdminService _adminService;
        private readonly JobDbContext _context;

        public AdminServiceTests()
        {
            var options = new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new JobDbContext(options);

            this._adminService = new AdminService(_context);
        }

        [Fact]
        public void TestDelete_ShouldDeleteCorrectJobAd()
        {
            var jobAdId = Guid.NewGuid().ToString();
            var job = new JobAdd
            {
                Id = jobAdId,
                Description = "job",
                JobTitle = ".net"
            };

            this._context.JobAdds.Add(job);
            this._context.SaveChanges();

            var expected = this._context.JobAdds.Find(jobAdId);

            Assert.Equal(expected, job);

            this._adminService.Delete(jobAdId);
            this._context.SaveChanges();

            var expectedNull = this._context.JobAdds.FirstOrDefault(x => x.Id == jobAdId);
            Assert.Null(expectedNull);
        }

        [Fact]
        public void TestUpdate_ShouldReturnCorrectJobAd()
        {
            var jobAdId = Guid.NewGuid().ToString();
            var job = new JobAdd
            {
                Id = jobAdId,
                Description = "job",
                JobTitle = ".net",
                JobType = JobType.FullTime,
                Location = "NYC"
            };

            this._context.JobAdds.Add(job);
            this._context.SaveChanges();

            var expected = this._context.JobAdds.Find(jobAdId);

            Assert.Equal(expected, job);

            var model = this._adminService.Update(jobAdId);
            var mappedModel = new JobAdd()
            {
                JobTitle = model.JobTitle,
                Description = model.Description,
                JobType = model.JobType,
                Location = model.Location
            };
            this._context.SaveChanges();

            Assert.Equal(mappedModel.JobTitle, job.JobTitle);
            Assert.Equal(mappedModel.Description, job.Description);
            Assert.Equal(mappedModel.JobType, job.JobType);
            Assert.Equal(mappedModel.Location, job.Location);
        }

        [Fact]
        public void TestEditModel_ShouldEditCorrectJobAd()
        {
            var jobAdId = Guid.NewGuid().ToString();
            var job = new JobAdd
            {
                Id = jobAdId,
                Description = "job",
                JobTitle = ".net",
                JobType = JobType.FullTime,
                Location = "NYC"
            };

            this._context.JobAdds.Add(job);
            this._context.SaveChanges();

            var expected = this._context.JobAdds.Find(jobAdId);

            Assert.Equal(expected, job);

            var update = new UpdateViewModel
            {
                Description = "da",
                JobTitle = "da",
                JobType = JobType.FullTime,
                Location = "da"
            };

            this._adminService.EditedModel(update,jobAdId);
            this._context.SaveChanges();

            var mappedModel = new JobAdd()
            {
                JobTitle = update.JobTitle,
                Description = update.Description,
                JobType = update.JobType,
                Location = update.Location
            };
            var editedJob = this._context.JobAdds.Find(jobAdId);

            Assert.Equal(mappedModel.JobTitle, editedJob.JobTitle);
            Assert.Equal(mappedModel.Description, editedJob.Description);
            Assert.Equal(mappedModel.JobType, editedJob.JobType);
            Assert.Equal(mappedModel.Location, editedJob.Location);
        }

    }
}
