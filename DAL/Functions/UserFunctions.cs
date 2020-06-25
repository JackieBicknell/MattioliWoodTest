using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Functions
{
    public class UserFunctions : IUserFunction
    {
        SqlConnection sqlConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JackieB\source\repos\MattioliWoodTest\DAL\MattioliWoodDB.mdf;Integrated Security=True;");

        public UserFunctions()
        {

        }

        public List<Staff> GetAllStaffRecords()
        {
            List<Staff> staffList = new List<Staff>();
            string sqlQuery = "  SELECT * FROM Staff";
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Staff thisStaff = new Staff();
            if (reader.Read())
            {
                thisStaff.Forename = reader["Forename"] as string;
                thisStaff.Surname = reader["Surname"] as string;
                thisStaff.DateOfBirth = (DateTime)reader["DateOfBirth"];
            }
            staffList.Add(thisStaff);
            sqlConn.Close();
            return staffList;
        }

        public string AddStaffRecordToDB(Staff newStaff)
        {
            sqlConn.Close();
            using (sqlConn)
            {
                sqlConn.Open();
                String query = "INSERT INTO Staff (forename,surname,dateOfBirth) VALUES (@Forename,@Surname, @DateOfBirth)";

                using (SqlCommand command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@Forename", newStaff.Forename);
                    command.Parameters.AddWithValue("@Surname", newStaff.Surname);
                    command.Parameters.AddWithValue("@DateOfBirth", newStaff.DateOfBirth);
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
                sqlConn.Close();
                return "Staff has been successfully saved in database";

            }
        }

        public string AddClientToDataBase(Client newClient)
        {
            using (sqlConn)
            {
                string query = "INSERT INTO Clients (forename, surname, dateOfBirth, firstLineAddress, secondLineAddress, postcode) VALUES (@Forename,@Surname, @DateOfBirth, @FirstLineAddress, @SecondLineAddress, @PostCode)";

                using (SqlCommand command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@Forename", newClient.Forename);
                    command.Parameters.AddWithValue("@Surname", newClient.Surname);
                    command.Parameters.AddWithValue("@DateOfBirth", newClient.DateOfBirth);
                    command.Parameters.AddWithValue("@FirstLineAddress", newClient.AddressLine1);

                    if (!string.IsNullOrEmpty(newClient.AddressLine2))
                    {
                        command.Parameters.AddWithValue("@SecondLineAddress", newClient.AddressLine2);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@SecondLineAddress", DBNull.Value);
                    }
                    command.Parameters.AddWithValue("@PostCode", newClient.PostCode);

                    sqlConn.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
                sqlConn.Close();
                return "Successfully Added to database";

            }
        }

        public bool CheckUserExistsInDB(string forename, string surname, string userType)
        {
            bool userExists = false;
            switch (userType)
            {
                case "Staff":
                    userExists = CheckStaffExists(forename, surname);
                        break;
                case "Client":
                    userExists = CheckClientExists(forename, surname);
                    break;
                //default:
                //    throw new NotImplementedException();
            }
            return userExists;
        }

        public bool CheckStaffExists(string forename, string surname)
        {
            bool doesUserExist = false;
            string sqlQuery = "SELECT * FROM Staff WHERE forename = @forename AND surname = @surname";
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            sqlCommand.Parameters.AddWithValue("@forename", forename);
            sqlCommand.Parameters.AddWithValue("@surname", surname);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                doesUserExist = true;
            }
            return doesUserExist;
        }


        public bool CheckClientExists(string forename, string surname)
        {
            bool doesUserExist = false;
            string sqlQueryForClient = "SELECT * FROM Clients WHERE forename = @forename AND surname = @surname";
            sqlConn.Open();
            SqlCommand sqlClientCommand = new SqlCommand(sqlQueryForClient, sqlConn);
            sqlClientCommand.Parameters.AddWithValue("@forename", forename);
            sqlClientCommand.Parameters.AddWithValue("@surname", surname);
            SqlDataReader reader = sqlClientCommand.ExecuteReader();
            if (reader.Read())
            {
                doesUserExist = true;
            }
            sqlConn.Close();
            return doesUserExist;
        }
    }

}
