namespace MyBanker_Library.Interfaces
{
    public interface IAddress
    {
        string City { get; }
        string Country { get; }
        string Street { get; }
    }
}