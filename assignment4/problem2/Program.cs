using problem2.Model;

namespace problem2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee("Rahul Suthar", 250000);
            Console.WriteLine("Employee Details");
            emp.Display();
            Manager mnger = new Manager("Sandesh Naik",2500000, 50000);
            Console.WriteLine("Manager Details");
            mnger.Display();
            Console.WriteLine("------------------------");
        }
    }
}
