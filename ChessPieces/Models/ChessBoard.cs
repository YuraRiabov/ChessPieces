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
        public List<ChessPiece> Pieces { get; private set; }
        public Dictionary<ChessPiece, List<ChessPiece>> Captures { get; private set; }
        public ChessBoard(List<(ChessPieceTypeEnum, int, int)> pieces)
        {
            Pieces = new List<ChessPiece>();
            foreach ((ChessPieceTypeEnum type, int row, int column) piece in pieces)
            {
                Pieces.Add(ChessPiece.CreateChessPiece(piece.type, new Cell(piece.row, piece.column)));
            }
            CalculateCaptures();
        }
        private void CalculateCaptures()
        {
            Captures = new Dictionary<ChessPiece, List<ChessPiece>>();
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
        public bool CanAddPiece((int row, int column) coordinates)
        {
            return !Pieces.Any(x => x.Location.Equals(new Cell(coordinates.row, coordinates.column)));
        }
        public void AddPiece((ChessPieceTypeEnum type, int row, int column) piece)
        {
            ChessPiece chessPiece = ChessPiece.CreateChessPiece(piece.type, new Cell(piece.row, piece.column));
            Pieces.Add(chessPiece);
            CalculateCaptures();
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

        public List<Cell> GetCapturableCells(int row, int column)
        {
            ChessPiece? capturingPiece = Pieces.FirstOrDefault(x => x.Location.Equals(new Cell(row, column)));
            if (capturingPiece == null)
            {
                return new List<Cell>();
            }
            else
            {
                return Captures[capturingPiece].Select(x => x.Location).ToList();
            }
        }
    }
}
