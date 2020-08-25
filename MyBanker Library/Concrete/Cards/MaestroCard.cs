using System;
using MyBanker_Library.Abstracts;
using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete.Cards
{
    public sealed class MaestroCard : AbstractCard
    {
        protected override string[] Prefixes { get; } = { "5018", "5020", "5038", "5893", "6304", "6759", "6761", "6762", "6763" };
        protected override int CardNumberLength { get; } = 19;
        public override int AgeLimit { get; } = 18;
        public override bool CanBeUsedInternationally { get; } = true;

        public MaestroCard(IAccount account, DateTime expirationDateTime) : base(account, expirationDateTime)
        {
        }

        public MaestroCard(IAccount account) : base(account)
        {
        }

        
    }
}