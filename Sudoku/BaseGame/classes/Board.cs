using Sudoku.BaseGame.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.BaseGame.classes
{
    class Board
    {
        private static int ArraySize = 9;
        private Cell[,] cells;

        public Board()
        {
            Random rand = new Random();
            this.cells = new Cell[ArraySize, ArraySize]; 
            for (int i = 0; i < ArraySize; i++)
            {
                for (int j = 0; j < ArraySize; j++)
                {
                    this.cells[i, j] = new Cell(rand.Next(1, 10), i, j);
                }
            }
        }

        public Board(String encodedGame)
        {
            this.cells = new Cell[ArraySize, ArraySize];
            for (int i = 0; i < ArraySize; i++)
            {
                for (int j = 0; j < ArraySize; j++)
                {
                    int val = int.Parse(encodedGame.ElementAt(i * 9 + j).ToString());
                    this.cells[i, j] = new Cell(val, i, j, true);
                }
            }
        }

        internal List<int> possibleValues(Cell c)
        {
            var rows = getRow(c);
            var columns = getColumn(c);
            var squares = getSquare(c);
            
            List<int> basePossibilities = getBasePossibleValues(c);

            foreach (int pos in basePossibilities)
            {
                bool multDef = false;
                foreach (Cell cell in rows)
                {
                    if (!cell.Equals(c) && getBasePossibleValues(cell).Contains(pos))
                    {
                        multDef = true;
                    }
                }
                if (!multDef)
                    return new List<int> { pos };
                multDef = false;
                foreach (Cell cell in columns)
                {
                    if (!cell.Equals(c) && getBasePossibleValues(cell).Contains(pos))
                    {
                        multDef = true;
                    }
                }
                if (!multDef)
                    return new List<int> { pos };
                multDef = false;
                foreach (Cell cell in squares)
                {
                    if (!cell.Equals(c) && getBasePossibleValues(cell).Contains(pos))
                    {
                        multDef = true;
                    }
                }
                if (!multDef)
                    return new List<int> { pos };
            }
            return basePossibilities;
        }

        public List<int> getBasePossibleValues(Cell c)
        {
            var answer = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var rows = getRow(c).Select(x => x.getValue());
            var columns = getColumn(c).Select(x => x.getValue());
            var squares = getSquare(c).Select(x => x.getValue());
            var union = rows.Union(columns).Union(squares).ToList();

            answer.RemoveAll(x => union.Contains(x));

            return c.isDecided() ? new List<int>() : answer;
        }

        private Board(Board duplicate)
        {
            this.cells = new Cell[ArraySize, ArraySize];
            foreach (Cell cell in duplicate.getCells())
            {
                this.cells[cell.getRow(), cell.getColumn()] = new Cell(cell.getValue(), cell.getRow(), cell.getColumn(), cell.getIsBase());
            }
        }

        internal Board insertValue(Cell c, int move)
        {
            for (int i = 0; i < ArraySize; i++)
            {
                for (int j = 0; j < ArraySize; j++)
                {
                    if (c.Equals(this.getCells()[i,j]))
                    {
                        return insertValue(i, j, move);
                    }

                }
            }

            throw new CellNotFoundException("Cell could not be found");
        }

        public Cell[,] getCells()
        {
            return this.cells;
        }

        public Board insertValue(int rowIndex, int columnIndex, int value)
        {
            Board tempBoard = this.duplicate();
            tempBoard.getCells()[rowIndex, columnIndex] = new Cell(value, rowIndex, columnIndex);
            if (tempBoard.isValid())
                return tempBoard;
            return null;
        }

        public bool isValid()
        {
            return gridIsValid(getRows()) && gridIsValid(getColumns()) && gridIsValid(getSquares());
        }

        private bool gridIsValid(List<List<Cell>> grid)
        {
            foreach(List<Cell> subgrid in grid)
            {
                if (subgrid.Where(x => x.getValue() != Cell.EmptyVal).GroupBy(x => x.getValue()).Any(g => g.Count() > 1))
                    return false;
            }
            return true;
        }

        public List<List<Cell>> getRows()
        {
            List<List<Cell>> answer = new List<List<Cell>>();
            for (int i = 0; i < ArraySize; i++)
            {
                List<Cell> temp = new List<Cell>();
                for (int j = 0; j < ArraySize; j++)
                {
                    temp.Add(this.getCells()[i, j]);
                }
                answer.Add(temp);
            }
            return answer;
        }

        public List<List<Cell>> getColumns()
        {
            List<List<Cell>> answer = new List<List<Cell>>();
            for (int i = 0; i < ArraySize; i++)
            {
                List<Cell> temp = new List<Cell>();
                for (int j = 0; j < ArraySize; j++)
                {
                    temp.Add(this.getCells()[j, i]);
                }
                answer.Add(temp);
            }
            return answer;
        }

        public List<List<Cell>> getSquares()
        {
            List<List<Cell>> answer = new List<List<Cell>>();
            for (int i = 0; i < 3; i++)
            {
                List<Cell> temp1 = new List<Cell>();
                List<Cell> temp2 = new List<Cell>();
                List<Cell> temp3 = new List<Cell>();
                for (int j = 0; j < ArraySize; j++)
                {
                    if (j < 3)
                    {
                        temp1.Add(this.getCells()[j, i * 3]);
                        temp1.Add(this.getCells()[j, i * 3 + 1]);
                        temp1.Add(this.getCells()[j, i * 3 + 2]);
                    } else if (j < 6)
                    {
                        temp2.Add(this.getCells()[j, i * 3]);
                        temp2.Add(this.getCells()[j, i * 3 + 1]);
                        temp2.Add(this.getCells()[j, i * 3 + 2]);
                    } else {
                        temp3.Add(this.getCells()[j, i * 3]);
                        temp3.Add(this.getCells()[j, i * 3 + 1]);
                        temp3.Add(this.getCells()[j, i * 3 + 2]);
                    }
                }
                answer.Add(temp1);
                answer.Add(temp2);
                answer.Add(temp3);
            }
            return answer;
        }

        public void printBoard()
        {
            Console.WriteLine("\n\n\t-------------------------");
            for (int i = 0; i < ArraySize; i++)
            {
                Console.Write("\t| ");
                for (int j = 0; j < ArraySize; j++)
                {
                    Cell cell = this.cells[i, j];
                    if (cell.getValue() != Cell.EmptyVal)
                    {
                        if (cell.getIsBase())
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.Write(this.cells[i, j].getValue());
                    Console.ResetColor();
                    Console.Write(((j + 1) % 3 == 0 ? " | " : " "));
                }
                Console.WriteLine(((i + 1) % 3 == 0) ? "\n\t-------------------------" : "");
            }
        } 

        public Board duplicate()
        {
            return new Board(this);
        }

        private List<Cell> getRow(Cell cell)
        {
            foreach (List<Cell> row in getRows())
            {
                foreach (Cell c in row)
                {
                    if (c.Equals(cell))
                        return row;
                }
            }
            return null;
        }

        private List<Cell> getColumn(Cell cell)
        {
            foreach (List<Cell> column in getColumns())
            {
                foreach (Cell c in column)
                {
                    if (c.Equals(cell))
                        return column;
                }
            }
            return null;
        }

        private List<Cell> getSquare(Cell cell)
        {
            foreach (List<Cell> square in getSquares())
            {
                foreach (Cell c in square)
                {
                    if (c.Equals(cell))
                        return square;
                }
            }
            return null;
        }
    }
}
