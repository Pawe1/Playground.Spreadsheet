using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PC.Spreadsheet.Tests
{
    [TestFixture]
    public class ParserTestFixture
    {
        [Test]
        public void Simple_parsing()
        {
            Assert.That(Parser.Parse("1+2") == 3);
        }
    }
}