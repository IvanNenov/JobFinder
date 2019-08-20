using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services;
using JobFinder.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JobFinder.Tests.Service
{
    public class UserServiceTests
    {
        //void DeleteFromFavorite(string jobAddId);

        private readonly IUserService _userService;
        private readonly JobDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new JobDbContext(options);
            _accessor = new HttpContextAccessor();
            this._userService = new UserService(_context, _accessor);
        }

        [Fact]
        public void TestDeleteFromApplied_ShouldDeleteJobAdFromUserApplied()
        {
            var userId = Guid.NewGuid().ToString();
            var jobId = Guid.NewGuid().ToString();

            var user = new User()
            {
                Id = userId,
                UserName = "ivan",
                Email = "ivan@abv.bg",
            };

            var job = new JobAdd()
            {
                Id = jobId,
                JobTitle = "js",
                Description = "js",
                JobType = JobType.FullTime
            };

            var userJobAd = new UserJobAdds()
            {
                UserId = userId,
                User = user,
                JobAdd = job,
                JobAddId = jobId
            };
            this._context.Users.Add(user);
            this._context.SaveChanges();

            this._context.JobAdds.Add(job);
            this._context.SaveChanges();

            user.UserJobAdds.Add(userJobAd);
            job.CandidatesForPosition.Add(userJobAd);
            this._context.SaveChanges();

            var actual = this._context.UserJobAdds.FirstOrDefault();

            Assert.Equal(actual.JobAddId , userJobAd.JobAddId);
            Assert.Equal(actual.UserId, userJobAd.UserId);

            this._userService.DeleteFromApplied(jobId);

            var expectedNull = this._context.UserJobAdds.FirstOrDefault();

            Assert.Null(expectedNull);
        }

        [Fact]
        public void TestDeleteFromFavorite_ShouldDeleteJobAdFromUserFavorite()
        {
            var userId = Guid.NewGuid().ToString();
            var jobId = Guid.NewGuid().ToString();

            var user = new User()
            {
                Id = userId,
                UserName = "ivan",
                Email = "ivan@abv.bg",
            };

            var job = new JobAdd()
            {
                Id = jobId,
                JobTitle = "js",
                Description = "js",
                JobType = JobType.FullTime
            };

            var userJobAd = new FavoriteUserJobAds()
            {
                UserId = userId,
                User = user,
                JobAdd = job,
                JobAddId = jobId
            };

            this._context.Users.Add(user);
            this._context.SaveChanges();

            this._context.JobAdds.Add(job);
            this._context.SaveChanges();

            user.FavoriteJobs.Add(userJobAd);
            job.FavoriteUserJob.Add(userJobAd);
            this._context.SaveChanges();

            var actual = this._context.FavoriteUserJobAds.FirstOrDefault();

            Assert.Equal(actual.JobAddId, userJobAd.JobAddId);
            Assert.Equal(actual.UserId, userJobAd.UserId);

            this._userService.DeleteFromFavorite(jobId);

            var expectedNull = this._context.FavoriteUserJobAds.FirstOrDefault();

            Assert.Null(expectedNull);
        }
    }
}
