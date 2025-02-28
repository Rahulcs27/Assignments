using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.Model
{
    public class TwoWheeler : VehicleInsurance
    {
        public TwoWheeler(string ownerName, double vehicleValue) : base("Two-Wheeler", ownerName, vehicleValue) { }

        public override double CalculatePremium()
        {
            return VehicleValue * 0.02; // 2% of vehicle value
        }
    }
}
