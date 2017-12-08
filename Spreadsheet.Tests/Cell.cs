using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PC.Spreadsheet.Tests
{
    [TestFixture]
    class CellTestFixture
    {
        [TestCase(" A2 +   ", true)]
        [TestCase("   2 + ", false)]
        public void Check_reference(string formula, bool shouldDetectReference)
        {
            Cell sut = new Cell();
            sut.Formula = formula;
            Assert.That(sut.HasReferences() == shouldDetectReference);
        }

        [TestCase(" 1+   2", true)]
        [TestCase("   A2 ", false)]
        public void Check_expression(string formula, bool shouldDetectExpression)
        {
            Cell sut = new Cell();
            sut.Formula = formula;
            Assert.That(sut.IsExpression() == shouldDetectExpression);
        }
    }
}
