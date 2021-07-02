using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.BaseGame.classes
{
    class Cell
    {
        private int value;

        public static int EmptyVal = 0;

        public Cell(int value)
        {
            this.value = value;
        }

        public int getValue()
        {
            return this.value;
        }

        public bool isDecided()
        {
            return this.value != EmptyVal;
        }

         public enum Value
        {
            EmptyVal = 0
        }
    }
}
