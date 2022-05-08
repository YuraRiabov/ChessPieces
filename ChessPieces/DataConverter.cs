using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessPieces.Enums;
using ChessPieces.Models;

namespace ChessPieces
{
    internal class DataConverter
    {
        public static bool StringToPiece(string line, out (ChessPieceTypeEnum type, int row, int column) piece)
        {
            List<string> members;
            if (line.Length == 3)
            {
                if (TryParsePieceType(line[0].ToString(), out piece.type) && 
                    int.TryParse(line[1].ToString(), out piece.row) &&
                    int.TryParse(line[2].ToString(), out piece.column) &&
                    Cell.IsValid(piece.row, piece.column))
                {
                    return true;
                }
            }
            else if ((members = line.Split(" ").ToList()).Count == 3)
            {
                if (TryParsePieceType(members[0], out piece.type) &&
                    int.TryParse(members[1], out piece.row) &&
                    int.TryParse(members[2], out piece.column) &&
                    Cell.IsValid(piece.row, piece.column))
                {
                    return true;
                }
            }
            piece = (ChessPieceTypeEnum.King, 0, 0);
            return false;
        }
        public static string CapturesToString(Dictionary<ChessPiece, List<ChessPiece>> captures)
        {
            string capturesString = "";
            foreach (ChessPiece piece in captures.Keys)
            {
                if (captures[piece].Count > 0)
                {
                    capturesString += $"{piece.ToString()} can capture ";
                    foreach (ChessPiece capture in captures[piece])
                    {
                        capturesString += $"{capture.ToString().ToLower()}, ";
                    }
                    capturesString.TrimEnd();
                    capturesString.TrimEnd(',');
                    capturesString += "\n";
                }
            }
            capturesString.TrimEnd('\n');
            return capturesString;
        }
        private static bool TryParsePieceType(string piece, out ChessPieceTypeEnum type)
        {
            try
            {
                type = piece.ToLower() switch
                {
                    "k" => ChessPieceTypeEnum.King,
                    "king" => ChessPieceTypeEnum.King,
                    "q" => ChessPieceTypeEnum.Queen,
                    "queen" => ChessPieceTypeEnum.Queen,
                    "r" => ChessPieceTypeEnum.Rook,
                    "rook" => ChessPieceTypeEnum.Rook,
                    "b" => ChessPieceTypeEnum.Bishop,
                    "bishop" => ChessPieceTypeEnum.Bishop,
                    "n" => ChessPieceTypeEnum.Knight,
                    "knight" => ChessPieceTypeEnum.Knight,
                    _ => throw new ArgumentException()
                };
                return true;
            }
            catch (ArgumentException e)
            {
                type = ChessPieceTypeEnum.King;
                return false;
            }
        }
    }
}
