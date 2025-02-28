using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Model
{
    class BankAccount
    {
        public string AccountNumber { get; }
        public string AccountHolderName { get; }

        public BankAccount(string accountNumber, string accountHolderName)
        {
            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
        }
    }
}
