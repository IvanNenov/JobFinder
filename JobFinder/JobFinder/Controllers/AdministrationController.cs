using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    public class AdministrationController : Controller
    {
        public IActionResult AdminIndex()
        {
            return this.View();
        }
    }
}
