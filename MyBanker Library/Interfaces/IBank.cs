using System.Collections.Generic;

namespace MyBanker_Library.Interfaces
{
    public interface IBank
    {
        string Identifier { get; }
        ICollection<ICustomer> Customers { get; }
        
        IAddress Address { get; }
    }
}