using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.BaseGame.classes
{
    class Cell
    {
        private int value;
        private int row;
        private int column;
        private bool isBase = false;
        public static int EmptyVal = 0;

        public Cell(int value, int row, int column)
        {
            this.value = value;
            this.row = row;
            this.column = column;
        }

        public Cell(int value, int row, int column, bool isBase)
        {
            this.value = value;
            this.row = row;
            this.column = column;
            this.isBase = isBase;
        }

        public int getValue()
        {
            return this.value;
        }

        public bool getIsBase()
        {
            return isBase;
        }

        public bool isDecided()
        {
            return this.value != EmptyVal;
        }

        public override bool Equals(object obj)
        {
            return obj is Cell cell &&
                   value == cell.value &&
                   row == cell.row &&
                   column == cell.column;
        }

        public enum Value
        {
            EmptyVal = 0
        }

        public int getRow()
        {
            return this.row;
        }

        public int getColumn()
        {
            return this.column;
        }
        
    }
}
