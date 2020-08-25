using MyBanker_Library.Abstracts;
using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete
{
    public sealed class BankAccount : AbstractAccount
    {
        public BankAccount(string accountNumber, IPerson owner) : base(accountNumber, 0 , owner)
        {
            
        }

        public override string ToString()
        {
            return $"AccNo: ({AccountNumber}. Owner: {Owner.ToString()}";
        }
    }
}