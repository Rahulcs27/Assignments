using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyBazaar.Models
{
    public enum PolicyType
    {
        Life,
        Health,
        Vehicle,
        Property
    }
    public class Policy
    {
        public int PolicyId { get; set; }
        public string PolicyHolderName { get; set; }
        public PolicyType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Policy(int policyId, string policyHolderName, PolicyType type, DateTime startDate, DateTime endDate)
        {
            PolicyId = policyId;
            PolicyHolderName = policyHolderName;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsActive()
        {
            return DateTime.Now >= StartDate && DateTime.Now<= EndDate;
        }
        public override string ToString()
        {
            return $"ID: {PolicyId}, Name: {PolicyHolderName}, Type: {Type}, " +
                   $"Start: {StartDate.ToShortDateString()}, End: {EndDate.ToShortDateString()}, " +
                   $"Active: {IsActive()}";
        }

    }
}
