using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces.Models
{
    internal class ChessPiecePath
    {
        public List<Cell> Path { get; private set; }
        public ChessPiecePath(ChessPiece piece, Cell cell)
        {
            ConstructPath(piece.Location, cell);
        }

        private void ConstructPath(Cell from, Cell to)
        {
            Path = new List<Cell>();
            if (from.InSameRow(to))
            {
                AddRow(from, to);
            }
            else if (from.InSameColumn(to))
            {
                AddColumn(from, to);
            }
            else if (from.OnSameDiagonal(to))
            {
                AddDiagonal(from, to);
            }
        }
        private void AddRow(Cell from, Cell to)
        {
            int leftBoundary = Math.Min(from.ColumnIndex, to.ColumnIndex);
            int rightBoundary = Math.Max(from.ColumnIndex, to.ColumnIndex);
            for (int i = leftBoundary + 1; i < rightBoundary; i++)
            {
                Path.Add(new Cell(from.RowIndex, i));
            }
        }
        private void AddColumn(Cell from, Cell to)
        {
            int bottomBoundary = Math.Min(from.RowIndex, to.RowIndex);
            int topBoundary = Math.Max(from.RowIndex, to.RowIndex);
            for (int i = bottomBoundary + 1; i < topBoundary; i++)
            {
                Path.Add(new Cell(i, from.ColumnIndex));
            }
        }
        private void AddDiagonal(Cell from, Cell to)
        {
            Cell leftCell = from.ColumnIndex < to.ColumnIndex ? from : to;
            Cell rightCell = leftCell.Equals(from) ? to : from;
            if (leftCell.RowIndex < rightCell.RowIndex)
            {
                for (int i = leftCell.RowIndex + 1, j = leftCell.ColumnIndex + 1; i < rightCell.RowIndex; i++, j++)
                {
                    Path.Add(new Cell(i, j));
                }
            }
            else
            {
                for (int i = leftCell.RowIndex - 1, j = leftCell.ColumnIndex + 1; i > rightCell.RowIndex; i--, j++)
                {
                    Path.Add(new Cell(i, j));
                }
            }
        }
        public bool Contains(ChessPiece piece)
        {
            foreach (Cell cell in Path)
            {
                if (cell.Equals(piece.Location))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
