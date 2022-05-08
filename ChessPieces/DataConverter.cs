using System;
using System.Collections.Generic;
using System.IO;
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
            if (line == null)
            {
                piece = (ChessPieceTypeEnum.King, 0, 0);
                return false;
            }
            List<string> members;
            if (line.Length == 3)
            {
                piece.column = (int)line[1] - 97;
                if (TryParsePieceType(line[0].ToString(), out piece.type) &&
                    int.TryParse(line[2].ToString(), out piece.row) &&
                    Cell.IsValid(--piece.row, piece.column))
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
        public static bool StringToCell(string line, out Cell cell)
        {
            if (line == null)
            {
                cell = new Cell(0, 0);
                return false;
            }
            List<string> members;
            if (line.Length == 2)
            {
                int column = (int)line[0] - 97;
                if (int.TryParse(line.Substring(1, 1), out int row) &&
                    Cell.IsValid(--row, column))
                {
                    cell = new Cell(row, column);
                    return true;
                }
            }
            else if ((members = line.Split(" ").ToList()).Count == 2)
            {
                if (int.TryParse(members[0], out int row) &&
                    int.TryParse(members[1], out int column) &&
                    Cell.IsValid(row, column))
                {
                    cell = new Cell(row, column);
                    return true;
                }
            }
            cell = new Cell(0, 0);
            return false;
        }
        public static List<(ChessPieceTypeEnum type, int row, int column)> GetPiecesFromFile(string fileName)
        {
            List<(ChessPieceTypeEnum type, int row, int column)> pieces = new List<(ChessPieceTypeEnum type, int row, int column)> ();
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string? line;
                while((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            foreach (string line in lines)
            {
                if (StringToPiece(line, out (ChessPieceTypeEnum type, int row, int column) piece))
                {
                    pieces.Add(piece);
                }
            }
            return pieces;
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
                    capturesString = capturesString.TrimEnd();
                    capturesString = capturesString.TrimEnd(',');
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
