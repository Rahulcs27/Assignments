using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace problem2.Model
{
    class Employee
    {
        public string Name { get; set; }
        public double Salary { get; set; }

        public Employee(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }
        public virtual void Display()
        {
            Console.WriteLine($"Employee Name :: {Name} \t Salary :: {Salary}");
        }

    }
}
