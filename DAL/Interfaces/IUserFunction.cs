using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IUserFunction
    {
        List<Staff> GetAllStaffRecords();

        string AddStaffRecordToDB(Staff newStaff);

        string AddClientToDataBase(Client newClient);

        bool CheckUserExistsInDB(string forename, string surname, string userType);
    }
}
