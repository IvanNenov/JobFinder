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
    public class FormEntryTests
    {
        private readonly IFormEntryService _formEntryService;
        private readonly JobDbContext _context;

        public FormEntryTests()
        {
            var options = new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new JobDbContext(options);

            this._formEntryService = new FormEntryService(_context);
        }

        [Fact]
        public void TestCreateFormEntry_ShouldCreateCorrectFormEntry()
        {
            var entryModel = new FormEntryInputViewModel()
            {
                SenderName = "ivan",
                SenderEmail = "ivan@abv.bg",
                Message = "super"
            };

            this._formEntryService.CreateFormEntry(entryModel);

            var expectedEntryMapped = new FormEntry()
            {
                SenderEmail = entryModel.SenderEmail,
                MessageBody = entryModel.Message,
                SenderName = entryModel.SenderName
            };

            var actualEntry = this._context.FormEntries.FirstOrDefault();

            Assert.Equal(expectedEntryMapped.MessageBody, actualEntry.MessageBody);
            Assert.Equal(expectedEntryMapped.SenderName, actualEntry.SenderName);
            Assert.Equal(expectedEntryMapped.SenderEmail, actualEntry.SenderEmail);
        }

        [Fact]
        public void TestGetAllEntries_ShouldReturnAllEntries()
        {
            var entryModel = new FormEntry()
            {
                SenderName = "ivan",
                SenderEmail = "ivan@abv.bg",
                MessageBody = "super"
            };

            var entryModel2 = new FormEntry()
            {
                SenderName = "petko",
                SenderEmail = "petko@abv.bg",
                MessageBody = "perfect"
            };

            this._context.FormEntries.Add(entryModel);
            this._context.FormEntries.Add(entryModel2);

            this._context.SaveChanges();

            var listOfEntriesActual = this._formEntryService.GetAll();
            var countOfJob = this._context.FormEntries.Count();
            Assert.Equal(listOfEntriesActual.Count() , countOfJob);
        }

        [Fact]
        public void TestDeleteFormEntry_ShouldDeleteCorrectEntry()
        {
            var id = Guid.NewGuid().ToString();
            const int expectedEntriesCount = 1;
            var entryModel = new FormEntry()
            {
                Id = id,
                SenderName = "ivan",
                SenderEmail = "ivan@abv.bg",
                MessageBody = "super"
            };

            this._context.Add(entryModel);
            this._context.SaveChanges();

            var ActualCountOfEntries = this._context.FormEntries.Count();
            Assert.Equal(expectedEntriesCount,ActualCountOfEntries);

            this._formEntryService.DeleteFormEntry(id);

            var expectedNull = this._context.FormEntries.FirstOrDefault(x => x.Id == id);

            Assert.Null(expectedNull);
        }
    }
}
