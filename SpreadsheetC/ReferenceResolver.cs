using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        static string Resolve(string formula, Func<coordinates, string> lookupFunction)
        {
            throw new NotImplementedException();
        }
    }
}
