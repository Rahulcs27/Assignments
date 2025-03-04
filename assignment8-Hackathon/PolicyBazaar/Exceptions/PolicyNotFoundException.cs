using System;

namespace InsurancePolicyApp.Exceptions
{
    public class PolicyNotFoundException : Exception
    {
        public PolicyNotFoundException(string message) : base(message) { }
    }
}
