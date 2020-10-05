using System;
using System.Collections.Generic;
using System.Text;
using MyBanker_Library.Concrete;
using MyBanker_Library.Concrete.Cards;
using MyBanker_Library.Interfaces;
using Xunit;

namespace MyBanker_Library.Tests
{
    public class CustomerTest
    {
        [Fact]
        public void Customer_ShouldConstructJustFine()
        {
            var address = new Address("Ringsted", "Denmark", "Ahorn Allé 77");
            var customer = new Customer(address, "John", "Erik", 22);
        }

        [Fact]
        public void Customer_TryAddCardShouldReturnFalseWhenCustomerIsNotOwner()
        {
            // Create 2 customers.
            var john = new Customer(new Address("Vordingborg", "Denmark", "Kirsebærvej 12"), "John", "Mogens", 44);
            var peter = new Customer(new Address("Nørre Alslev", "Denmark", "Hammervej 4"), "Peter", "Hansen", 38);

            // Create a bank account for each.
            var john_account = new BankAccount("10000", john);
            var peter_account = new BankAccount("20000", peter);

            // Create a card for each.
            var john_card = new MasterCard(john_account);
            var peter_card = new MaestroCard(peter_account);

            // Now try to add a card to a person whom that cards connected account does not belong,
            // It should return false.
            var actual = peter.TryAddCard(john_card);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Customer_TryAddCardShouldReturnTrueWhenCustomerIsOwner()
        {
            // Create new Customer, Account and Card.
            var john = new Customer(new Address("Vordingborg", "Denmark", "Kirsebærvej 12"), "John", "Mogens", 44);
            var john_account = new BankAccount("10000", john);
            var john_card = new MasterCard(john_account);

            var actual = john.TryAddCard(john_card);
            var expected = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Customer_RemoveCardShouldRemoveCardProperly()
        {

            var owner = new Customer(new Address("A", "B", "C"), "Nicky", "Hansen", 14);
            var account = new BankAccount("100200", owner);

            var somecard = new VisaDankort(account);
            var someothercard = new VisaElectron(account);

            List<ICard> cards = new List<ICard>
            {
                somecard,
                someothercard,
                new MaestroCard(account), 
            };

            foreach(ICard c in cards)
            {
                owner.TryAddCard(c);
            }

            owner.RemoveCard(somecard);

            var actual = owner.Cards.Contains(somecard);
            var expected = false;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Customer_TryAddAccountShouldReturnFalseWhenCustomerIsNotOwner()
        {
            var john = new Customer(new Address("A", "B", "C"), "John", "Black", 35);
            var someOtherCustomer = new Customer(new Address("B", "C", "D"), "Other", "Person", 99);
            var someOthersAccount = new BankAccount("100099", someOtherCustomer);

            var actual = john.TryAddAccount(someOthersAccount);
            var expected = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Customer_TryAddAccountShouldReturnTrueWhenCustomerIsOwner()
        {
            var john = new Customer(new Address("A", "B", "C"), "John", "Black", 25);
            var johnAccount = new BankAccount("100", john);

            var actual = john.TryAddAccount(johnAccount);
            var expected = true;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Customer_RemoveAccountShouldRemoveAccountProperly()
        {
            var someCustomer = new Customer(new Address("A", "B", "C"), "Annie", "Bandana", 21);
            var someAccount = new BankAccount("100", someCustomer);
            var someOtherAccount = new BankAccount("200", someCustomer);
            var someOtherOtherAccount = new BankAccount("300", someCustomer);

            List<IAccount> accounts = new List<IAccount>
            {
                someAccount,
                someOtherAccount,
                someOtherOtherAccount
            };

            foreach(IAccount account in accounts)
            {
                someCustomer.TryAddAccount(account);
            }

            someCustomer.RemoveAccount(someOtherOtherAccount);

            var actual = someCustomer.Accounts.Contains(someOtherOtherAccount);
            var expected = false;

            // Should no longer contain that account.
            Assert.Equal(expected, actual);
        }

    }
}
