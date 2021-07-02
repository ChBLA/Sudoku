using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.BaseGame.classes
{
    class Solver
    {

        public Board solveSuduko(Board initial)
        {
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
                        Board solution = solveSuduko(second);
                        if (solution != null)
                            return solution;
                    }
                }
            }

            return null;
        }

        /*
        for cell in B'
        if not cell.isSolved() 
            let S = cell.possibleValues()
            for val in S 
                let B'' = B'.makeMove(cell, val)
                let Solution = solveSuduko(B'')
                if (Solution != null)
                    return Solution
        return null*/

        private Board makeDeterministicMoves(Board initial)
        {
            bool hasBeenChanged = false;
            Board answer = initial;
            do
            {
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
            return true;
        }

        private bool isInvalid(Board initial)
        {
            return !initial.isValid();
        }
    }
}
