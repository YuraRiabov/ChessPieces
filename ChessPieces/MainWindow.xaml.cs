using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessPieces.ViewModels;
using ChessPieces.Enums;
using Microsoft.Win32;

namespace ChessPieces
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }
        private string _welcomeMessage = "Welcome to Chess Pieces!\nHere you can load a chess position from a file(from 1 to 10 pieces in format \"king 0 0\") and see all possible captures.\n" +
            " In addition you can add pieces(in initial input format of by actual coordinates \"Ka1\") or delete them(by coordinates in either input format without specifying piece type).\n" +
            "To get started select a file containing initial position";
        public MainWindow()
        {
            MessageBox.Show(_welcomeMessage, "Chess Pieces");
            ViewModel = new MainWindowViewModel(GetPositionFromFile());
            InitializeComponent();
            AddPieceImages();
            ViewModel.PiecesChanged += AddPieceImages;
        }
        private void AddPieceImages()
        {
            ClearPictures();
            foreach((ChessPieceTypeEnum type, int row, int column) piece in ViewModel.Pieces)
            {
                BitmapImage image = GetImageByType(piece.type);
                int gridRow = 8 - piece.row;
                int gridColumn = piece.column + 1;
                SetImage(gridRow, gridColumn, image);
            }
        }
        private void ClearPictures()
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    SetImage(i, j, null);
                }
            }
        }
        private List<(ChessPieceTypeEnum, int, int)> GetPositionFromFile()
        {
            List<(ChessPieceTypeEnum, int, int)> position = new List<(ChessPieceTypeEnum, int, int)>();
            OpenFileDialog openDialog = new OpenFileDialog { Filter = "Text files |*.txt" };
            if (true == openDialog.ShowDialog())
            {
                position = DataConverter.GetPiecesFromFile(openDialog.FileName);
            }
            return position;
        }
        private void SetImage(int row, int column, BitmapImage? image)
        {
            BoardGrid.Children.Cast<StackPanel>().First(x => Grid.GetRow(x) == row && Grid.GetColumn(x) == column).Children.Cast<Image>().ToList()[0].Source = image;
        }
        private BitmapImage GetImageByType(ChessPieceTypeEnum type)
        {
            string fileName = type switch
            {
                ChessPieceTypeEnum.King => "King.png",
                ChessPieceTypeEnum.Queen => "Queen.png",
                ChessPieceTypeEnum.Rook => "Rook.png",
                ChessPieceTypeEnum.Bishop => "Bishop.png",
                ChessPieceTypeEnum.Knight => "Knight.png",
                _ => throw new ArgumentException()
            };
            string path = Environment.CurrentDirectory;
            BitmapImage image = new BitmapImage(new Uri($@"{path}\PieceImages\{fileName}"));
            return image;
        }
    }
}
