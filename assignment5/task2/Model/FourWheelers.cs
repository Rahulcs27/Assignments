using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.Model
{
    public class FourWheelers : VehicleInsurance
    {
        public FourWheelers(string ownerName, double vehicleValue)  : base("Four-Wheeler", ownerName, vehicleValue) { }

        public override double CalculatePremium()
        {
            return VehicleValue * 0.05; // 5% of vehicle value
        }
    }
}
