using System.Collections.Generic;
using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete
{
    public class Customer : Person , ICustomer
    {
        public IAddress Address { get; private set; }
        public ICollection<ICard> Cards { get; private set; } = new List<ICard>();
        public ICollection<IAccount> Accounts { get; private set; } = new List<IAccount>();
        
        public Customer(IAddress address, string firstName, string lastName, int age) : base(firstName, lastName, age)
        {
            this.Address = address;
        }
        
        public bool TryAddCard(ICard card)
        {
            if (card.Account.Owner != this)
                return false;

            if (!this.Cards.Contains(card) )
            {
                this.Cards.Add(card);
                return true;
            }
            
            

            return true;
        }

        public void RemoveCard(ICard card)
        {

            if (Cards.Contains(card))
            {
                Cards.Remove(card);
            }
        }


        public bool TryAddAccount(IAccount account)
        {
            if (account.Owner != this)
                return false;

            if (!this.Accounts.Contains(account))
            {
                this.Accounts.Add(account);
                return true;
            }

            return true;
        }

        public void RemoveAccount(IAccount account)
        {
            if (Accounts.Contains(account))
            {
                Accounts.Remove(account);
            }
        }
        
        
    }
}