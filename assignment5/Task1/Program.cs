using Task1.Exceptions;
using Task1.Model;
using Task1.Repository;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankRepo bankRepo = new BankRepo();
            bankRepo.GetBankAcc();
        }
    }
}
