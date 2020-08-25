using System;
using MyBanker_Library.Abstracts;
using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete.Cards
{
    public sealed class VisaElectron : AbstractCard, IMonthlyLimit
    {
        protected override string[] Prefixes { get; } = {"4026", "417500", "4508", "4844", "4913", "4917"};
        public decimal MonthlyUsage { get; private set; } = 0;
        public decimal MonthlyLimit { get; } = 10000;
        
        public VisaElectron(IAccount account, DateTime expirationDateTime) : base(account, expirationDateTime)
        {
        }

        public VisaElectron(IAccount account) : base(account)
        {
        }

        public override decimal Withdraw(decimal money)
        {
            if (money + MonthlyUsage <= MonthlyLimit)
            {
                decimal withdrawn = this.Account.Withdraw(money);
                MonthlyUsage += withdrawn;
                return withdrawn;
            }

            return 0;
        }

        public void ResetMonthlyUsage()
        {
            this.MonthlyUsage = 0;
        }
    }
}