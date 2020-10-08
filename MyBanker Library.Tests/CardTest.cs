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
            
            // Grab concrete classes that extend AbstractCard.
            IEnumerable<Type> derivedTypes = GetConcreteDerivativesOf(typeof(AbstractCard));

            // We change this if one card does not behave properly.
            var allCardsBehavedProperly = true;

            foreach (var derived in derivedTypes)
            {
                var temporaryAccount = new BankAccount("100", owner);
                ICard instance = (ICard)Activator.CreateInstance(derived, temporaryAccount);
                instance.Deposit(previousBalance);
                instance.Deposit(toDeposit);

                if (instance.Account.Balance != expectedBalanceAfter)
                {
                    allCardsBehavedProperly = false;
                }

            }
            
            Assert.True(allCardsBehavedProperly);
        }

        private static IEnumerable<Type> GetConcreteDerivativesOf(Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(assembly => assembly.GetTypes())
                            .Where(x => type.IsAssignableFrom(x)
                            && x.IsClass
                            && !x.IsInterface
                            && !x.IsAbstract
                            );
        }

        [Theory]
        [InlineData(0, 100, -100)]
        public void AllCards_ShouldWithdrawProperly(decimal previousBalance, decimal toWithdraw, decimal expectedBalanceAfter)
        {
            var owner = new Customer(new Address("Ringsted", "Nykøbing F", "Lillavej 1"), "Nicky", "Hansen", 25);

            // Grab concrete classes that extend AbstractCard.
            IEnumerable<Type> derivesTypes = GetConcreteDerivativesOf(typeof(AbstractCard));

            // We change this if one card does not have properly.
            var allCardsBehavedProperly = true;

            foreach(var derived in derivesTypes)
            {
                var temporaryAccount = new BankAccount("100", owner);
                ICard instance = (ICard)Activator.CreateInstance(derived, temporaryAccount);
                instance.Deposit(previousBalance);
                instance.Withdraw(toWithdraw);

                if(instance.Account.Balance != expectedBalanceAfter)
                {
                    allCardsBehavedProperly = false;
                }
            }

            Assert.True(allCardsBehavedProperly);
        }

    }
}
