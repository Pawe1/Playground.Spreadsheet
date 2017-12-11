using System;
using System.Linq;

namespace PC.Spreadsheet
{
    static class ReferenceResolver
    {
        static coordinates referenceToCoordinates(string value)
        {
            if (value.Length != 2)
                throw new Exception();

            char xPart = value[0];
            char yPart = value[1];

            int ix = char.ToUpper(xPart) - 64;
            int iy = int.Parse(yPart.ToString());

            ix--;
            iy--;

            return new coordinates { x = (byte)ix, y = (byte)iy };
        }

          /*      public IEnumerable<string> extractReferences()
        {
            List<string> result = new List<string>();

            Regex regex = new Regex(@"[A-Z][0-9]");
            var match = regex.Match(_formula);
            if (match.Success)
            {
                result.AddRange(regex.Matches(_formula).Cast<Match>()
                    .Select(match_ => match_.Value));
            }
            return result;
        }*/

        public static string Resolve(string formula, Func<coordinates, string> lookupFunction)
        {
            var tokens = Classifier.Classiffy(formula);

            var processedTokens = tokens.Select(
                t =>
                {
                    if (t.isReference)
                    {
                        var coordinates = referenceToCoordinates(t.value);
                        var solved_value = lookupFunction(coordinates);                    
                   //     if (!Classifier.ContainsReferences(solved_value))
                        {
                            return solved_value;
                        }
                       // return t.value;
                    }
                    else
                    {
                      return t.value;
                    }
                 }

                ).ToArray();
            return String.Join(" ", processedTokens);
        }
    }
}
