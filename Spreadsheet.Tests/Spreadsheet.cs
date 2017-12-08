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
         //   private readonly string[] lines = { @" 1 | 3 | 45 | 3.4 |", @" 2 | 2 | -213 | 12;" };
            //Environment.NewLine

            Spreadsheet sut = new Spreadsheet();
            Assert.DoesNotThrow(() => {
                sut.loadLine(0, " 1 | 3 | 45 | 3.4 |");  sut.loadLine(1, " 2 | 2 | -213 | 12;");
            });
        }

        [Test]
        public void Simple_Input_2()
        {
            // private readonly string[] lines = { " 1 | 3 | 45 |", " A1 + A2 + 4 | 1 | B1 + 3;" };

            Spreadsheet sut = new Spreadsheet();
            Assert.DoesNotThrow(() => {
                sut.loadLine(0, " 1 | 3 | 45 |");  sut.loadLine(1, " A1 + A2 + 4 | 1 | B1 + 3;");
            });
        }

       /* [Test]
        public void Expression()
        {
            // private readonly string expression = { ""A1+A3* B2"" };

            Spreadsheet sut = new Spreadsheet();
            Assert.That(sut.ToString().Length > 0);
        }*/
    }
}