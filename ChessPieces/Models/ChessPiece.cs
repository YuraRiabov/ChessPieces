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
        private Cell _location;

        public Cell Location
        {
            get => _location;
            set
            {
                _location = value;
                CalculateReachableCells();
            }
        }
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

        protected ChessPiece(Cell cell)
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
            return IsReachable(cell);
        }
        public ChessPieceTypeEnum GetPieceType()
        {
            return this switch
            {
                ChessKing => ChessPieceTypeEnum.King,
                ChessQueen => ChessPieceTypeEnum.Queen,
                ChessRook => ChessPieceTypeEnum.Rook,
                ChessBishop => ChessPieceTypeEnum.Bishop,
                ChessKnight => ChessPieceTypeEnum.Knight,
                _ => throw new ArgumentException()
            };
        }
        public override string ToString()
        {
            string type = this.GetType().Name;
            type = type.Replace("Chess", "");
            return $"{type} at {Location.ToString()}";
        }
    }
}
