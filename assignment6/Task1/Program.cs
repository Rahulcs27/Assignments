using System;

class Program
{
    static void Main()
    {
        WorkshopManager manager = new WorkshopManager();

        while (true)
        {
            Console.WriteLine("\n===== Workshop Registration =====");
            Console.WriteLine("1. Register a Student");
            Console.WriteLine("2. View Registrations");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    RegisterStudent(manager);
                    break;
                case 2:
                    manager.DisplayRegistrations();
                    break;
                case 3:
                    Console.WriteLine("Exiting... Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void RegisterStudent(WorkshopManager manager)
    {
        Console.WriteLine("\n Choose a Workshop:");
        foreach (var workshop in manager.Workshops)
            Console.WriteLine($" {workshop.Key}. {workshop.Value}");

        Console.Write("Enter choice (1-3): ");
        if (!int.TryParse(Console.ReadLine(), out int workshopChoice))
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        Console.Write("Enter Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int studentID))
        {
            Console.WriteLine(" Invalid Student ID.");
            return;
        }

        if (manager.RegisterStudent(workshopChoice, studentID))
            Console.WriteLine($"Student {studentID} registered successfully.");
        else
            Console.WriteLine($" Student {studentID} is already registered or invalid workshop.");
    }
}
