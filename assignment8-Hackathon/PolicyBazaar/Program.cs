using PolicyBazaar.Interfaces;
using PolicyBazaar.Repositories;

class Program
{
    static void Main()
    {
        IUserService userService = new UserService();
        IPolicyRepository policyRepo = new PolicyRepository();

        while (true)
        {
            Console.WriteLine("\n1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice: ");
            int choice = int.Parse(Console.ReadLine());

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

                    if (userService.authenticateUser(username, password))
                    {
                        Console.WriteLine("\n Login successful! Welcome to the Policy Bazaar");

                        // Show Policy Menu
                        ShowPolicyMenu(policyRepo);
                    }
                    else
                    {
                        Console.WriteLine(" Invalid credentials! Try again.");
                    }
                    break;

                case 3:
                    Console.WriteLine("Exiting... Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice! Try again.");
                    break;
            }
        }
    }

    // Policy Management Menu
    static void ShowPolicyMenu(IPolicyRepository policyRepo)
    {
        while (true)
        {
            Console.WriteLine("\n PolicyBazaar Menu:");
            Console.WriteLine("1. Add Policy");
            Console.WriteLine("2. View All Policies");
            Console.WriteLine("3. Search Policy by ID");
            Console.WriteLine("4. Update Policy");
            Console.WriteLine("5. Delete Policy");
            Console.WriteLine("6. View Active Policies");
            Console.WriteLine("7. Logout");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    policyRepo.AddPolicy();
                    break;
                case 2:
                    policyRepo.ViewPolicy();
                    break;
                case 3:
                    policyRepo.SearchPolicyById();
                    break;
                case 4:
                    policyRepo.UpdatePolicy();
                    break;
                case 5:
                    try
                    {
                        policyRepo.DeletePolicy();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                case 6:
                    policyRepo.ViewActivePolicy();
                    break;
                case 7:
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    Console.WriteLine("Invalid choice! Try again.");
                    break;
            }
        }
    }
}
