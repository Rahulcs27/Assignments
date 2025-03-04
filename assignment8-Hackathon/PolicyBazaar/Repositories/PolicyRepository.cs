using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsurancePolicyApp.Exceptions;
using PolicyBazaar.Interfaces;
using PolicyBazaar.Models;

namespace PolicyBazaar.Repositories
{
    internal class PolicyRepository : IPolicyRepository
    {
        private List<Policy> policies = new List<Policy>();
        private int nextPolicyId = 1; 

        public void AddPolicy()
        {
            Console.Write("Enter Policy Holder Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Policy Type (Life, Health, Vehicle, Property): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out PolicyType type) || !Enum.IsDefined(typeof(PolicyType), type))
            {
                Console.WriteLine("❌ Invalid policy type! Please enter a valid type.");
                return;
            }

            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("❌ Invalid date format! Please enter in yyyy-MM-dd.");
                return;
            }

            Console.Write("Enter End Date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate <= startDate)
            {
                Console.WriteLine(" End Date must be after Start Date!");
                return;
            }

            Policy newPolicy = new Policy(nextPolicyId++, name, type, startDate, endDate); // Auto-increment ID
            policies.Add(newPolicy);

            Console.WriteLine($" Policy added successfully! Policy ID: {newPolicy.PolicyId}");
        }
        public void ViewAllPolicy()
        {
            if (policies.Count == 0)
            {
                Console.WriteLine("No policies found!");
                return;
            }

            Console.WriteLine("\n List of Policies:");
            foreach (var policy in policies)
            {
                Console.WriteLine($" ID: {policy.PolicyId} | Name: {policy.PolicyHolderName} | Type: {policy.PolicyType} | Ends: {policy.EndDate:yyyy-MM-dd}");
            }
        }

        public void SearchPolicyById()
        {
            Console.Write("Enter Policy ID to search: ");
            int id = int.Parse(Console.ReadLine());

            var policy = policies.Find(p => p.PolicyId == id);
            if (policy != null)
                Console.WriteLine(policy);
            else
                Console.WriteLine("Policy not found!");
        }

        public void UpdatePolicy()
        {
            try
            {
                Console.Write("Enter Policy ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("❌ Invalid Policy ID! Please enter a number.");
                    return;
                }

                var policy = policies.Find(p => p.PolicyId == id);
                if (policy == null)
                {
                    Console.WriteLine($"❌ Policy with ID {id} not found!");
                    return;
                }

                Console.WriteLine("\nWhat would you like to update?");
                Console.WriteLine("1. Policy Holder Name");
                Console.WriteLine("2. Policy Type");
                Console.WriteLine("3. End Date");
                Console.Write("Enter your choice (1-3): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter new Policy Holder Name: ");
                        string name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                            policy.PolicyHolderName = name;
                        break;

                    case "2":
                        Console.Write("Enter new Policy Type (Life, Health, Vehicle, Property): ");
                        string typeInput = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(typeInput) && Enum.TryParse(typeInput, true, out PolicyType newType) && Enum.IsDefined(typeof(PolicyType), newType))
                        {
                            policy.Type = newType;
                        }
                        else
                        {
                            Console.WriteLine(" Invalid policy type! Keeping the existing type.");
                        }
                        break;

                    case "3":
                        Console.Write("Enter new End Date (yyyy-MM-dd): ");
                        string endDateInput = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(endDateInput) && DateTime.TryParse(endDateInput, out DateTime newEndDate) && newEndDate > policy.StartDate)
                        {
                            policy.EndDate = newEndDate;
                        }
                        else
                        {
                            Console.WriteLine(" Invalid or past date! Keeping the existing end date.");
                        }
                        break;

                    default:
                        Console.WriteLine(" Invalid choice! No changes made.");
                        return;
                }

                Console.WriteLine(" Policy updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }




        public void DeletePolicy()
        {
            Console.Write("Enter Policy ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            var policy = policies.Find(p => p.PolicyId == id);
            if (policy == null)
                throw new PolicyNotFoundException($"Policy with ID {id} not found.");

            policies.Remove(policy);
            Console.WriteLine("Policy deleted successfully!");
        }

        public void ViewActivePolicy()
        {
            foreach (var policy in policies)
            {
                if (policy.IsActive())
                    Console.WriteLine(policy);
            }
        }

    }
}
