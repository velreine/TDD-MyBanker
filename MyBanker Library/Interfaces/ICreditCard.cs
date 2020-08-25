namespace MyBanker_Library.Interfaces
{
    public interface ICreditCard
    {
        decimal CreditUsage { get;  }
        decimal CreditLimit { get;  }
        void PayOutstandingCredit();
    }
}