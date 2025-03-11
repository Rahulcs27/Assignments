using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicyManagement.Interface;
using PolicyManagement.Utility;

namespace PolicyManagement.Repository
{
    internal class UserService : IUserService
    {
        private readonly string connectionString = DbConnUtil.GetConnectionString();

        public void RegisterUser()
        {
            try
            {
                Console.Write("Enter New Username: ");
                string username = Console.ReadLine();

                Console.Write("Enter New Password: ");
                string password = Console.ReadLine();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Users (UserName, Password) VALUES (@username, @password)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine(" User registered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error: {ex.Message}");
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE UserName = @username AND Password = @password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
