using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace myFacility.Web.Controllers.Vendor
{
    public class VendorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult registration()
        {
            return View();
        }
    }
}
