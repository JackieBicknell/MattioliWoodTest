using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MattioliWoodTest.Models;
using MattioliBL;
using Entities;

namespace MattioliWoodTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private StaffLogic _staffLogic = new StaffLogic();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string AddStaff(string subject)
        {
            string staff = _staffLogic.AddStaff();
            return staff;
        }


        public string SaveUser(string forename, string surname, DateTime dateOfBirth, string UserType, string Address1, string Address2, string postcode)
        {
            string hello = "hi";

            switch(UserType)
            {
                case "staff":
                    SaveStaff(forename, surname, dateOfBirth);
                    break;
                case "client":
                    break;
            }
            return hello;
        }



        public string SaveStaff(string forename, string surname, DateTime dateOfBirth)
        {

           

            Staff currentStaff = new Staff();
            currentStaff.Forename = forename;
            currentStaff.Surname = surname;
            currentStaff.DateOfBirth = dateOfBirth;


            return "staff has been saved in database";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
