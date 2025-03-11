using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicyManagement.Constants;
using PolicyManagement.Exceptions;
using PolicyManagement.Interface;
using PolicyManagement.Models;
using PolicyManagement.Utility;

namespace PolicyManagement.Repository
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly string connectionString = DbConnUtil.GetConnectionString();

        public int AddPolicy()
        {
            try
            {
                Console.Write("Enter Policy Holder Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Policy Type (Life, Health, Vehicle, Property): ");
                if (!Enum.TryParse(Console.ReadLine(), true, out PolicyType type))
                {
                    Console.WriteLine(" Invalid policy type!");
                    return 0;
                }

                Console.Write("Enter End Date (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate <= DateTime.Now)
                {
                    Console.WriteLine(" Invalid End Date! It must be a future date.");
                    return 0;
                }

                DateTime startDate = DateTime.Now; 

                if (endDate <= startDate)
                {
                    Console.WriteLine("End date must be after the start date.");
                    return 0;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                INSERT INTO Policies (PolicyHolderName, PolicyType, StartDate, EndDate)  
                VALUES (@name, @type, @startDate, @endDate);
                SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@type", type.ToString());
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);

                        conn.Open();
                        int policyId = Convert.ToInt32(cmd.ExecuteScalar()); 
                        Console.WriteLine($" Policy added successfully! Policy ID: {policyId}");
                        return policyId;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error: {ex.Message}");
                return 0;
            }
        }

        //View all policies
        public List<Policy> ViewPolicy()
        {
            List<Policy> policies = new List<Policy>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Policies";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                policies.Add(new Policy
                                {
                                    PolicyId = (int)reader["PolicyId"],
                                    PolicyHolderName = reader["PolicyHolderName"].ToString(),
                                    PolicyType = reader["PolicyType"].ToString(),
                                    StartDate = (DateTime)reader["StartDate"],
                                    EndDate = (DateTime)reader["EndDate"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error: {ex.Message}");
            }

            return policies;
        }

        //  Search Policy by ID 
        public Policy SearchPolicyById(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Policies WHERE PolicyId = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Policy
                                {
                                    PolicyId = (int)reader["PolicyId"],
                                    PolicyHolderName = reader["PolicyHolderName"].ToString(),
                                    PolicyType = reader["PolicyType"].ToString(), 
                                    StartDate = (DateTime)reader["StartDate"],
                                    EndDate = (DateTime)reader["EndDate"]
                                };
                            }
                        }
                    }
                }
                throw new PolicyNotFoundException($"Policy with ID {id} not found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        // Update Policy 
        //public bool UpdatePolicy(int id)
        //{
        //    try
        //    {
        //        Console.Write("Enter New Policy Holder Name: ");
        //        string newName = Console.ReadLine();

        //        Console.Write("Enter New Policy Type (Life, Health, Vehicle, Property): ");
        //        if (!Enum.TryParse(Console.ReadLine(), true, out PolicyType newType))
        //        {
        //            Console.WriteLine(" Invalid policy type!");
        //            return false;
        //        }

        //        Console.Write("Enter New End Date (yyyy-MM-dd): ");
        //        if (!DateTime.TryParse(Console.ReadLine(), out DateTime newEndDate) || newEndDate <= DateTime.Now)
        //        {
        //            Console.WriteLine(" End Date must be a future date!");
        //            return false;
        //        }

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            string getStartDateQuery = "SELECT StartDate FROM Policies WHERE PolicyId = @id";
        //            DateTime startDate;

        //            using (SqlCommand cmd = new SqlCommand(getStartDateQuery, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@id", id);
        //                conn.Open();
        //                object result = cmd.ExecuteScalar();
        //                conn.Close();

        //                if (result == null)
        //                {
        //                    Console.WriteLine("Policy not found!");
        //                    return false;
        //                }

        //                startDate = (DateTime)result;
        //            }

        //            if (newEndDate <= startDate)
        //            {
        //                Console.WriteLine("Error: End date must be after the start date.");
        //                return false;
        //            }

        //            string query = "UPDATE Policies SET PolicyHolderName = @name, PolicyType = @type, EndDate = @endDate WHERE PolicyId = @id";

        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@id", id);
        //                cmd.Parameters.AddWithValue("@name", newName);
        //                cmd.Parameters.AddWithValue("@type", newType.ToString());
        //                cmd.Parameters.AddWithValue("@endDate", newEndDate);

        //                conn.Open();
        //                int rowsAffected = cmd.ExecuteNonQuery();
        //                return rowsAffected > 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($" Error: {ex.Message}");
        //        return false;
        //    }
        //}
        public bool UpdatePolicy(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string getPolicyQuery = "SELECT StartDate FROM Policies WHERE PolicyId = @id";
                    DateTime startDate;

                    using (SqlCommand cmd = new SqlCommand(getPolicyQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        conn.Close();

                        if (result == null)
                        {
                            Console.WriteLine("Policy not found!");
                            return false;
                        }

                        startDate = (DateTime)result;
                    }

                    Console.WriteLine("\nWhat would you like to update?");
                    Console.WriteLine("1. Policy Holder Name");
                    Console.WriteLine("2. Policy Type");
                    Console.WriteLine("3. End Date");
                    Console.WriteLine("4. Cancel");
                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    string query = "";
                    SqlCommand updateCmd = new SqlCommand();
                    updateCmd.Parameters.AddWithValue("@id", id);

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter New Policy Holder Name: ");
                            string newName = Console.ReadLine();
                            query = "UPDATE Policies SET PolicyHolderName = @value WHERE PolicyId = @id";
                            updateCmd.Parameters.AddWithValue("@value", newName);
                            break;

                        case "2":
                            Console.Write("Enter New Policy Type (Life, Health, Vehicle, Property): ");
                            if (!Enum.TryParse(Console.ReadLine(), true, out PolicyType newType))
                            {
                                Console.WriteLine("Invalid policy type!");
                                return false;
                            }
                            query = "UPDATE Policies SET PolicyType = @value WHERE PolicyId = @id";
                            updateCmd.Parameters.AddWithValue("@value", newType.ToString());
                            break;

                        case "3":
                            Console.Write("Enter New End Date (yyyy-MM-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime newEndDate) || newEndDate <= startDate)
                            {
                                Console.WriteLine("End Date must be after the Start Date!");
                                return false;
                            }
                            query = "UPDATE Policies SET EndDate = @value WHERE PolicyId = @id";
                            updateCmd.Parameters.AddWithValue("@value", newEndDate);
                            break;

                        case "4":
                            Console.WriteLine("Update cancelled.");
                            return false;

                        default:
                            Console.WriteLine("Invalid choice! Please enter 1, 2, or 3.");
                            return false;
                    }

                    updateCmd.CommandText = query;
                    updateCmd.Connection = conn;

                    conn.Open();
                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Policy updated successfully!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Failed to update policy.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


        // Delete Policy
        public bool DeletePolicy(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Policies WHERE PolicyId = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error: {ex.Message}");
                return false;
            }
        }

    }
}
