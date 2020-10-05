using System;
using System.Collections.Generic;
using System.Text;
using MyBanker_Library.Concrete.Cards;
using MyBanker_Library.Abstracts;
using MyBanker_Library.Concrete;
using MyBanker_Library.Interfaces;
using System.Linq;
using System.Reflection;
using Xunit;


namespace MyBanker_Library.Tests
{
    public class CardTest
    {

        [Theory]
        [InlineData(0, 100, 100)]
        public void AllCards_ShouldDepositProperly(decimal previousBalance, decimal toDeposit, decimal expectedBalanceAfter)
        {

            var owner = new Customer(new Address("Ringsted", "Nykøbing F.", "Lillavej 1"), "Nicky", "Hansen", 25);
            //var underlying_account = new BankAccount("100", owner);

            var type = typeof(AbstractCard);
            var derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p)
                && p.IsClass
                && !p.IsInterface 
                && !p.IsAbstract
                );

            var allCardsBehavedProperly = true;

            foreach(var derived in derivedTypes)
            {
                var temporaryAccount = new BankAccount("100", owner);
                ICard instance = (ICard)Activator.CreateInstance(derived, temporaryAccount);
                instance.Deposit(previousBalance);
                instance.Deposit(toDeposit);

                if(instance.Account.Balance != expectedBalanceAfter)
                {
                    allCardsBehavedProperly = false;
                }

            }

            Assert.True(allCardsBehavedProperly);
        }

    }
}
