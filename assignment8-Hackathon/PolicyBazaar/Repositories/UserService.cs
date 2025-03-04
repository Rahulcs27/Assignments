using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using PolicyBazaar.Interfaces;

namespace PolicyBazaar.Repositories
{
    internal class UserService:IUserService
    {
         private Dictionary<string, string> users = new Dictionary<string, string>();
         
         public void RegisterUser()
        {
            Console.WriteLine("Enter New UserName ");
            string username = Console.ReadLine();

            if(users.ContainsKey(username))
            {
                Console.WriteLine("User Already Exists");
                return;
            }
            Console.WriteLine("Enter New Password");
            string password = Console.ReadLine();

            users[username] = password;
            Console.WriteLine("User Registered Successfully");
        }

        public bool authenticateUser(string username, string password) { 
            return users.ContainsKey(username) && users[username] == password;
        }
    }
}
