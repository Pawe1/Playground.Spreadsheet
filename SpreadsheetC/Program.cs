using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.Spreadsheet
{
    class Program
    {
        static void Main(string[] args)
        {
            Spreadsheet s = new Spreadsheet();
            s.loadLine(0, " 1 | 3 | 45 | 3.4 |");
            s.loadLine(1, " 2 | 2 | -213 | 12;");
            s.loadLine(2, " 1 | 3 | 45 |");
            s.loadLine(3, " A1 + A2 + 4 | 1 | B1 + 3;");
            s.print();
            Console.ReadLine();
        }
    }
}
