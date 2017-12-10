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
            Assert.That(sut.cells[0, 0].Value == 1);
            Assert.That(sut.cells[1, 0].Value == 3);
            Assert.That(sut.cells[2, 0].Value == 45);
            Assert.That(sut.cells[3, 0].Value == 3.4);
            Assert.That(sut.cells[0, 1].Value == 2);
            Assert.That(sut.cells[1, 1].Value == 2);
            Assert.That(sut.cells[2, 1].Value == -213);
            Assert.That(sut.cells[3, 1].Value == 12);
        }

        [Test]
        public void Simple_Input_2()
        {
             string[] lines = {
                " 1 | 3 | 45 |",
                " A1 + A2 + 4 | 1 | B1 + 3;" };

            Spreadsheet sut = new Spreadsheet();
            Assert.That(sut.cells[0, 0].Value == 1);
            Assert.That(sut.cells[1, 0].Value == 3);
            Assert.That(sut.cells[2, 0].Value == 45);
            Assert.That(sut.cells[0, 1].Value == 8);
            Assert.That(sut.cells[1, 1].Value == 1);
            Assert.That(sut.cells[2, 1].Value == 11);
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
            sut.Load(lines);
            Assert.That(sut.cells[0, 2].Value == 16);
        }
    }
}