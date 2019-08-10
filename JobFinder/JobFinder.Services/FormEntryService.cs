using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;
using JobFinder.ViewModels.OutputViewModels;

namespace JobFinder.Services
{
   public class FormEntryService : IFormEntryService
    {
        private readonly JobDbContext context;

        public FormEntryService(JobDbContext context)
        {
            this.context = context;
        }
        public void CreateFormEntry(FormEntryInputViewModel model)
        {
            var entry = new FormEntry
            {
                SenderName = model.SenderName,
                SenderEmail = model.SenderEmail,
                PhoneNumber = model.PhoneNumber,
                MessageBody = model.Message,
                CreatedOn = DateTime.UtcNow
            };

            this.context.FormEntries.Add(entry);
            this.context.SaveChanges();
        }

        public IEnumerable<FormEntryOutputViewModel> GetAll()
        {
            var entries = this.context.FormEntries.AsEnumerable();
            var listOfFormEntryViewModels = new List<FormEntryOutputViewModel>();

            foreach (var entry in entries)
            {
                var entryOutput = new FormEntryOutputViewModel
                {
                    Id = entry.Id,
                    Message = entry.MessageBody,
                    PhoneNumber = entry.PhoneNumber,
                    SenderEmail = entry.SenderEmail,
                    SenderName = entry.SenderName
                };

                listOfFormEntryViewModels.Add(entryOutput);
            }

            return listOfFormEntryViewModels;
        }

        public void DeleteFormEntry(string id)
        {
            var currentEntry = this.context.FormEntries.Find(id);
            this.context.Remove(currentEntry);
            this.context.SaveChanges();
        }
    }
}
