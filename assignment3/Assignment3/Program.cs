using System.Text;
using assignment3;

namespace Assignment3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Car Management System
            // Creating a Car object using the parameterized constructor
            Car myCar = new Car(101, "Toyota", "Corolla", 2022, 25000);
            myCar.DisplayCarDetails();

            Console.WriteLine("\n");

            // Creating a Car object using the default constructor
            Car defaultCar = new Car();
            defaultCar.DisplayCarDetails();
            #endregion
            #region Password Validation
            while (true) // Loop until a valid password is entered
            {
                Console.WriteLine("Please create your password: ");
                string password = Console.ReadLine();

                StringBuilder errors = new StringBuilder();

                if (password.Length < 6)
                    errors.AppendLine("Password must be at least 6 characters long.");
                if (!password.Any(char.IsUpper))
                    errors.AppendLine("Password must contain at least one uppercase letter.");
                if (!password.Any(char.IsDigit))
                    errors.AppendLine("Password must contain at least one digit.");

                if (errors.Length == 0)
                {
                    Console.WriteLine("Your password has been created successfully.");
                    break;
                }
                else
                    Console.WriteLine(errors.ToString()); // Show all errors at once
            }
            #endregion

        }
    }
}
