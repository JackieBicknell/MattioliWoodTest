using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserFunction
    {
        List<Staff> GetAllStaffRecords();

        string AddStaffRecordToDB(Staff newStaff);

        string AddClientToDataBase(Client newClient);

        bool CheckUserExistsInDB(string forename, string surname, string userType);
        List<Client> GetAllClientRecords();
        Task DeleteUser(int id, string userType);
    }
}
