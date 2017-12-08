using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.Spreadsheet
{
    internal struct Cell
    {
        public string Formula;
        public int Value;

        public bool HasReferences()
        {
            throw new NotImplementedException();
        }            
    }    

    public class Spreadsheet
    {
        const char _cellSeparator = '|';
        const char _spreadsheetTerminator = ';';

        public void load(string lines)
        {
            throw new NotImplementedException();
        }

        public void print()
        {
            Console.WriteLine("Tadaaa!");
        }
    }
}
