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
            this.cells = new Cell[ArraySize, ArraySize]; 
            for (int i = 0; i < ArraySize; i++)
            {
                for (int j = 0; j < ArraySize; j++)
                {
                    this.cells[i, j] = new Cell(i % 9 + 1);
                }
            }
        }

        internal List<int> possibleValues(Cell c)
        {
            throw new NotImplementedException();
        }

        private Board(Board duplicate)
        {
            this.cells = duplicate.getCells();
        }

        internal Board insertValue(Cell c, int move)
        {
            throw new NotImplementedException();
        }

        public Cell[,] getCells()
        {
            return this.cells;
        }

        public void insertValue(int rowIndex, int columnIndex, int value)
        {
            Board tempBoard = this.duplicate();
            tempBoard.getCells()[rowIndex, columnIndex] = new Cell(value);
            if (tempBoard.isValid())
                this.cells = tempBoard.getCells();
        }

        public bool isValid()
        {
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

        public void printBoard()
        {
            Console.WriteLine("\n\n\t-------------------------");
            for (int i = 0; i < ArraySize; i++)
            {
                Console.Write("\t| ");
                for (int j = 0; j < ArraySize; j++)
                {
                    int value = this.cells[i, j].getValue();
                    if (value != Cell.EmptyVal)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ResetColor();
                    Console.Write(this.cells[i, j].getValue());
                    Console.ResetColor();
                    Console.Write(((j + 1) % 3 == 0 ? " | " : " "));
                }
                Console.WriteLine(((i + 1) % 3 == 0) ? "\n\t-------------------------" : "");
            }
        } 

        private Board duplicate()
        {
            return new Board(this);
        }
    }
}
