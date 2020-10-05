using System;
using System.Collections.Generic;
using System.Text;
using MyBanker_Library.Concrete;
using Xunit;

namespace MyBanker_Library.Tests
{
    
    public class PersonTest
    {
     
        [Fact]
        public void Person_ShouldConstructJustFine()
        {
            var person = new Person("Mogens", "Jensen", 15);
        }

    }
}
