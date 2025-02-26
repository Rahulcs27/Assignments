using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace problem2.Model
{
    class Manager: Employee
    {
        public int Bonus { get; set; }

        public Manager(string name , double salary,int bonus) :base(name,salary)
        {
            Bonus = bonus;
        }
        public override void Display()
        {
            base.Display(); // Call base method to show name and salary
            Console.WriteLine($"Bonus: {Bonus}");
        }
    }
}
