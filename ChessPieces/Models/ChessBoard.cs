using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessPieces.Enums;

namespace ChessPieces.Models
{
    internal class ChessBoard
    {
        public List<ChessPiece> Pieces { get; set; }
        public Dictionary<ChessPiece, List<ChessPiece>> Captures { get; set; }
        public ChessBoard(List<(ChessPieceTypeEnum, int, int)> pieces)
        {
            Pieces = new List<ChessPiece>();
            foreach ((ChessPieceTypeEnum type, int row, int column) piece in pieces)
            {
                Pieces.Add(ChessPiece.ChessPieceFactoryMethod(piece.type, new Cell(piece.row, piece.column)));
            }
            Captures = new Dictionary<ChessPiece, List<ChessPiece>>();
            CalculateCaptures();
        }
        private void CalculateCaptures()
        {
            foreach (ChessPiece currentPiece in Pieces)
            {
                Captures.Add(currentPiece, new List<ChessPiece>());
                foreach (ChessPiece possibleCapture in Pieces)
                {
                    if (currentPiece.CanReach(possibleCapture.Location))
                    {
                        bool isCapturable = true;
                        ChessPiecePath path = new ChessPiecePath(currentPiece, possibleCapture.Location);
                        foreach (ChessPiece possibleInterruptor in Pieces)
                        {
                            if (path.Contains(possibleInterruptor))
                            {
                                isCapturable = false;
                            }
                        }
                        if (isCapturable)
                        {
                            Captures[currentPiece].Add(possibleCapture);
                        }
                    }
                }
            }
        }
        public bool AddPiece((ChessPieceTypeEnum type, int row, int column) piece)
        {
            ChessPiece chessPiece = ChessPiece.ChessPieceFactoryMethod(piece.type, new Cell(piece.row, piece.column));
            if (!Pieces.Where(x => x.Location.Equals(chessPiece.Location)).Any())
            {
                Pieces.Add(chessPiece);
                Captures.Clear();
                CalculateCaptures();
                return true;
            }
            return false;
        }
        public bool RemoveAt(int row, int column)
        {
            int previousCount = Pieces.Count;
            Pieces.RemoveAll(x => x.Location.Equals(new Cell(row, column)));
            if (previousCount != Pieces.Count)
            {
                Captures.Clear();
                CalculateCaptures();
                return true;
            }
            return false;
        }
    }
}
