using System.Collections.Generic;

namespace MyBanker_Library.Interfaces
{
    public interface ICustomer : IPerson
    {
        ICollection<ICard> Cards { get; }
        ICollection<IAccount> Accounts { get; }
        
        IAddress Address { get;  }

        bool TryAddCard(ICard card);
        void RemoveCard(ICard card);

        bool TryAddAccount(IAccount account);
        void RemoveAccount(IAccount account);

    }
}