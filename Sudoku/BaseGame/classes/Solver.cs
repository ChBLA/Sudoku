using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.BaseGame.classes
{
    class Solver
    {
        private int reqCounter = -1;
        private DateTime startTime;
        private const int timeoutInSec = 30;

        public Board solveSudoku(Board initial, int depth)
        {
            this.startTime = DateTime.Now;
            return solve(initial, depth);
        }

        private Board solve(Board initial, int depth)
        {
            reqCounter++;
            if ((DateTime.Now - startTime).TotalSeconds > timeoutInSec)
                throw new TimeoutException();
            //initial.printBoard();
            if (this.isInvalid(initial))
                return null;
            if (this.isSolved(initial))
                return initial;
            Board first = this.makeDeterministicMoves(initial);
            foreach (Cell c in first.getCells())
            {
                if (!c.isDecided())
                {
                    List<int> moves = first.possibleValues(c);
                    foreach (int move in moves)
                    {
                        Board second = first.insertValue(c, move);
                        if (second == null)
                            break;
                        Board solution = solve(second, depth + 1);
                        if (solution != null)
                            return solution;
                    }
                }
            }

            return isSolved(first) ? first : null;
        }

        public int getReqCounter()
        {
            return this.reqCounter;
        }

        private Board makeDeterministicMoves(Board initial)
        {
            Board answer = initial.duplicate();
            bool hasBeenChanged;
            do
            {
                hasBeenChanged = false;
                foreach (Cell cell in answer.getCells())
                {
                    List<int> moves = answer.possibleValues(cell);
                    if (!cell.isDecided() && moves.Count == 1)
                    {
                        answer = answer.insertValue(cell, moves.ElementAt(0));
                        hasBeenChanged = true;
                    }
                }
            } while (hasBeenChanged);
            return answer;
        }

        private bool isSolved(Board initial)
        {
            if (checkGrid(initial.getRows()) && checkGrid(initial.getColumns()) && checkGrid(initial.getSquares()))
                return true;
            return false;
        }

        private bool checkGrid(List<List<Cell>> grid)
        {
            foreach (List<Cell> row in grid)
            {
                var checker = makeCheckArray();
                foreach (Cell cell in row)
                {
                    if (cell.getValue() != Cell.EmptyVal)
                    {
                        checker[cell.getValue() - 1] = true;
                    }
                }

                if (!checkChecker(checker))
                    return false;
            }

            return true;
        }

        private bool checkChecker(bool[] checker)
        {
            for (int i = 0; i < 9; i++)
            {
                if (!checker[i])
                    return false;
            }
            return true;
        }

        private bool isInvalid(Board initial)
        {
            return !initial.isValid();
        }

        private bool[] makeCheckArray()
        {
            bool[] answer = new bool[9];
            for (int i = 0; i < 9; i++)
            {
                answer[i] = false;
            }
            return answer;
        }
    }
}
