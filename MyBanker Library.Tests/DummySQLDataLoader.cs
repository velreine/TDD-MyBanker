using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Autofac;
using Autofac.Extras.Moq;
using MyBanker_Library.Interfaces;
using MyBanker_Library.Concrete;

namespace MyBanker_Library.Tests
{
    public class DummySQLDataLoader
    {

        [Fact]
        public void DummySQLDataLoader_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDummySQLDataLoader>()
                    .Setup(x => x.LoadData<Customer>("select * from customer"))
                    .Returns(GetDummyDataCustomers());

                var dummy = mock.Create<Concrete.DummySQLDataLoader>();

                var expected = GetDummyDataCustomers();
                var actual = dummy.LoadData<Customer>("select * from customer");


                // If the actual variable dont return the same list of customers as GetDummyDataCustomers()
                // Then the mock is not working.
                // Mocks are good for testing business logic without having to rely on external systems.
                // Unit Tests should not rely on any external things, if you need to test that you should write Integration Tests.

                Assert.True(actual != null);

                // Cant use normal Assert.Equal as the reference to the list is not the same.
                Assert.Equal(expected.Count, actual.Count);

                

            }

            
        }


        private List<Customer> GetDummyDataCustomers()
        {
            return new List<Customer>()
            {
                new Customer(new Address("A", "B", "C"), "Nicky", "Hansen", 25),
                new Customer(new Address("A", "B", "C"), "Lukas", "Zebbelin", 21),
                new Customer(new Address("A", "B", "C"), "Nicklas", "Pedersen", 19),
            };
        }

    }
}
