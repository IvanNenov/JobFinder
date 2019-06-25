using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    public class JobsController : Controller
    {
        public IActionResult PostJob()
        {
            return this.View();
        }

        //public async Task<IActionResult> PostJob(PostJobInputModel model)
        //{

        //}
    }
}
