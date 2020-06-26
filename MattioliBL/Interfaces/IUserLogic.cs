using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MattioliBL.Interfaces
{
    public interface IUserLogic
    {
        List<Staff> GetAllStaffRecords();

        List<Client> GetAllClientRecords();

        string AddStaffToDataBase(Staff newStaff);

        string AddClientToDataBase(Client newClient);

        bool CheckUserExists(string forename, string surname, string userType);

        Task DeleteUser(int id, string userType);
    }
}
