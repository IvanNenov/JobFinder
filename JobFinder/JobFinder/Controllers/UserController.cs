using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Register()
        {
            return this.View();
        }
    }
}
