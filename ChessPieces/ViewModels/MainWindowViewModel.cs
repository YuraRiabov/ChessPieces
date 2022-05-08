using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessPieces.Enums;
using ChessPieces.Models;

namespace ChessPieces.ViewModels
{
    public class MainWindowViewModel
    {
        List<(ChessPieceTypeEnum, int, int)> mockPieces = new List<(ChessPieceTypeEnum, int, int)>
        {
            (ChessPieceTypeEnum.Queen, 1, 1),
            (ChessPieceTypeEnum.Rook, 2, 2),
            (ChessPieceTypeEnum.King, 3, 3),
            (ChessPieceTypeEnum.Bishop, 2, 4),
            (ChessPieceTypeEnum.Knight, 1, 4)
        };
        public string Captures { get; set; }
        private ChessBoard _chessBoard;
        public MainWindowViewModel()
        {
            _chessBoard = new ChessBoard(mockPieces);
            Captures = "Possible captures:\n" + DataConverter.CapturesToString(_chessBoard.Captures);
        }

    }
}
