using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces.Models
{
    internal class ChessQueen : ChessPiece
    {
        public ChessQueen(Cell cell) : base(cell) {}

        protected override bool IsReachable(Cell cell)
        {
            return Location.InSameRow(cell) || Location.InSameColumn(cell) || Location.OnSameDiagonal(cell);
        }
    }
}
