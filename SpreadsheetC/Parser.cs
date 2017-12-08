using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BinaryFunction = System.Func<double, double, double>;  

namespace PC.Spreadsheet
{
    public static class Parser
    {
        private static Dictionary<char, Tuple<byte, BinaryFunction>> functions =  new Dictionary<char, Tuple<byte, BinaryFunction>>()
        {
            { '+',  new Tuple<byte, BinaryFunction>( 2, (x, y) => x + y ) },
            { '-', new Tuple<byte, BinaryFunction>( 2, (x, y) => x - y ) },
            { '*', new Tuple<byte, BinaryFunction>( 1, (x, y) => x * y ) },
            { '/', new Tuple<byte, BinaryFunction>( 1, (x, y) => x / y ) }
        };

        public static int Parse(string x)
        {
           // Stack<double>

           // x.Count(x => x == '&');

            return 0;
        }


    }
}
