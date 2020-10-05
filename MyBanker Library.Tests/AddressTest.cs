using System;
using System.Collections.Generic;
using System.Text;
using MyBanker_Library.Concrete;
using Xunit;

namespace MyBanker_Library.Tests
{
    public class AddressTest
    {
        [Fact]
        public void Address_ShouldConstructJustFine()
        {
            var address = new Address("Nykøbing F.", "Denmark", "Kastanievej 8");
        }

    }
}
