using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.Model
{
    public class Commercial : VehicleInsurance
    {
        public Commercial(string ownerName, double vehicleValue)  : base("Commercial Vehicle", ownerName, vehicleValue) { }

        public override double CalculatePremium()
        {
            return VehicleValue * 0.08; // 8% of vehicle value
        }
    }
}
