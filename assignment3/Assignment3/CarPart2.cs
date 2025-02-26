using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{
    public partial class Car
    {
        // Method to Display Car Details
        public void DisplayCarDetails()
        {
            Console.WriteLine($"Presenting Car Information: Car ID: {CarID}, Brand: {Brand}, Model: {Model}, Year: {Year}, Price: Rs{Price}");
        }
    }
}
