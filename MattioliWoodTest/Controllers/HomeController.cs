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

        private UserLogic _userLogic = new UserLogic();

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            List<Staff> staffList = _userLogic.GetAllStaffRecords();
            return View();
        }
        public IActionResult SaveUser(string forename, string surname, DateTime dateOfBirth, string userType, string firstLineAddress, string secondLineAddress, string postcode)
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            if (CheckInputDoesNotAlreadyExist(forename, surname, userType))
            {
                return UserExistsMessageToView(forename, surname, homeViewModel);
            }
            else
            {
                switch (userType)
                {
                    case "Staff":
                        SaveStaff(forename, surname, dateOfBirth);
                        break;
                    case "Client":
                        SaveClient(forename, surname, dateOfBirth, firstLineAddress, secondLineAddress, postcode);
                        break;
                    default:
                        homeViewModel.ErrorMsg = "Something has gone wrong, please try again";
                        ViewBag.ErrorMsg = "Something has gone wrong, please try again";
                        return View("Index", homeViewModel);
                        throw new NotImplementedException();
                }
                
            }
            return UserSuccessfullySavedInDBMsg(forename, surname, homeViewModel);
        }


        private IActionResult UserExistsMessageToView(string forename, string surname, HomeViewModel homeViewModel)
        {
            string fullname = forename + " " + surname;
            ViewBag.UserExistsMsg = $"The details entered for '{fullname}' already exists";
            return View("Index", homeViewModel);
        }

        private IActionResult UserSuccessfullySavedInDBMsg(string forename, string surname, HomeViewModel homeViewModel)
        {
            string fullname = forename + " " + surname;
            ViewBag.SuccessfullyAddedMsg = $"{fullname} has been successfully added";
            return View("Index", homeViewModel);
        }

        public string SaveStaff(string forename, string surname, DateTime dateOfBirth)
        {
            Staff newStaff = CreateStaffObj(forename, surname, dateOfBirth);
            _userLogic.AddStaffToDataBase(newStaff);
            return "staff has been saved in database";
        }

        public string SaveClient(string forename, string surname, DateTime dateOfBirth, string firstLineAddress, string secondLineAddress, string postcode)
        {
            Client newClient = CreateClientObj(forename, surname, dateOfBirth, firstLineAddress, secondLineAddress, postcode);
            _userLogic.AddClientToDataBase(newClient);
            return "staff has been saved in database";
        }

        public bool CheckInputDoesNotAlreadyExist(string forename, string surname, string userType)
        {
            bool doesUserExist = _userLogic.CheckUserExists(forename, surname, userType);
            return doesUserExist;
        }

        private static Staff CreateStaffObj(string forename, string surname, DateTime dateOfBirth)
        {
            Staff newStaff = new Staff();
            newStaff.Forename = forename;
            newStaff.Surname = surname;
            newStaff.DateOfBirth = dateOfBirth;
            return newStaff;
        }

        private static Client CreateClientObj(string forename, string surname, DateTime dateOfBirth, string firstLineAddress, string secondLineAddress, string postcode)
        {
            Client newClient = new Client();
            newClient.Forename = forename;
            newClient.Surname = surname;
            newClient.DateOfBirth = dateOfBirth;
            newClient.AddressLine1 = firstLineAddress;
            newClient.AddressLine2 = secondLineAddress;
            newClient.PostCode = postcode;
            return newClient;
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
