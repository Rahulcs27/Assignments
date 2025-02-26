using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{
    public partial class Car
    {
        // Private fields (Encapsulation)
        private int carID;
        private string brand;
        private string model;
        private int year;
        private double price;

        // Public properties
        public int CarID { get { return carID; } set { carID = value; } }
        public string Brand { get { return brand; } set { brand = value; } }
        public string Model { get { return model; } set { model = value; } }
        public int Year { get { return year; } set { year = value; } }
        public double Price { get { return price; } set { price = value; } }

        // Parameterized Constructor (No 'this' keyword)
        public Car(int id, string carBrand, string carModel, int carYear, double carPrice)
        {
            Console.WriteLine("Receiving Car Information...");
            carID = id;
            brand = carBrand;
            model = carModel;
            year = carYear;
            price = carPrice;
        }

        // Default Constructor (Overloaded)
        public Car()
        {
            Console.WriteLine("Receiving Default Car Information...");
            carID = 0;
            brand = "Unknown";
            model = "Unknown";
            year = 2000;
            price = 0.0;
        }
    }
}
