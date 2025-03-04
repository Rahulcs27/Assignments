using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyBazaar.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public User(string username , string password) 
        {
            UserName = username;
            Password = password;
        }
    }
}
