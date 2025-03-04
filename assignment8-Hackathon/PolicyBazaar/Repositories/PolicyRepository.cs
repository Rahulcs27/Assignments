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
        public void AddPolicy()
        {
            Console.WriteLine("Enter Policy ID:");
            int id = Convert.ToInt32(Console.ReadLine());

            // Check if policy already exists
            if (policies.Exists(p => p.PolicyId == id))
            {
                Console.WriteLine("Policy Already Exists");
                return;
            }
            Console.Write("Enter Policy Holder Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Policy Type (Life, Health, Vehicle, Property): ");
            PolicyType type = (PolicyType)Enum.Parse(typeof(PolicyType), Console.ReadLine(), true);

            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter End Date (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            policies.Add(new Policy(id, name, type, startDate, endDate));
            Console.WriteLine("Policy added successfully!");
        }
        public void ViewPolicy()
        {
            if (policies.Count == 0)
            {
                Console.WriteLine("No policies found.");
                return;
            }

            foreach (var policy in policies)
            {
                Console.WriteLine(policy);
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
            Console.Write("Enter Policy ID to update: ");
            int id = int.Parse(Console.ReadLine());

            var policy = policies.Find(p => p.PolicyId == id);
            if (policy == null)
            {
                Console.WriteLine("Policy not found!");
                return;
            }

            Console.Write("Enter new Policy Holder Name: ");
            policy.PolicyHolderName = Console.ReadLine();

            Console.WriteLine("Policy updated successfully!");
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
