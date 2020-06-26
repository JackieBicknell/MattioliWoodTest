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
        //private readonly ILogger<HomeController> _logger;

        private UserLogic _userLogic = new UserLogic();

        public HomeViewModel homeViewModel = new HomeViewModel();

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            GetAllStaffRecords(homeViewModel);
            GetAllClientRecords(homeViewModel);
            return View(homeViewModel);
        }

        public async Task<IActionResult> DeleteUser(int id, string userType)
        {
            await _userLogic.DeleteUser(id, userType);
            GetAllStaffRecords(homeViewModel);
            GetAllClientRecords(homeViewModel);
            ViewBag.UserDeletedMsg = "Record Successfully deleted!";
            return View("Index", homeViewModel);
        }
        private List<Staff> GetAllStaffRecords(HomeViewModel homeViewModel)
        {
            List<Staff> staffList = _userLogic.GetAllStaffRecords();
            if(staffList.Count != 0)
            {
                homeViewModel.StaffList = staffList;
            }
            return staffList;
        }

        private List<Client> GetAllClientRecords(HomeViewModel homeViewModel)
        {
            List<Client> clientList = _userLogic.GetAllClientRecords();
            if (clientList.Count != 0)
            {
                homeViewModel.clientList = clientList;
            }
            return clientList ;
        }

        public IActionResult SaveUser(string forename, string surname, DateTime dateOfBirth, string userType, string firstLineAddress, string secondLineAddress, string postcode)
        { 
            HomeViewModel homeViewModel = new HomeViewModel();
            if (CheckInputDoesNotAlreadyExist(forename, surname, userType))
            {
                 UserExistsMessageToView(forename, surname, homeViewModel);
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
                UserSuccessfullySavedInDBMsg(forename, surname, homeViewModel);

            }
           
            GetAllStaffRecords(homeViewModel);
            GetAllClientRecords(homeViewModel);
            return View("Index", homeViewModel);
        }

        private void UserExistsMessageToView(string forename, string surname, HomeViewModel homeViewModel)
        {
            string fullname = forename + " " + surname;
            ViewBag.UserExistsMsg = $"The details entered for '{fullname}' already exists";
            homeViewModel.UserExistMsg = $"The details entered for '{fullname}' already exists";
  
        }

        private void UserSuccessfullySavedInDBMsg(string forename, string surname, HomeViewModel homeViewModel)
        {
            string fullname = forename + " " + surname;
            ViewBag.SuccessfullyAddedMsg = $"{fullname} has been successfully added";
        }

        public void SaveStaff(string forename, string surname, DateTime dateOfBirth)
        {
            Staff newStaff = CreateStaffObj(forename, surname, dateOfBirth);
            _userLogic.AddStaffToDataBase(newStaff);
        }

        public void SaveClient(string forename, string surname, DateTime dateOfBirth, string firstLineAddress, string secondLineAddress, string postcode)
        {
            Client newClient = CreateClientObj(forename, surname, dateOfBirth, firstLineAddress, secondLineAddress, postcode);
            _userLogic.AddClientToDataBase(newClient);
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
