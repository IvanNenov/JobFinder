using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.Data;
using JobFinder.Models;
using JobFinder.Services.Contracts;
using JobFinder.ViewModels.InputViewModels;

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
                MessageBody = model.Message
            };

            this.context.FormEntries.Add(entry);
            this.context.SaveChanges();
        }
    }
}
