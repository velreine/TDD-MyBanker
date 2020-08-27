using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Abstracts
{
    public abstract class AbstractAccount : IAccount
    {
        public string AccountNumber { get; protected set; }
        public decimal Balance { get; protected set; }
        public IPerson Owner { get; protected set; }

        protected AbstractAccount(string accountNumber, decimal balance, IPerson owner)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Owner = owner;

        }
        
        public virtual void Deposit(decimal money)
        {
            this.Balance += money;
        }

        public virtual decimal Withdraw(decimal money)
        {
            this.Balance -= money;

            return money;
        }
    }
}