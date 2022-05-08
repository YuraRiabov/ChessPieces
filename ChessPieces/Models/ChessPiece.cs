using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessPieces.Enums;

namespace ChessPieces.Models
{
    internal abstract class ChessPiece
    {
        public Cell Location { get; set; }
        public List<Cell> ReachableCells { get; protected set; }
        protected void CalculateReachableCells()
        {
            ReachableCells = new List<Cell>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Cell currentCell = new Cell(i, j);
                    if (IsReachable(currentCell))
                    {
                        ReachableCells.Add(currentCell);
                    }
                }
            }
        }
        protected abstract bool IsReachable(Cell cell);
        public ChessPiece(Cell cell)
        {
            Location = cell;
            CalculateReachableCells();
        }
        public static ChessPiece ChessPieceFactoryMethod(ChessPieceTypeEnum type, Cell cell)
        {
            return type switch
            {
                ChessPieceTypeEnum.King => new ChessKing(cell),
                ChessPieceTypeEnum.Queen => new ChessQueen(cell),
                ChessPieceTypeEnum.Rook => new ChessRook(cell),
                ChessPieceTypeEnum.Bishop => new ChessBishop(cell),
                ChessPieceTypeEnum.Knight => new ChessKnight(cell),
                _ => throw new ArgumentException()
            };
        }
        public bool CanReach(Cell cell)
        {
            foreach (Cell reachable in ReachableCells)
            {
                if (reachable.Equals(cell))
                {
                    return true;
                }
            }
            return false;
        }
        public override string ToString()
        {
            string fullType = this.GetType().Name;
            string shortType = fullType.Split(".")[2];
            return $"{shortType} at {Location.ToString()}";
        }
    }
}
