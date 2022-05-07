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

        private void ConstructPath(Cell firstCell, Cell secondCell)
        {
            Path = new List<Cell>();
            if (firstCell.InSameRow(secondCell))
            {
                AddRow(firstCell, secondCell);
            }
            else if (firstCell.InSameColumn(secondCell))
            {
                AddColumn(firstCell, secondCell);
            }
            else if (firstCell.OnSameDiagonal(secondCell))
            {
                AddDiagonal(firstCell, secondCell);
            }
        }
        private void AddRow(Cell firstCell, Cell secondCell)
        {
            int leftBoundary = Math.Min(firstCell.RowIndex, secondCell.RowIndex);
            int rightBoundary = Math.Max(firstCell.RowIndex, secondCell.RowIndex);
            for (int i = leftBoundary + 1; i < rightBoundary; i++)
            {
                Path.Add(new Cell(i, firstCell.ColumnIndex));
            }
        }
        private void AddColumn(Cell firstCell, Cell secondCell)
        {
            int bottomBoundary = Math.Min(firstCell.ColumnIndex, secondCell.ColumnIndex);
            int topBoundary = Math.Max(firstCell.ColumnIndex, secondCell.ColumnIndex);
            for (int i = bottomBoundary + 1; i < topBoundary; i++)
            {
                Path.Add(new Cell(firstCell.RowIndex, i));
            }
        }
        private void AddDiagonal(Cell firstCell, Cell secondCell)
        {
            Cell leftCell = firstCell.RowIndex < secondCell.RowIndex ? firstCell : secondCell;
            Cell rightCell = leftCell.Equals(firstCell) ? secondCell : firstCell;
            if (leftCell.ColumnIndex < rightCell.ColumnIndex)
            {
                for (int i = leftCell.RowIndex + 1, j = leftCell.ColumnIndex + 1; i < rightCell.RowIndex; i++, j++)
                {
                    Path.Add(new Cell(i, j));
                }
            }
            else
            {
                for (int i = leftCell.RowIndex + 1, j = leftCell.ColumnIndex + 1; i < rightCell.RowIndex; i++, j--)
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
