using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyManagement.Interface
{
    public interface IUserService
    {
        void RegisterUser();
        bool AuthenticateUser(string username, string password);
    }
}
