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
        public void Simple_Input_1()
        {
             string[] lines = {
                " 1 | 3 | 45 | 3.4 |",
                " 2 | 2 | -213 | 12;" };

            Spreadsheet sut = new Spreadsheet();
            Assert.DoesNotThrow(() => { sut.load(lines); });
        }

        [Test]
        public void Simple_Input_2()
        {
             string[] lines = {
                " 1 | 3 | 45 |",
                " A1 + A2 + 4 | 1 | B1 + 3;" };

            Spreadsheet sut = new Spreadsheet();
            Assert.DoesNotThrow(() => { sut.load(lines); });
        }
        
        [Test]
        public void References()
        {
             string[] lines = {
                " 1 | 2 | 3 |",
                " 4 | 5 | 6 |",
                " A1 + A3 * B2;"
            };

            Spreadsheet sut = new Spreadsheet();
            sut.load(lines);
            Assert.That(sut.cells[0, 2].Value == 16);
        }
    }
}