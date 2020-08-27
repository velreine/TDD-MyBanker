using MyBanker_Library.Concrete;
using MyBanker_Library.Concrete.Cards;
using System;
using System.IO;
using System.Reflection.Metadata;
using Xunit;

namespace MyBanker_Library.Tests
{
    public class ATMTest
    {
        [Fact]
        public void InsertCard_PassNullAsCardShouldMaintainProperState()
        {
            // Arrange
            const string input = "";
            var atm = new ATM(new StringReader(input), TextWriter.Null);

            // Act
            atm.InsertCard(null);

            // Assert
            Assert.True(atm.State == ATMState.Ready);
        }

        [Fact]
        public void IsCardInserted_ShouldReturnCorrectValueWhenNotInserted()
        {
            string input = "1234" + Environment.NewLine + "0" + Environment.NewLine + "0";
            var atm = new ATM(new StringReader(input), TextWriter.Null);
            var account = new BankAccount("1", new Customer(null, "Jens", "Petersen", 59));
            var card = new MasterCard(account, DateTime.MaxValue, "1234");

            atm.InsertCard(card);

            Assert.True(atm.IsCardInserted());
        }

        [Fact]
        public void InsertCard_ShouldThrowWhenThereIsAlreadyACardInserted()
        {
            const string input = "";
            var atm = new ATM(new StringReader(input), TextWriter.Null);

            var card = new MasterCard(null);
            var card2 = new MasterCard(null);

            atm.InsertCard(card);

            Assert.Throws<Exception>(() =>
            {
                atm.InsertCard(card2);
            });

        }

        [Theory]
        [InlineData(0, int.MaxValue, 0, 0)]
        [InlineData(0, 100, 100, -100)]
        [InlineData(2000, 500, 500, 1500)]
        public void RequestMoney_ShouldReturnWithdrawnMoney(decimal balance, decimal toWithdraw, decimal expectedWithdrawn, decimal expectedBalanceAfter)
        {
            const string input = "1234";
            var atm = new ATM(new StringReader(input), TextWriter.Null);
            var owner = new Customer(new Address("A", "b", "c"), "Hans", "Hansen", 29);
            var account = new BankAccount("1", owner);
            account.Deposit(balance);

            var card = new MasterCard(account, DateTime.MaxValue, "1234");

            atm.InsertCard(card);
            decimal actualWithdrawn = atm.RequestMoney(toWithdraw);

            Assert.Equal(expectedWithdrawn, actualWithdrawn);
            Assert.Equal(expectedBalanceAfter, card.Account.Balance);




        }

    }
}