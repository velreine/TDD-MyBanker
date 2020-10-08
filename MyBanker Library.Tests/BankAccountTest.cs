using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Extras.Moq;
using MyBanker_Library.Concrete;
using Xunit;

namespace MyBanker_Library.Tests
{
    public class BankAccountTest
    {

        [Fact]
        public void BankAccount_ShouldConstructJustFine()
        {
            var owner_address = new Address("Nykøbing F.", "Denmark", "Blommegade 54 st th");
            var owner = new Customer(owner_address, "Lars", "Larsen", 78);
            var account = new BankAccount("100200300400", owner);
        }

        [Theory]
        [InlineData(0, int.MaxValue, int.MaxValue, int.MaxValue * - 1)]
        [InlineData(0, 100, 100, -100)]
        [InlineData(2000, 500, 500, 1500)]
        public void BankAccount_ShouldWithdrawMoneyProperly(decimal balance, decimal toWithdraw, decimal expectedWithdrawn, decimal expectedBalanceAfter)
        {
            // NOTE The BankAccount does NOT make sure that X money is available before withdrawing them.
            // That is up the Card/ATM implementation to put that protection/fence up.

            var owner = new Customer(new Address("A", "B", "C"), "Nicky", "Hansen", 25);
            var account = new BankAccount("100", owner);
            account.Deposit(balance);

            var actualWithdrawn = account.Withdraw(toWithdraw);
            var actualBalanceAfter = account.Balance;

            Assert.Equal(expectedWithdrawn, actualWithdrawn);
            Assert.Equal(expectedBalanceAfter, actualBalanceAfter);
        }

        [Theory]
        [InlineData(100, 100, 200)]
        [InlineData(-100, 100, 0)]
        [InlineData(int.MinValue, int.MaxValue, -1)]
        [InlineData(0.50, 0.50, 1.0)]
        public void BankAccount_ShouldDepositMoneyProperly(decimal previousBalance, decimal toDeposit, decimal expectedNewBalance)
        {
            var owner = new Customer(new Address("A", "B", "C"), "Nicky", "Hansen", 25);
            var account = new BankAccount("100", owner);
            account.Deposit(previousBalance);
            account.Deposit(toDeposit);

            var actualNewBalance = account.Balance;

            Assert.Equal(expectedNewBalance, actualNewBalance);
        }



    }
}
