using PolicyManagement.Interface;
using PolicyManagement.Models;
using PolicyManagement.Repository;

namespace PolicyManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserService userService = new UserService();
            IPolicyRepository policyRepo = new PolicyRepository();

            while (true)
            {
                Console.WriteLine("\n Welcome To PolicyBazaar");
                Console.WriteLine("\n1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Enter choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        userService.RegisterUser();
                        break;

                    case 2:
                        Console.Write("Enter username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();

                        if (userService.AuthenticateUser(username, password))
                        {
                            Console.WriteLine("\nLogin successful! Welcome to Policy Bazaar Portal");
                            ShowPolicyMenu(policyRepo);
                        }
                        else
                        {
                            Console.WriteLine("Invalid credentials! Try again.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }

        static void ShowPolicyMenu(IPolicyRepository policyRepo)
        {
            while (true)
            {
                Console.WriteLine("\nPolicyBazaar Menu:");
                Console.WriteLine("1. Add Policy");
                Console.WriteLine("2. View Policies");
                Console.WriteLine("3. Search Policy by ID");
                Console.WriteLine("4. Update Policy");
                Console.WriteLine("5. Delete Policy");
                Console.WriteLine("6. Logout");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        int policyId = policyRepo.AddPolicy();
                        if (policyId > 0)
                            Console.WriteLine($"Policy added successfully! Policy ID: {policyId}");
                        else
                            Console.WriteLine("Failed to add policy.");
                        break;

                    case 2:
                        List<Policy> policies = policyRepo.ViewPolicy();
                        if (policies.Count == 0)
                            Console.WriteLine("No policies found!");
                        else
                            foreach (var policy in policies)
                                Console.WriteLine($"ID: {policy.PolicyId}, Name: {policy.PolicyHolderName}, Type: {policy.PolicyType}, Start: {policy.StartDate}, End: {policy.EndDate}");
                        break;

                    case 3:
                        Console.Write("Enter Policy ID to search: ");
                        if (!int.TryParse(Console.ReadLine(), out int searchId))
                        {
                            Console.WriteLine("Invalid ID!");
                            break;
                        }
                        Policy foundPolicy = policyRepo.SearchPolicyById(searchId);
                        if (foundPolicy != null)
                            Console.WriteLine($"Found: ID {foundPolicy.PolicyId}, Name: {foundPolicy.PolicyHolderName}, Type: {foundPolicy.PolicyType}");
                        else
                            Console.WriteLine("Policy not found!");
                        break;
                    case 4:
                        Console.Write("Enter Policy ID to update: ");
                        if (!int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            Console.WriteLine("Invalid ID!");
                            break;
                        }
                        bool updated = policyRepo.UpdatePolicy(updateId);
                        break;


                    case 5:
                        Console.Write("Enter Policy ID to delete: ");
                        if (!int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            Console.WriteLine("Invalid ID!");
                            break;
                        }
                        bool deleted = policyRepo.DeletePolicy(deleteId);
                        Console.WriteLine(deleted ? "Policy deleted successfully!" : "Policy deletion failed.");
                        break;

                    case 6:
                        Console.WriteLine("Logging out...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }
    }

}
