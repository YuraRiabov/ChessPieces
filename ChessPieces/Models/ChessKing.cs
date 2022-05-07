using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces.Models
{
    internal class ChessKing : ChessPiece
    {
        protected override bool IsReachable(Cell cell)
        {
            return Location.IsNeighbour(cell);
        }
    }
}
