using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _6ttiAndrasEx2Damiers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            int compt = 1;
            for (int i = 0; i < 10; i++)
            {
                
                bool leftToRight = (i % 2 == 0);

                for (int j = 0; j < 10; j++)
                {
                    
                    int col = leftToRight ? j : 9 - j;

                    Button rect = new Button();
                    rect.Width = 65;
                    rect.Height = 65;
                    rect.Click += new RoutedEventHandler(Clikage);
                    rect.Content = compt.ToString();
                    rect.Foreground = Brushes.Red;
                    rect.FontSize = 12;

                    if ((i + col) % 2 == 0)
                    {
                        rect.Background = Brushes.Black;
                    }
                    else
                    {
                        rect.Background = Brushes.White;
                    }

                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, col);
                    grdMain.Children.Add(rect);

                    MatButton[i, col] = rect;
                    compt++;
                }
            }
        }

        private void Clikage(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Content = "";
        }
    }
}