using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PC.Spreadsheet
{
    public struct ClassifiedToken
    {
        public string value;
        public bool isReference;
    }

    public static class Classifier
    {
        static public bool IsExpression(string formula)
        {
            Regex regex = new Regex(@".[+\-*\/]" );
            return regex.Match(formula).Success;
        }

        public static bool ContainsReferences(string formula)
        {
            Regex regex = new Regex(@"[A-Z][0-9]");
            return regex.Match(formula).Success;
        }

        static public IEnumerable<ClassifiedToken> Classiffy(string expression)
        {
            var tokens = Tokenizer.Execute(expression);
            return tokens.Select(t => new ClassifiedToken { value = t, isReference = ContainsReferences(t) } );
        }
    }
}
