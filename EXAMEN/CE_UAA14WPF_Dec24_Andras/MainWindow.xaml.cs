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

namespace CE_UAA14WPF_Dec24_Andras
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Img
        public BitmapImage bRouge = new BitmapImage(new Uri("C:\\Users\\Andra\\Desktop\\wpf\\EXAMEN\\CE_UAA14WPF_Dec24_Andras\\img\\petitCercleRouge.png"));
        public BitmapImage bBlanche = new BitmapImage(new Uri("C:\\Users\\Andra\\Desktop\\wpf\\EXAMEN\\CE_UAA14WPF_Dec24_Andras\\img\\golfBall60.png"));

        // Mat
        public Button[,] MatButton;

        public MainWindow()
        {
            InitializeComponent();
            but.MouseDown += new MouseButtonEventHandler(Down);

        }
        private void Down(object sender, MouseButtonEventArgs e)
        {
            Test();
           
        }
        // Met la matrice dans la fenetre
        private void SetUp(Button[,] Mat)
        {
            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {
                    Grid.SetRow(Mat[i, j], 1);
                    Grid.SetColumn(Mat[i, j], j);
                    grdMain.Children.Add(Mat[i, j]);
                }
            }
        }

        // Fait le déroulement du programme
        private void Test()
        {
            if (r1.IsChecked == true)
            {
                Solitaire();
            }
            else if (r2.IsChecked == true)
            {
                Marelle();
            }
            else if (r3.IsChecked == true)
            {
                colBox.PreviewTextInput += new TextCompositionEventHandler(VerifInput);
                rowBox.PreviewTextInput += new TextCompositionEventHandler(VerifInput);
            }
            else
            {
                dessous.Text = "Veuillez sélectionner qqch";
            }
        }

        // Vérifie si c'est bien de 1 à 12
        public void VerifInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int value) || value < 1 || value > 12)
            {
                e.Handled = true;
                dessous.Text = "Veuillez encoder les dimensions valides comprises entre 1 et 12";
            }
            else
            {
                dessous.Text = "Test WPF 6T 2024";
            }

            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length >= 2)
            {
                e.Handled = true;
            }
        }
        // Placer les images
        private void PlacerBalles(int ligne, int colonne, BitmapImage image)
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
        // Forme de solitaire
        private void Solitaire()
        {
            MatButton = new Button[9, 9];
            for (int i = 3; i <= 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    PlacerBalles(i, j, bRouge);
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 3; j <= 5; j++)
                {
                    PlacerBalles(i, j, bRouge);
                }
            }
            SetUp(MatButton);
        }
        // Forme de marelle
        private void Marelle()
        {
            MatButton = new Button[7, 7];
            PlacerBalles(0, 0, bRouge);
            PlacerBalles(0, 3, bRouge);
            PlacerBalles(0, 6, bRouge);

            PlacerBalles(1, 1, bRouge);
            PlacerBalles(1, 3, bRouge);
            PlacerBalles(1, 5, bRouge);

            PlacerBalles(2, 2, bRouge);
            PlacerBalles(2, 3, bRouge);
            PlacerBalles(2, 4, bRouge);

            PlacerBalles(3, 0, bRouge);
            PlacerBalles(3, 1, bRouge);
            PlacerBalles(3, 2, bRouge); 
            PlacerBalles(3, 4, bRouge);
            PlacerBalles(3, 5, bRouge);
            PlacerBalles(3, 6, bRouge);

            PlacerBalles(4, 2, bRouge);
            PlacerBalles(4, 3, bRouge);
            PlacerBalles(4, 4, bRouge);

            PlacerBalles(5, 1, bRouge);
            PlacerBalles(5, 3, bRouge);
            PlacerBalles(5, 5, bRouge);

            PlacerBalles(6, 0, bRouge);
            PlacerBalles(6, 3, bRouge);
            PlacerBalles(6, 6, bRouge);

            SetUp(MatButton);
        }

        
    }
}
