using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            sqlConn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JackieB\source\repos\MattioliWoodTest\DAL\MattioliWoodDB.mdf;Integrated Security=True;";
            List<Staff> staffList = new List<Staff>();
            string sqlQuery = "  SELECT * FROM Staff";
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Staff thisStaff = new Staff();
                thisStaff.Id = (int)reader["Id"];
                thisStaff.Forename = reader["Forename"] as string;
                thisStaff.Surname = reader["Surname"] as string;
                thisStaff.DateOfBirth = (DateTime)reader["DateOfBirth"];
                staffList.Add(thisStaff);
            }
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
            sqlConn.Close();
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

        public List<Client> GetAllClientRecords()
        {
            List<Client> clientList = new List<Client>();
            string sqlQuery = "SELECT * FROM Clients";
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Client thisClient = new Client();
                thisClient.Id = (int)reader["Id"];
                thisClient.Forename = reader["Forename"] as string;
                thisClient.Surname = reader["Surname"] as string;
                thisClient.DateOfBirth = (DateTime)reader["DateOfBirth"];
                thisClient.AddressLine1 = reader["FirstLineAddress"] as string;
                thisClient.AddressLine2 = reader["SecondLineAddress"] as string;
                thisClient.PostCode = reader["Postcode"] as string;
                clientList.Add(thisClient);
            }
            sqlConn.Close();
            return clientList;
        }

        public async Task DeleteUser(int id, string userType)
        {
           
            switch(userType)
            {
                case "Staff":
                    using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JackieB\source\repos\MattioliWoodTest\DAL\MattioliWoodDB.mdf;Integrated Security=True;"))
                    using (var command = new SqlCommand("SPROCDeleteRecordFromStaff", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                        
                    })
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        conn.Open();
                       await command.ExecuteNonQueryAsync();
                    }
                    sqlConn.Close();
                    break;
                case "Client":
                    using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JackieB\source\repos\MattioliWoodTest\DAL\MattioliWoodDB.mdf;Integrated Security=True;"))
                    using (var command = new SqlCommand("SPROCDeleteRecordFromClient", conn)
                    {
                        CommandType = CommandType.StoredProcedure

                    })
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        conn.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                    sqlConn.Close();
                    break;
            }
        }
    }

}
