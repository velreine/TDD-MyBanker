using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete
{
    public class Person : IPerson
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        
        public int Age { get; private set; }

        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public override string ToString()
        {
            return $"{FirstName}, {LastName}";
        }
    }
}