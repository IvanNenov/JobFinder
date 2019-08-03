using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services
{
    public class CvService : ICvService
    {
        private readonly JobDbContext context;
        private readonly IHttpContextAccessor accessor;

        public CvService(JobDbContext context, IHttpContextAccessor accessor)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (accessor == null)
            {
                throw new ArgumentNullException(nameof(accessor));
            }
            
            this.context = context;
            this.accessor = accessor;
        }

        public void CreateCv(CvInputViewModel model)
        {
            var currentUserName = this.accessor.HttpContext.User.Identity.Name;
            var currentUserObject = this.context.Users.FirstOrDefault(x => x.UserName == currentUserName);

            var cv = new Cv
            {
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                User = currentUserObject,
                UserId = currentUserObject.Id,
            };
            this.context.Cvs.Add(cv);
            this.context.SaveChanges();

            var userToSetCvId = this.context.Users.FirstOrDefault(x => x.Id == currentUserObject.Id);

            userToSetCvId.Cv = cv;
            userToSetCvId.CvId = cv.Id;

            this.context.SaveChanges();
        }

        public bool HasCv()
        {
            var currentUserName = this.accessor.HttpContext.User.Identity.Name;
            var currentUserObject = this.context.Users.FirstOrDefault(x => x.UserName == currentUserName);

            if (currentUserObject?.CvId == null)
            {
                return false;
            }

            return true;
        }

        public CvOutputViewModel GetMyCv()
        {
            var currentUserName = this.accessor.HttpContext.User.Identity.Name;
            var currentUserObject = this.context.Users.FirstOrDefault(x => x.UserName == currentUserName);

            var cv = this.context.Cvs.FirstOrDefault(x => x.UserId == currentUserObject.Id);
            
            var cvOutputModel = new CvOutputViewModel
            {
                Description = cv.Description,
                ImageUrl = cv.ImageUrl,
                User = cv.User,
                UserId = cv.UserId,
                Email = cv.User.Email
            };

            return cvOutputModel;
        }
    }
}
