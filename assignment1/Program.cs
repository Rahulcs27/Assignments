namespace assignment1
{
    internal class program
    {
        static void Main(string[] args)
        {
            //# region Scenario1 Student Management System
            //Console.WriteLine("Enter Student Name");
            //string name = Console.ReadLine();
            //Console.WriteLine("Enter Student Age");
            //int age = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Enter Your Percentage");
            //double percentage = double.Parse(Console.ReadLine());
            //Console.WriteLine("--Student Details--");
            //Console.WriteLine($"Name: {name}\nAge: {age}\nPercentage: {percentage}");
            //#endregion

            //#region Scenario2 Student Management System
            //Console.WriteLine("Enter New Student Name");
            //string newname = Console.ReadLine();
            //retryAge:
            //Console.WriteLine("Enter New Student Age");
            //int newage;
            //bool isAgeValid = int.TryParse(Console.ReadLine(), out newage);
            //if (!isAgeValid)
            //{
            //    Console.WriteLine("Invalid Age");
            //    return;
            //    goto retryAge;
            //}
            //Console.WriteLine("\n--- Student Details ---");
            //Console.WriteLine($"Name: {newname}");
            //Console.WriteLine($"Age: {newage}");
            //#endregion

            //#region Scenario3 Student Management System
            //Console.WriteLine("Enter Student Name");
            //string studentName = Console.ReadLine();
            //retry:
            //Console.WriteLine("Enter Student Age");
            //int age1;
            //bool isValid = int.TryParse(Console.ReadLine(), out age1);
            //if (!isValid)
            //{
            //    Console.WriteLine("Invalid Age");
            //    goto retry;
            //}
            //retryMail:
            //Console.WriteLine("Please enter your e-mail: ");
            //string email = Console.ReadLine();
            //if (string.IsNullOrEmpty(email))
            //{
            //    Console.WriteLine("Please enter the e-mail.");
            //    goto retryMail;
            //}
            //else
            //{
            //    Console.WriteLine($"Your e-mail is: {email}.");
            //}
            //#endregion

            #region Hospital management system

            Console.WriteLine("Enter Patient Name:");
            string patientName = Console.ReadLine();

            bool isDate;
        retryDate:
            DateTime dischargeDate;
            Console.WriteLine("Please enter discharge date of patient (MM-DD-YYYY): ");
            isDate = DateTime.TryParse(Console.ReadLine(), out dischargeDate);
            if (isDate == true)
            {
                Console.WriteLine($"Patient Name : {patientName}\tDischarge Date: {dischargeDate.Day} Month: {dischargeDate.Month} Year: {dischargeDate.Year}");
            }
            else
            {
                Console.WriteLine("Not Discharged.");
                goto retryDate;
            }
            #endregion
        }
    }
}
