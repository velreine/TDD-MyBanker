namespace MyBanker_Library.Interfaces
{
    public interface IAccount
    {
        string AccountNumber { get; }
        decimal Balance { get; }
        IPerson Owner { get; }
        void Deposit(decimal money);
        decimal Withdraw(decimal money);
    }
}