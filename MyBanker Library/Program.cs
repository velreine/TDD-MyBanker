using MyBanker_Library.Concrete;
using MyBanker_Library.Concrete.Cards;
using System;

namespace MyBanker_Library
{
    class Program
    {
        static void Main(string[] args)
        {

            var peter = new Customer(new Address("Ringsted", "Denmark", "Ahorn Allé 3"), "Peter", "Jensen", 38);
            var account = new BankAccount("1", peter);
            var card = new MasterCard(account, DateTime.Now + TimeSpan.FromDays(365), "1234");

            var atm = new ATM(Console.In, Console.Out);

            while(true)
            {
                atm.InsertCard(card);
            }


        
        }
    }
}