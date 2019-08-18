using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.OutputViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException(nameof(userService));
            }
            _userService = userService;
        }

        [Authorize()]
        [HttpPost]
        public IActionResult ApplyJobs(string id, int? currentPage)
        {
            var isAdded = this._userService.TryAddToApplied(id);

            return this.RedirectToAction(nameof(GetAppliedJobs));
        }

        [Authorize()]
        public IActionResult GetAppliedJobs(int? currentPage)
        {
            var allJobs = new List<AppliedJobOutputViewModel>();

            var page = currentPage ?? 1;
            var pageSize = 5;
            var skip = (page - 1) * pageSize;

            double totalPageCount;

            allJobs = this._userService
                .GetAppliedJobs()
                .Skip(skip)
                .Take(pageSize)
                .OrderByDescending(x => x.CreatedOn)
                .ToList();

            totalPageCount = Math.Ceiling((double)this._userService.GetAppliedJobs().Count() / pageSize);

            var viewModel = new ListOfFavoriteJobs()
            {
                AppliedJobs = allJobs,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPagesCount = totalPageCount,
                IsAny = true
            };

            if (allJobs.Count > 0)
            {
                return this.View(viewModel);
            }

            viewModel.IsAny = false;
            return View(viewModel);
        }

        [Authorize()]
        public IActionResult FavoriteJobs(int? currentPage)
        {
            var allJobs = new List<FavJobsDto>();

            var page = currentPage ?? 1;
            var pageSize = 5;
            var skip = (page - 1) * pageSize;

            double totalPageCount;

            allJobs = this._userService
                .GetFavoriteJobs()
                .Skip(skip)
                .Take(pageSize)
                .OrderByDescending(x => x.CreatedOn)
                .ToList();

            totalPageCount = Math.Ceiling((double)this._userService.GetFavoriteJobs().Count() / pageSize);

            var viewModel = new ListOfFavoriteJobs()
            {
                FavoriteJobsAds = allJobs,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPagesCount = totalPageCount,
                IsAny = true
            };

            if (allJobs.Count > 0)
            {
                return this.View(viewModel);
            }

            viewModel.IsAny = false;
            return View(viewModel);
        }

        [Authorize()]
        public IActionResult AddToFavoriteJobs(string id)
        {
            this._userService.TryAddToFavorite(id);
            return this.RedirectToAction(nameof(FavoriteJobs));
        }

        [Authorize()]
        public IActionResult RemoveFavorite(string id)
        {
            this._userService.DeleteFromFavorite(id);
            return this.RedirectToActionPermanent(nameof(FavoriteJobs));
        }

        [Authorize()]
        [HttpPost]
        public IActionResult RemoveApplied(string id)
        {
            this._userService.DeleteFromApplied(id);
            return this.RedirectToActionPermanent(nameof(GetAppliedJobs));
        }
    }
}