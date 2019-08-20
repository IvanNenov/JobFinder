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
        //bool HasCv();

        //CvOutputViewModel GetMyCv();

        //UpdateCvViewModel Update(string id);

        //void EditedModel(UpdateCvViewModel model, string id);

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


        //[Fact]
        //public void TestCreateCv_ShouldCreateCv()
        //{
        //    var user = new User
        //    {
        //        UserName = "ivan",
        //        Email = "ivan@abv.bg",
        //    };

        //    this._context.Users.Add(user);
        //    this._context.SaveChanges();

        //    var cvModel = new CvInputViewModel()
        //    {
        //        Description = "az",
        //        ImageUrl = "wwww",
        //        User = user,
        //        UserId = user.Id
        //    };

        //    this._cvService.CreateCv(cvModel);
        //    this._context.SaveChanges();

        //    var actualCv = this._context.Cvs.FirstOrDefault();

        //    Assert.Equal(cvModel.Description, actualCv.Description);
        //    Assert.Equal(cvModel.ImageUrl, actualCv.ImageUrl);

        //}
    }
}
