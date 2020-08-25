using System;

namespace MyBanker_Library.Interfaces
{
    public interface ICard
    {
        IAccount Account { get; }
        int AgeLimit { get; }
        string CardNumber { get; }
        
        string PinCode { get; }
        
        DateTime Expires { get; }
        bool CanBeUsedInternationally { get; }
        
        string GenerateCardNumber();

        void Deposit(decimal money);
        decimal Withdraw(decimal money);
    }
}