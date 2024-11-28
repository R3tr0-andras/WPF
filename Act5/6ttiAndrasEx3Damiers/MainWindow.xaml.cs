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
        public Button[,] MatButton = new Button[10, 10];
        public MainWindow()
        {
            InitializeComponent();
            InitializeGrid();
            GridColorSet();
        }
        private void InitializeGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                grdMain.ColumnDefinitions.Add(new ColumnDefinition());
                grdMain.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void GridColorSet()
        {
            int compt = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    compt++;
                    Button rect = new Button();
                    rect.Width = 65;
                    rect.Height = 65;
                    rect.Click += new RoutedEventHandler(Clikage);
                    rect.Content = compt.ToString();
                    rect.Foreground = Brushes.Red;
                    rect.FontSize = 12;

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
    }
}
