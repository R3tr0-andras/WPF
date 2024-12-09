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

namespace _6ttiAndrasEx3Damiers
{
    
    public partial class MainWindow : Window
    {
        public Button[,] MatButton;

        // Img
        public BitmapImage fou = new BitmapImage(new Uri("C:\\Users\\Andra\\Desktop\\wpf\\Act5\\6ttiAndrasEx3Damiers\\IMG\\b.png"));
        public BitmapImage roi = new BitmapImage(new Uri("C:\\Users\\Andra\\Desktop\\wpf\\Act5\\6ttiAndrasEx3Damiers\\IMG\\k.png"));
        public BitmapImage cavalier = new BitmapImage(new Uri("C:\\Users\\Andra\\Desktop\\wpf\\Act5\\6ttiAndrasEx3Damiers\\IMG\\kn.png"));
        public BitmapImage pion = new BitmapImage(new Uri("C:\\Users\\Andra\\Desktop\\wpf\\Act5\\6ttiAndrasEx3Damiers\\IMG\\p.png"));
        public BitmapImage reine = new BitmapImage(new Uri("C:\\Users\\Andra\\Desktop\\wpf\\Act5\\6ttiAndrasEx3Damiers\\IMG\\q.png"));
        public BitmapImage tour = new BitmapImage(new Uri("C:\\Users\\Andra\\Desktop\\wpf\\Act5\\6ttiAndrasEx3Damiers\\IMG\\t.png"));

        public MainWindow()
        {
            InitializeComponent();
            InitialiserEchiquier();
        }
        private void InitialiserEchiquier()
        {
            InitializeGrid();
            GridColorSet();
            PlacerPieces();
        }
        private void InitializeGrid()
        {
            for (int i = 0; i < 8; i++)
            {
                grdMain.ColumnDefinitions.Add(new ColumnDefinition());
                grdMain.RowDefinitions.Add(new RowDefinition());
            }
        }
        private void GridColorSet()
        {
            MatButton = new Button[10, 10];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    
                    Button rect = new Button();
                    rect.Width = 65;
                    rect.Height = 65;
                    rect.Click += new RoutedEventHandler(Clikage);
 
                    if ((i + j) % 2 == 0)
                    {
                        rect.Background = Brushes.Black;
                    }
                    else
                    {
                        rect.Background = Brushes.White;
                    }

                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    grdMain.Children.Add(rect);

                    // Stockage du bouton dans la matrice
                    MatButton[i, j] = rect;
                }
            }
        }
        private void Clikage(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Content = "";
        }
        private void PlacerPieces()
        {
            // Placement des pièces blanches
            PlacerPiece(0, 0, tour);
            PlacerPiece(0, 1, cavalier);
            PlacerPiece(0, 2, fou);
            PlacerPiece(0, 3, reine);
            PlacerPiece(0, 4, roi);
            PlacerPiece(0, 5, fou);
            PlacerPiece(0, 6, cavalier);
            PlacerPiece(0, 7, tour);
            for (int i = 0; i < 8; i++)
            {
                PlacerPiece(1, i, pion);
            }

            // Placement des pièces noires
            PlacerPiece(7, 0, tour);
            PlacerPiece(7, 1, cavalier);
            PlacerPiece(7, 2, fou);
            PlacerPiece(7, 3, reine);
            PlacerPiece(7, 4, roi);
            PlacerPiece(7, 5, fou);
            PlacerPiece(7, 6, cavalier);
            PlacerPiece(7, 7, tour);
            for (int i = 0; i < 8; i++)
            {
                PlacerPiece(6, i, pion);
            }
        }
        private void PlacerPiece(int ligne, int colonne, BitmapImage image)
        {
            if (MatButton[ligne, colonne] is Button button)
            {
                Image pieceImage = new Image
                {
                    Source = image,
                    Stretch = Stretch.Uniform
                };
                button.Content = pieceImage;
            }
        }
    }
}
