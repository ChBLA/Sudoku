using Sudoku.BaseGame.classes;
using System;
using System.Collections.Generic;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.printBoard();

            board.insertValue(0, 0, 9);
            board.printBoard();
            foreach (List<Cell> row in board.getColumns())
            {
                foreach (Cell cell in row)
                {
                    Console.Write(cell.getValue() + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
