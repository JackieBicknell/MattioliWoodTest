﻿using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MattioliBL
{

    public class UserLogic
    {
        private IUserFunction _staffFunction = new DAL.Functions.UserFunctions();

        public List<Staff> GetAllStaffRecords()
        {
            List<Staff> allStaffRecord = new List<Staff>();
            allStaffRecord = _staffFunction.GetAllStaffRecords();
            return allStaffRecord;
        }


        public string AddStaffToDataBase(Staff newStaff)
        {
            string staff = _staffFunction.AddStaffRecordToDB(newStaff);
            return staff;
        }


        public string AddClientToDataBase(Client newClient)
        {
            string client = _staffFunction.AddClientToDataBase(newClient);
            return client;
        }

        public bool CheckUserExists(string forename, string surname, string userType)
        {
            bool doesUserExist = _staffFunction.CheckUserExistsInDB(forename, surname, userType);
            return doesUserExist;
        }
    }
}
