using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces.Models
{
    internal class ChessKnight : ChessPiece
    {
        protected override bool IsReachable(Cell cell)
        {
            return Location.WithinKnightReach(cell);
        }
    }
}
