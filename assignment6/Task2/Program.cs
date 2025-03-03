using System;

class Program
{
    static void Main()
    {
        BankQueue bankQueue = new BankQueue();

        while (true)
        {
            Console.WriteLine("\n===== Bank Token System =====");
            Console.WriteLine("1. Get New Token");
            Console.WriteLine("2. Serve Customer");
            Console.WriteLine("3. Check Next Customer");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    int token = bankQueue.GetNewToken();
                    Console.WriteLine($"Token {token} issued.");
                    break;
                case 2:
                    int? servedToken = bankQueue.ServeCustomer();
                    if (servedToken == null)
                        Console.WriteLine("No customers in the queue.");
                    else
                        Console.WriteLine($" Serving Customer with Token {servedToken}.");
                    break;
                case 3:
                    int? nextToken = bankQueue.CheckNextCustomer();
                    if (nextToken == null)
                        Console.WriteLine(" No customers in the queue.");
                    else
                        Console.WriteLine($"Next customer to be served: Token {nextToken}.");
                    break;
                case 4:
                    Console.WriteLine("Exiting... Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
