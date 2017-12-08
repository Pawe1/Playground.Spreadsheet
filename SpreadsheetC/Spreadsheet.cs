using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Coordinates = System.Tuple<byte, byte>;

namespace PC.Spreadsheet
{
    public class Spreadsheet
    {
        const char _cellSeparator = '|';
        const char _spreadsheetTerminator = ';';

        Cell[,] _cells = new Cell[256, 256];

        public Spreadsheet()
        {

        }          

        private void clearRow(int index)
        {
            if (index > _cells.GetLength(0))
            {
                throw new Exception("Index out of range");
            }
            for (int LC = 0; LC < _cells.GetLength(0) ; LC++)
                _cells[index, LC].Clear();
        }

        private void clear()
        {
            for(int LC = 0; LC < _cells.GetLength(1) ; LC++)
                clearRow(LC);
        }

        private Coordinates referenceToCoordinates(string value)
        {
            if (value.Length != 2)
                throw new Exception();

            char xPart = value[0];
            char yPart = value[1];

            int x = char.ToUpper(xPart) - 64;
            int y = int.Parse(yPart.ToString());

            return new Coordinates((byte)x, (byte)y);
        }

        public void loadLine(int index, string line)
        {
            if (line.Split(_cellSeparator).Length > _cells.GetLength(0))
            {
                throw new Exception("Too long line");
            }
            if (index > _cells.GetLength(0))
            {
                throw new Exception("Index out of range");
            }

            clearRow(index);

            string[] formulas = line.Replace(_spreadsheetTerminator.ToString(), "").Split(_cellSeparator).ToArray();
               
//                .ForEach(item => ); .foreach()

            for(int LC = 0; LC < Math.Min(formulas.GetLength(0), _cells.GetLength(0)); LC++)
                _cells[index, LC].Formula = formulas[LC];

        }

        public void load(string[] lines)
        {
            if (lines.GetLength(0) > _cells.GetLength(1))
            {
                throw new Exception("Too many lines");
            }

            for (int LC = 0; LC < _cells.GetLength(0) ; LC++)
            {
                if (LC < lines.GetLength(0))
                    loadLine(LC, lines[LC]);
                else
                    clearRow(LC);
            }
        }

        private void evaluateCellValue(int x, int y)
        {



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
                    string value = (_cells[x, y].Value ??default(int)).ToString();                       
                    Console.Write(String.Format("[{0, 10}] ", value));
                }
            }

            Console.WriteLine();

            for (int x = 0; x < 5/*_cells.GetLength(0)*/; x++)
            {
                Console.WriteLine();
                for (int y = 0; y < 5 /*_cells.GetLength(1)*/; y++)
                { 
                    string value = _cells[x, y].Formula;                       
                    Console.Write(String.Format("[{0, 10}] ", value));
                }
            }
        }
    }
}
