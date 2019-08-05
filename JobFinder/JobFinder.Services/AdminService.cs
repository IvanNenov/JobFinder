using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobFinder.Data;
using JobFinder.Services.Contracts;

namespace JobFinder.Services
{
    public class AdminService : IAdminService
    {
        private readonly JobDbContext _context;

        public AdminService(JobDbContext context)
        {
            _context = context;
        }
        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete(string jobAddId)
        {
            if (jobAddId != null)
            {
                var jobAdd = this._context.JobAdds.Find(jobAddId);

                this._context.Remove(jobAdd);
                this._context.SaveChanges();
            }
        }
    }
}
