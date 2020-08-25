using System;
using System.Globalization;
using System.Text;
using MyBanker_Library.Abstracts;
using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete.Cards
{
    public sealed class VisaDankort : AbstractCard, IMonthlyLimit, ICreditCard
    {
        protected override string[] Prefixes { get; } = {"4"};
        public override int AgeLimit { get; } = 18;
        public decimal MonthlyUsage { get; private set; } = 0;
        public decimal MonthlyLimit { get; } = 25000;
        public decimal CreditUsage { get; private set; } = 0;
        public decimal CreditLimit { get; } = 20000;
        

        public VisaDankort(IAccount account, DateTime expirationDateTime) : base(account, expirationDateTime) { }

        public VisaDankort(IAccount account) : base(account) { }
        
        

        public override decimal Withdraw(decimal money)
        {
            if (money + MonthlyUsage <= MonthlyLimit)
            {

                decimal withdrawn = this.Account.Withdraw(money);
                if (withdrawn == 0)
                {
                    // Try to get credit instead.
                    if (money - this.Account.Balance + CreditUsage <= CreditLimit)
                    {
                        // The Credit we need.
                        decimal creditDelta = money - this.Account.Balance;
                        
                        // Withdraw money in the account.
                        this.Account.Withdraw(this.Account.Balance);

                        // The money we didn't have ourselves will be added to credit.
                        CreditUsage += creditDelta;

                        // The full amount will count towards our monthly use.
                        MonthlyUsage += money;
                        return money;
                    }
                }
                MonthlyUsage += withdrawn;
                return withdrawn;
            }

            return 0;
        }
        
        public void ResetMonthlyUsage()
        {
            this.MonthlyUsage = 0;
        }
        
        public void PayOutstandingCredit()
        {
            // Payment processing etc...
            this.CreditUsage = 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(this.GetType().ToString());
            sb.AppendLine(Account.ToString());
            sb.AppendLine($"CardNumber: {CardNumber}");
            sb.AppendLine($"Age Limit {this.AgeLimit}");
            sb.AppendLine($"Credit Limit: {CreditLimit}");
            sb.AppendLine($"Credit Use: {this.CreditUsage}");
            sb.AppendLine($"International card: {this.CanBeUsedInternationally}");
            sb.AppendLine($"Expires: {Expires.ToLocalTime().ToString(CultureInfo.CurrentCulture)}");

            return sb.ToString();
        }
    }
}