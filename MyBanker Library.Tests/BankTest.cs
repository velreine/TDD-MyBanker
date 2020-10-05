using System;
using System.Collections.Generic;
using System.Text;
using MyBanker_Library.Concrete;
using Xunit;

namespace MyBanker_Library.Tests
{
    public class BankTest
    {

        [Fact]
        public void Bank_ShouldConstructJustFine()
        {
            // The recommended way in XUnit to test if a method (in this case the constructor)
            // does not throw is just to call it.
            // So believe it or not this is actually a sane Unit Test.
            var address = new Address("Nykøbing F.", "Denmark", "Kastanievej 8");
            var bank = new Bank("madeupidentifier", address);
        }

    }
}
