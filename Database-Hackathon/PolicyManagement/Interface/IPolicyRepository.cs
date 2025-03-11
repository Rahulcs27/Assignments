using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicyManagement.Models;

namespace PolicyManagement.Interface
{
    public interface IPolicyRepository
    {
        int AddPolicy(); 
        List<Policy> ViewPolicy();  
        Policy SearchPolicyById(int id);  
        bool UpdatePolicy(int id);  
        bool DeletePolicy(int id);
    }
}
