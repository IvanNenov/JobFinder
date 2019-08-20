using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JobFinder.Tests.Service
{
    public class CvServiceTests
    {
        private readonly ICvService _cvService;
        private readonly JobDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        public CvServiceTests()
        {
            var options = new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new JobDbContext(options);
            _accessor = new HttpContextAccessor();
            this._cvService = new CvService(_context, _accessor);
        }


        [Fact]
        public void TestUpdate_ShouldReturnCorrectCv()
        {
            var cvId = Guid.NewGuid().ToString();

            var cv = new Cv()
            {
                Description = "a",
                Id = cvId,
                ImageUrl = "aa"
            };

            this._context.Cvs.Add(cv);
            this._context.SaveChanges();

            var expected = this._context.Cvs.Find(cvId);

            Assert.Equal(expected, cv);

            var model = this._cvService.Update(cvId);

            var mappedModel = new Cv()
            {
                ImageUrl = model.ImageUrl,
                Description = model.Description
            };

            this._context.SaveChanges();

            Assert.Equal(mappedModel.Description, cv.Description);
            Assert.Equal(mappedModel.ImageUrl, cv.ImageUrl);
        }

        [Fact]
        public void TestEditModel_ShouldEditCorrectCv()
        {
            var cvId = Guid.NewGuid().ToString();

            var cv = new Cv()
            {
                Description = "a",
                Id = cvId,
                ImageUrl = "aa"
            };

            this._context.Cvs.Add(cv);
            this._context.SaveChanges();

            var expected = this._context.Cvs.Find(cvId);

            Assert.Equal(expected, cv);

            var update = new UpdateCvViewModel()
            {
                Description = "da",
                ImageUrl = "a",
            };

            this._cvService.EditedModel(update, cvId);
            this._context.SaveChanges();

            var mappedModel = new Cv()
            {
                Description = update.Description,
                ImageUrl = update.ImageUrl,
            };

            var editedCv = this._context.Cvs.Find(cvId);

            Assert.Equal(mappedModel.Description, editedCv.Description);
            Assert.Equal(mappedModel.ImageUrl, editedCv.ImageUrl);
        }
    }
}
