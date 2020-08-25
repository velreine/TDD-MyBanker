namespace MyBanker_Library.Interfaces
{
    public interface IDailyLimit
    {
        decimal DailyUsage { get; }
        decimal DailyLimit { get; }

        void ResetDailyUsage();
    }
}