using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyBazaar.Interfaces
{
    internal interface IPolicyRepository
    {
        void AddPolicy();
        void ViewPolicy();
        void SearchPolicyById();
        void UpdatePolicy();
        void DeletePolicy();
        void ViewActivePolicy();
    }
}
