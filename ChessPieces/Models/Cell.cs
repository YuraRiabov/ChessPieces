using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces.Models
{
    internal class Cell
    {
        private int _rowIndex;
        private int _columnIndex;
        public int RowIndex
        { 
            get => _rowIndex;
            set
            {
                if (value >= 0 && value < 8)
                {
                    _rowIndex = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
            }
        }
        public int ColumnIndex
        {
            get => _columnIndex;
            set
            {
                if (value >= 0 && value < 8)
                {
                    _columnIndex = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
            }
        }
        public Cell(int row, int column)
        {
            RowIndex = row;
            ColumnIndex = column;
        }
        public string RowToString()
        {
            return RowIndex switch
            {
                0 => "a",
                1 => "b",
                2 => "c",
                3 => "d",
                4 => "e",
                5 => "f",
                6 => "g",
                7 => "h",
                _ => ""
            };
        }
        public string ColumnToString()
        {
            return (ColumnIndex + 1).ToString();
        }
        public override string ToString()
        {
            return RowToString() + ColumnToString();
        }
        static bool IsValid(int row, int column)
        {
            return row >= 0 && row < 8 && 
                   column >= 0 && column < 8;
        }
        public bool InSameRow(Cell cell)
        {
            return RowIndex == cell.RowIndex && !this.Equals(cell);
        }
        public bool InSameColumn(Cell cell)
        {
            return ColumnIndex == cell.ColumnIndex && !this.Equals(cell);
        }
        public bool OnSameDiagonal(Cell cell)
        {
            return Math.Abs(ColumnIndex - cell.ColumnIndex) == Math.Abs(RowIndex - cell.RowIndex) && !this.Equals(cell);
        }
        public bool IsNeighbour(Cell cell)
        {
            return (Math.Abs(RowIndex - cell.RowIndex) < 2) && (Math.Abs(ColumnIndex - cell.ColumnIndex) < 2) && !this.Equals(cell);
        }
        public bool WithinKnightReach(Cell cell)
        {
            return Math.Abs(RowIndex - cell.RowIndex) + Math.Abs(ColumnIndex - cell.ColumnIndex) == 3 && !InSameRow(cell) && !InSameColumn(cell);
        }
        public override bool Equals(object? obj)
        {
            if (obj is Cell cell)
            {
                return RowIndex == cell.RowIndex && ColumnIndex == cell.ColumnIndex;
            }
            else
            {
                return false;
            }
        }
    }
}
