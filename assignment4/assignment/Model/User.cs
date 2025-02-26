using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment.Model
{
     class User
    {

        private static int totalUsers = 0;
        public string userName { get; set; }

        public User(string name)
        { 
            userName = name;
            totalUsers++;
        }
        public static void ShowTotalUsers()
        {
            Console.WriteLine($"Total User LoggedIn {totalUsers}");
        }
    }
}
