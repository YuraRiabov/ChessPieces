using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces.Models
{
    internal class ChessBishop : ChessPiece
    {
        protected override bool IsReachable(Cell cell)
        {
            return Location.OnSameDiagonal(cell);
        }
    }
}
