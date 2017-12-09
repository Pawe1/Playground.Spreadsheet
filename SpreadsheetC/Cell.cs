using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PC.Spreadsheet
{
    public struct Cell
    {
        private string _formula;
        //private double? _value;

        public string Formula { get { return _formula; } set { _formula = value.Trim().ToUpper().Replace("  ", " ");  eval(); } }
        public double? Value { get; set; }

        public bool HasReferences()
        {
            Regex regex = new Regex(@"[A-Z][0-9]");
            return regex.Match(_formula).Success;
        }

        public bool IsExpression()
        {
            Regex regex = new Regex(@".[+\-*\/]" );
            return regex.Match(_formula).Success;
        }

        public void Clear()
        {
            _formula = "";
            Value = null;
        }

        private void eval()
        {
            if (!IsExpression() && !HasReferences() && _formula.Length > 0)
            {
                Value = double.Parse(_formula.Replace(".", ","));
            }
            else
                Value = null;
        }        
    }
}
