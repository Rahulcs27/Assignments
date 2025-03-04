using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Employee Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Joining Date (yyyy-mm-dd): ");
        DateTime joiningDate = DateTime.Parse(Console.ReadLine());

        Employee emp = new Employee(name, joiningDate);
        Console.WriteLine($"✅ {emp.Name} has {emp.GetExperience()} years of experience.");
    }
}
