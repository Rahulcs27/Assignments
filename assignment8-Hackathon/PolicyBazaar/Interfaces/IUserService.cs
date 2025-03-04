using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyBazaar.Interfaces
{
    internal interface IUserService
    {
        void RegisterUser();
        bool authenticateUser(string username, string password);
    }
}
