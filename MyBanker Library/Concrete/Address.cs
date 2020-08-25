using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete
{
    public class Address : IAddress
    {
        public string City { get; private set; }
        public string Country { get; private set; }

        public string Street { get; private set; }

        public Address(string city, string country, string street)
        {
            City = city;
            Country = country;
            Street = street;
        }
        
    }
}