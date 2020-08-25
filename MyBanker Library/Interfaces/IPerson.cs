namespace MyBanker_Library.Interfaces
{
    public interface IPerson
    {
        string FirstName { get; }
        string LastName { get; }
        
        int Age { get; }
    }
}