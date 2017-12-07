using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PC.Spreadsheet;
 
namespace PC.Spreadsheet.Tests
{
    [TestFixture]
    public class SpreadsheetTestFixture
    {
        [Test]
        public void Dummy()
        {
            Spreadsheet sut = new Spreadsheet();
            Assert.That(sut.ToString().Length > 0);
        }
    }
}