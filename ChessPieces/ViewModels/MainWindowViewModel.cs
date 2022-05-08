using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChessPieces.Enums;
using ChessPieces.Models;
using ChessPieces.Commands;
using System.ComponentModel;

namespace ChessPieces.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly RelayCommand<string> _addPieceCommand = null;
        private readonly RelayCommand<string> _deletePieceCommand = null;
        private string _captures;
        private ChessBoard _chessBoard;
        public RelayCommand<string> AddPieceCommand => _addPieceCommand ?? new RelayCommand<string>(AddPiece, CanAddPiece);
        public RelayCommand<string> DeletePieceCommand => _deletePieceCommand ?? new RelayCommand<string>(DeletePiece, CanDeletePiece);
        public string Captures
        {
            get => _captures;
            set
            {
                _captures = value;
                OnProperyChanged(nameof(Captures));
            }
        }
        public List<(ChessPieceTypeEnum, int, int)> Pieces { get; set; } = new List<(ChessPieceTypeEnum, int, int)>();
        public MainWindowViewModel()
        {
            _chessBoard = new ChessBoard(new List<(ChessPieceTypeEnum, int, int)>());
            UpdateCaptures();
            PiecesToTuple();
        }
        private void UpdateCaptures()
        {
            Captures = "Possible captures:\n" + DataConverter.CapturesToString(_chessBoard.Captures);
        }
        private void PiecesToTuple()
        {
            foreach(ChessPiece piece in _chessBoard.Pieces)
            {
                ChessPieceTypeEnum type = piece.GetPieceType();
                Pieces.Add((type, piece.Location.RowIndex, piece.Location.ColumnIndex));
            }
        }
        public bool CanAddPiece(string pieceString)
        {
            return DataConverter.StringToPiece(pieceString, out (ChessPieceTypeEnum, int, int) piece) && Pieces.Count < 10 && _chessBoard.CanAddPiece((piece.Item2, piece.Item3));
        }
        public void AddPiece(string pieceString)
        {
            DataConverter.StringToPiece(pieceString, out (ChessPieceTypeEnum, int, int) piece);
            Pieces.Add(piece);
            _chessBoard.AddPiece(piece);
            UpdateCaptures();
            PiecesChanged?.Invoke();
        }
        public bool CanDeletePiece(string cellString)
        {
            return DataConverter.StringToCell(cellString, out Cell cell) && Pieces.Count > 0 && !_chessBoard.CanAddPiece((cell.RowIndex, cell.ColumnIndex));
        }
        public void DeletePiece(string cellString)
        {
            DataConverter.StringToCell(cellString, out Cell cell);
            Pieces.RemoveAll(x => cell.Equals(new Cell(x.Item2, x.Item3)));
            _chessBoard.RemoveAt(cell.RowIndex, cell.ColumnIndex);
            UpdateCaptures();
            PiecesChanged?.Invoke();
        }
        public void OnProperyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
        public delegate void PiecesChangedEventHandler();
        public event PiecesChangedEventHandler PiecesChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
