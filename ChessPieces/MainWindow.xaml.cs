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

namespace ChessPieces
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; } = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            AddPieceImages();
            ViewModel.PiecesChanged += AddPieceImages;
        }
        private void AddPieceImages()
        {
            foreach((ChessPieceTypeEnum type, int row, int column) piece in ViewModel.Pieces)
            {
                BitmapImage image = GetImageByType(piece.type);
                int gridRow = 8 - piece.row;
                int gridColumn = piece.column + 1;
                BoardGrid.Children.Cast<StackPanel>().First(x => Grid.GetRow(x) == gridRow && Grid.GetColumn(x) == gridColumn).Children.Cast<Image>().ToList()[0].Source = image;
            }
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
