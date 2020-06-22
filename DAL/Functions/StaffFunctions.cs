using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Functions
{
    public class StaffFunctions : StaffFunctionInterface
    {
        SqlConnection sqlConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JackieB\source\repos\MattioliWoodTest\DAL\MattioliWoodDB.mdf;Integrated Security=True;");

        public StaffFunctions()
        {
           
        }

        public string AddStaffInDb()
        {
            Staff thisStaff = new Staff();

            string sqlQuery = "  SELECT * FROM Staff";
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                thisStaff.Forename = reader["Forename"] as string;               
            }
            sqlConn.Close();
            TestAddStaff();
            return thisStaff.Forename;
        }

         public string TestAddStaff()
        {
            using (sqlConn)
            {
                String query = "INSERT INTO Staff (forename,surname,dateOfBirth) VALUES (@Forename,@Surname, @DateOfBirth)";

                using (SqlCommand command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@Forename", "Kelly");
                    command.Parameters.AddWithValue("@Surname", "Anderson");
                    command.Parameters.AddWithValue("@DateOfBirth", "12-11-1998");

                    sqlConn.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
                sqlConn.Close();
                return "Anderson";

            }


        }
    }
}
