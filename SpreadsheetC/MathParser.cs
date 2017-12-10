using System;
using System.Collections.Generic;
using System.Linq;

using BinaryFunction = System.Func<double, double, double>;  

namespace PC.Spreadsheet
{
    public static class MathParser
    {
        static Dictionary<string, Tuple<byte, BinaryFunction>> functions =  new Dictionary<string, Tuple<byte, BinaryFunction>>()
        {
            { "+", new Tuple<byte, BinaryFunction>( 1, (x, y) => x + y ) },
            { "-", new Tuple<byte, BinaryFunction>( 1, (x, y) => x - y ) },
            { "*", new Tuple<byte, BinaryFunction>( 2, (x, y) => x * y ) },
            { "/", new Tuple<byte, BinaryFunction>( 2, (x, y) => x / y ) }
        };

        static bool IsOperator(string s)
        {
            return functions.ContainsKey(s);
        }

        static bool IsValidNumber(string s)
        {
            double tmp;
            return double.TryParse(s, out tmp);
        }

        static byte OpreratorPriority(string operatorr)
        {
            if (operatorr.Equals("(") || operatorr.Equals(")"))
            {
                return 0;
            }
            else if (functions.ContainsKey(operatorr))
            {
                return functions[operatorr].Item1;
            }
            else
                throw new Exception("Unknown operator");
        }

        static int Precedence(string operator1, string operator2)
        {
            byte operator1priority = OpreratorPriority(operator1);
            byte operator2priority = OpreratorPriority(operator2);
            if (operator1priority > operator2priority) return 1;
            if (operator1priority < operator2priority) return -1;
            return 0;
        }
        
        static IEnumerable<string> InfixNotationToPostfixNotation(IEnumerable<string> tokens)   // Shunting-yard algorithm
        {
            Stack<string> operators = new Stack<string>();
            List<string> output = new List<string>();            

            foreach (string token in tokens)
            {
                if (IsValidNumber(token))
                {
                    output.Add(token);
                }
                if (token == "(")
                {
                    operators.Push(token);
                }
                if (token == ")")
                {
                    while (operators.Count != 0 && operators.Peek() != "(")
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Pop();
                }
                if (IsOperator(token))
                {
                    while (operators.Count != 0 && (Precedence(operators.Peek(), token) > -1)) 
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Push(token);
                }
            }
            while (operators.Count != 0)
            {
                output.Add(operators.Pop());
            }
            return output;
        }

        public static double Evaluate(string s)
        {
            IEnumerable<string> tokens = Tokenizer.Execute(s);
            IEnumerable<string> tokensInRPN = InfixNotationToPostfixNotation(tokens.ToArray());

            Stack<string> stack = new Stack<string>();
            double operand1;
            double operand2;
            foreach (string token in tokensInRPN)
            {
                if (IsOperator(token))
                {
                    operand2 = double.Parse(stack.Pop());
                    operand1 = double.Parse(stack.Pop());
                    double functionResult = functions[token].Item2(operand1, operand2);
                    stack.Push(functionResult.ToString());                    
                }
                else
                {
                    stack.Push(token);
                }
            }
            return double.Parse(stack.Pop());
        }
    }
}
