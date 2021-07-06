using AngleSharp;
using AngleSharp.Io;
using Sudoku.BaseGame.classes;
using Sudoku.WebLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var req = new SudokuRequester();
            Board board = new Board(req.getSudoku(1).Result);
            Solver solver = new Solver();
            board.printBoard();

            DateTime start = DateTime.Now;
            Board solution = solver.solveSudoku(board, 0);
            Console.WriteLine("Recursive calls: " + solver.getReqCounter());
            Console.WriteLine("Time: " + (DateTime.Now - start).TotalSeconds + "\n\n");
            solution.printBoard();
        }
    }
}
