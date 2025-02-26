using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using assignment.Model;

namespace assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter UserName to log in (or type exit) to Stop");
                string userName = Console.ReadLine();
                if (userName.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting... Final User Count:");
                    User.ShowTotalUsers();
                    break;
                }

                User newUser = new User(userName); // Creating a user increases count
                Console.WriteLine($"User '{userName}' logged in.");
                User.ShowTotalUsers(); // Show updated count
                Console.WriteLine("------------------------");
            }  
        }
    }
}
