namespace assignment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //#region Salary Calculation System
            //Console.WriteLine("Enter Basic Salary");
            //double basicSalary = Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("Enter Performance Rating (0-10)");
            //double performanceRating = Convert.ToDouble(Console.ReadLine());

            ////calculating Tax deduction based on basic salary
            //double tax = 0.10 * basicSalary;

            ////calculating bonus based on performance rating
            //double bonus;
            //if (performanceRating >= 8)
            //{
            //    bonus = 0.20 * basicSalary;
            //}
            //else if (performanceRating >= 5)
            //{
            //    bonus = 0.10 * basicSalary;
            //}
            //else
            //{
            //    bonus = 0;

            //}
            //// Calculate Net Salary
            //double netSalary = basicSalary - tax + bonus;

            //// Display Results
            //Console.WriteLine("\n--- Salary Details ---");
            //Console.WriteLine($"Basic Salary: {basicSalary}");
            //Console.WriteLine($"Tax Deduction (10%): {tax}");
            //Console.WriteLine($"Bonus: {bonus}");
            //Console.WriteLine($"Net Salary: {netSalary}");
            //#endregion
            //#region Ticket Management System
            //int choice = 0;
            //string trainType = "";
            //int typeAmt = 0, noOfTickets;
            //double totalTicketAmt;
            //do
            //{
            //takeChoice:
            //    Console.WriteLine("--- Online Train Booking System ---");
            //    Console.WriteLine("Select the type of train ticket you want to book:");
            //    Console.WriteLine("1. General (Rs. 200)\n2. AC (Rs. 1000)\n3. Sleeper (Rs. 500)\n4. Exit");
            //    bool isChoice = int.TryParse(Console.ReadLine(), out choice);
            //    if (isChoice == false)
            //    {
            //        Console.WriteLine("Please enter correct choice number.");
            //        goto takeChoice;
            //    }
            //    switch (choice)
            //    {
            //        case 1:
            //            trainType = "General";
            //            typeAmt = 200;
            //            break;
            //        case 2:
            //            trainType = "AC";
            //            typeAmt = 1000;
            //            break;
            //        case 3:
            //            trainType = "Sleeper";
            //            typeAmt = 500;
            //            break;
            //        case 4:
            //            break;
            //        default:
            //            Console.WriteLine("Please enter correct choice number.");
            //            break;
            //    }

            //    if (choice >= 1 && choice < 4)
            //    {
            //        Console.WriteLine("How many tickets do you want to buy: ");
            //        noOfTickets = Convert.ToInt32(Console.ReadLine());
            //        totalTicketAmt = typeAmt * noOfTickets;
            //        Console.WriteLine($"Your Total Amount to be paid for {noOfTickets} {trainType} ticket(s) is: Rs. {totalTicketAmt}");
            //        Console.WriteLine("Thank you for booking.:)");
            //    }
            //} while (choice != 4);
            //#endregion
            #region Online Shopping Platform


            int[,] userData = new int[2, 2]
            {
                { 101, 10000 },
                { 102, 69000 }
            };

            while (true)
            {
                Console.Write("Please enter your user ID: ");
                if (int.TryParse(Console.ReadLine(), out int userID))
                {
                    int balance = -1; // if user is found

                    // Searching for user ID in the 2D array
                    for (int i = 0; i < userData.GetLength(0); i++)
                    {
                        if (userData[i, 0] == userID)
                        {
                            balance = userData[i, 1];
                            break;
                        }
                    }

                    if (balance != -1)
                    {
                        Console.WriteLine($"Your available wallet balance is Rs. {balance}.");
                        break; // Exit program after successful login
                    }
                    else
                    {
                        Console.WriteLine("Wrong User ID entered. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a numeric User ID.");
                }
            }
            #endregion
        }
    }
}