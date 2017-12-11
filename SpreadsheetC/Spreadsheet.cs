using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PC.Spreadsheet
{
    struct coordinates
    {
        public byte x;
        public byte y;
    }

    public class Spreadsheet
    {
        public static char terminator = ';';

        Cell[,] _cells = new Cell[256, 256];        
        public Cell[,] cells { get { return _cells; } }  

        private void ClearRow(int index)
        {
            if (index > _cells.GetLength(0))
            {
                throw new Exception("Index out of range");
            }
            for (int LC = 0; LC < _cells.GetLength(0) ; LC++)
                _cells[LC, index].Clear();
        }

        private void Clear()
        {
            for(int LC = 0; LC < _cells.GetLength(1) ; LC++)
                ClearRow(LC);
        }

        public void LoadLine(int index, string line)
        {
            const char cellSeparator = '|';

            if (line.Split(cellSeparator).Length > _cells.GetLength(0))
            {
                throw new Exception("Too long line");
            }
            if (index > _cells.GetLength(0))
            {
                throw new Exception("Index out of range");
            }

            ClearRow(index);

            string[] formulas = line.Replace(terminator.ToString(), "").Split(cellSeparator); 

            for(int LC = 0; LC < Math.Min(formulas.Count(), _cells.GetLength(0)); LC++)
            { 
                _cells[LC, index].formula = formulas[LC];
                evaluateCellValue(LC, index);
            }

        }

        public void Load(string[] lines)
        {
            if (lines.GetLength(0) > _cells.GetLength(1))
            {
                throw new Exception("Too many lines");
            }

            for (int LC = 0; LC < _cells.GetLength(0) ; LC++)
            {
                if (LC < lines.GetLength(0))
                    LoadLine(LC, lines[LC]);
                else
                    ClearRow(LC);
            }
        }

        private void evaluateCellValue(int x, int y)
        {
            Cell cell = _cells[x, y];
            string formula = cell.formula;

            if (formula.Length == 0)
            {
                cell.value = null;
            }
            else
            {
                if (Classifier.ContainsReferences(formula))
                {
                    formula = ReferenceResolver.Resolve(formula, (coordinates) => _cells[coordinates.x, coordinates.y].formula );
                }
                if (Classifier.ContainsReferences(formula))
                {
                    cell.value = null;
                }
                else if (Classifier.IsExpression(formula))
                {
                    try
                    {
                        cell.value = MathParser.Evaluate(formula);
                    }
                    catch
                    {
                        cell.value = null;
                    }                    
                }
                else   // do we need this?...
                {
                    cell.value = double.Parse(formula.Replace(".", ","));
                }
            }
             _cells[x, y] = cell;
        }

        private void evaluateCells()
        {
            for (int y = 0; y < _cells.GetLength(0); y++)
            {
                for (int x = 0; x < _cells.GetLength(1); x++)
                { 
                    evaluateCellValue(x, y);
                }
            }
        }

        public void print()
        {
            for (int y = 0; y < 5; y++)
            {
                Console.WriteLine();
                for (int x = 0; x < 5; x++)
                { 
                    string value = (_cells[x, y].value??default(int)).ToString();                       
                    Console.Write(String.Format("[{0, 12}] ", value));
                }
            }

            Console.WriteLine();

            for (int y = 0; y < 5; y++)
            {
                Console.WriteLine();
                for (int x = 0; x < 5; x++)
                { 
                    string value = _cells[x, y].formula;                       
                    Console.Write(String.Format("[{0, 12}] ", value));
                }
            }
        }
    }
}
