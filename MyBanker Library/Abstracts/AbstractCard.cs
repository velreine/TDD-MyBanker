using System;
using System.Globalization;
using System.Text;
using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Abstracts
{
    public abstract class AbstractCard : ICard
    {
        public string CardNumber { get; }
        public IAccount Account { get; }
        public DateTime Expires { get; }

        public string PinCode { get; }
        
        public virtual int AgeLimit { get; } = 0;
        public virtual bool CanBeUsedInternationally { get; } = false;
        
        
        protected abstract string[] Prefixes { get; }
        protected virtual int CardNumberLength { get; } = 16;


        protected AbstractCard(IAccount account, DateTime expirationDateTime, string pinCode = null)
        {
            this.Account = account ?? throw new ArgumentException("Account cannot be NULL!", "account");
            this.Expires = expirationDateTime;
            this.PinCode = pinCode ?? GenerateRandomPinCode();
            this.CardNumber = GenerateCardNumber();
        }

        private string GenerateRandomPinCode()
        {
            Random r = new Random();
            return r.Next(1000, 9999).ToString();
        }

        protected AbstractCard(IAccount account)
        {
            this.Account = account;
            this.Expires = DateTime.MaxValue;
            this.PinCode = GenerateRandomPinCode();
            this.CardNumber = GenerateCardNumber();
        }

        public virtual void Deposit(decimal money)
        {
            this.Account.Deposit(money);
        }
        
        public virtual decimal Withdraw(decimal money)
        {
            return this.Account.Withdraw(money);
        }
        
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(this.GetType().ToString());
            sb.AppendLine(Account.ToString());
            sb.AppendLine($"CardNumber: {CardNumber}");
            sb.AppendLine($"Age Limit {this.AgeLimit}");
            sb.AppendLine($"International card: {this.CanBeUsedInternationally}");
            sb.AppendLine($"Expires: {Expires.ToLocalTime().ToString(CultureInfo.CurrentCulture)}");

            return sb.ToString();
        }
        
        
        // Template method to GenerateCardNumber
        // Can be overwritten.
        public virtual string GenerateCardNumber()
        {
            StringBuilder sb = new StringBuilder();
            Random rng = new Random();

            if (this.Prefixes.Length == 0)
            {
                throw new Exception("Cannot generate a card number for a card type without prefixes.");
            }
            
            string selectedPrefix = Prefixes[rng.Next(0, Prefixes.Length)];

            sb.Append(selectedPrefix);
            sb.Append(" ");

            for (int i = 0; i < CardNumberLength - selectedPrefix.Length; i++)
            {
                sb.Append(rng.Next(0, 9));
            }

            return sb.ToString();
        }

    }
}