using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.Spreadsheet
{
    static class Tokenizer
    {
        public static IEnumerable<string> Execute(string expression)
        {
            return expression.Split(' ').ToList();
        }
    }   
}
