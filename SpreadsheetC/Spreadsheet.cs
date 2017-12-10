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
        public Cell[,] cells { get; set; }       

        private void ClearRow(int index)
        {
            if (index > _cells.GetLength(0))
            {
                throw new Exception("Index out of range");
            }
            for (int LC = 0; LC < _cells.GetLength(0) ; LC++)
                _cells[index, LC].clear();
        }

        private void Clear()
        {
            for(int LC = 0; LC < _cells.GetLength(1) ; LC++)
                ClearRow(LC);
        }

        public void LoadLine(int index, string line)
        {
            const char _cellSeparator = '|';

            if (line.Split(_cellSeparator).Length > _cells.GetLength(0))
            {
                throw new Exception("Too long line");
            }
            if (index > _cells.GetLength(0))
            {
                throw new Exception("Index out of range");
            }

            ClearRow(index);

            string[] formulas = line.Replace(terminator.ToString(), "").Split(_cellSeparator).ToArray();
               
//                .ForEach(item => ); .foreach()

            for(int LC = 0; LC < Math.Min(formulas.GetLength(0), _cells.GetLength(0)); LC++)
            { 
                _cells[index, LC].Formula = formulas[LC];
                evaluateCellValue(index, LC);
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
            if (cell.Formula.Length > 0)
            {
                if (Classifier.containsReferences(cell.Formula))
                {
                    var zzz = Classifier.classiffy(cell.Formula);


                //    IEnumerable<string> references = cell.extractReferences();
                  /*  var translatedReferences = references.Select( r => referenceToCoordinates(r) );

                    translatedReferences*/


                    
                    cell.Value = null;
                    return;
                }

                if (Classifier.isExpression(cell.Formula))
                {
                    try
                    {
                        cell.Value = Parser.Evaluate(cell.Formula);
                    }
                    catch
                    {
                        cell.Value = null;
                    }                    
                }
                else   // do we need this?...
                {
                    cell.Value = double.Parse(cell.Formula.Replace(".", ","));
                }
            }
            else
            {
                cell.Value = null;
            }

             _cells[x, y] = cell;
        }

        private void evaluateCells()
        {
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                { 
                    evaluateCellValue(x, y);
                }
            }
        }

        public void print()
        {
            for (int x = 0; x < 5/*_cells.GetLength(0)*/; x++)
            {
                Console.WriteLine();
                for (int y = 0; y < 5 /*_cells.GetLength(1)*/; y++)
                { 
                    string value = (_cells[x, y].Value??default(int)).ToString();                       
                    Console.Write(String.Format("[{0, 12}] ", value));
                }
            }

            Console.WriteLine();

            for (int x = 0; x < 5/*_cells.GetLength(0)*/; x++)
            {
                Console.WriteLine();
                for (int y = 0; y < 5 /*_cells.GetLength(1)*/; y++)
                { 
                    string value = _cells[x, y].Formula;                       
                    Console.Write(String.Format("[{0, 12}] ", value));
                }
            }
        }
    }
}
