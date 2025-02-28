using task2.Model;

namespace task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating a list of policies
            List<VehicleInsurance> policies = new List<VehicleInsurance>
            {
                new TwoWheeler("Rahul Suthar", 50000),
                new FourWheelers("Sakshi Bahirat", 800000),
                new Commercial("Vaishnavi Bhambure", 1500000)
            };

            // Displaying details
            Console.WriteLine("Vehicle Insurance Policies:");
            foreach (var policy in policies)
            {
                policy.DisplayPolicyDetails();
            }
        }
    }
}
