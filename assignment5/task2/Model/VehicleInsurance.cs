using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.Model
{
    public abstract class VehicleInsurance
    {
        public string VehicleType { get; set; }
        public string OwnerName { get; set; }
        public double VehicleValue { get; set; }

        // Constructor
        public VehicleInsurance(string vehicleType, string ownerName, double vehicleValue)
        {
            VehicleType = vehicleType;
            OwnerName = ownerName;
            VehicleValue = vehicleValue;
        }

        // Abstract method for premium calculation
        public abstract double CalculatePremium();

        // Display details
        public void DisplayPolicyDetails()
        {
            Console.WriteLine($"Owner: {OwnerName}, Vehicle: {VehicleType}, Premium RS: {CalculatePremium()}");
        }
    }
}
