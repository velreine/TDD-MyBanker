namespace MyBanker_Library.Interfaces
{
    public interface IMonthlyLimit
    {
        decimal MonthlyUsage { get; }
        decimal MonthlyLimit { get; }

        void ResetMonthlyUsage();
    }
}