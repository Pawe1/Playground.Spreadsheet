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
        public void Parser_Input()
        {
            Assert.DoesNotThrow(() => {
                Parser.Evaluate("-4 + ( 11 - ( 3 * 2 ) ) / 2");
            });
        }

        [TestCase("1 + 2", 3)]
        [TestCase("1 - 2", -1)]
        [TestCase("2 * 2", 4)]
        [TestCase("3 / 4", 0.75)]
        [TestCase("-1 + 2", 1)]
        [TestCase("0,1 + 0,2", 0.3)]
        [TestCase("-0,1 / 0,01", -10)]
        [TestCase("  1 +    6 ", 7)]
        public void Simple_parsing(string expression, double expectedResult)
        {
            double result = Parser.Evaluate(expression);
            Assert.That(result == expectedResult);
        }

        [Test]
        public void Parsing_with_priorities()
        {
            double result = Parser.Evaluate("1 + 2 * 3");
            Assert.That(result == 7);
        }

        [Test]
        public void Parenthesis()
        {
            double result = Parser.Evaluate("( 1 +  2 ) * 3");
            Assert.That(result == 9);
        }

        [Test]
        public void Advanced_parsing()
        {
            double result = Parser.Evaluate("-4,1 + ( ( ( 5 - 3 ) * 3 ) / 2 ) * 3");
            Assert.That(result == 4.9);
        }
    }
}