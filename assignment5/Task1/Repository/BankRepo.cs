using System;
using System.Collections.Generic;
using Task1.Exceptions;
using Task1.Model;

namespace Task1.Repository
{
    internal class BankRepo
    {
        private readonly List<BankAccount> accounts;

        // Constructor to initialize accounts
        public BankRepo()
        {
            accounts = new List<BankAccount>
            {
                new BankAccount("123456", "Rahul Suthar"),
                new BankAccount("654321", "Sakshi Bahirat"),
                new BankAccount("987654", "Vaishnavi Bhambure")
            };
        }

        // Method to get account details
        private BankAccount FindBankAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Account number cannot be empty.");
            }

            return accounts.Find(acc => acc.AccountNumber == accountNumber);
        }

        public void GetBankAcc()
        {
            try
            {
                Console.Write("Enter Beneficiary Account Number: ");
                string accountNumber = Console.ReadLine();

                // Get the account details
                var beneficiary = FindBankAccount(accountNumber);

                if (beneficiary == null)
                {
                    throw new InvalidAccountException($"Beneficiary account {accountNumber} not found.");
                }

                Console.WriteLine($"Transfer successful to: {beneficiary.AccountHolderName}");
            }
            catch (InvalidAccountException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Transaction Process Completed.");
            }
        }
    }
}
