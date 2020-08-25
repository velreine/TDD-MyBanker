using System.Collections.Generic;
using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete
{
    public class Bank : IBank
    {
        public string Identifier { get; }
        public ICollection<ICustomer> Customers { get; } = new List<ICustomer>();
        public IAddress Address { get; }

        public Bank(string identifier, IAddress address)
        {
            Identifier = identifier;
            Address = address;
        }

    }
}