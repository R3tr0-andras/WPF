using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace andrasWPFMatchingGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TextBlock timerBlock;

        public MainWindow()
        {
            InitializeComponent();
            ConfigureGrid();
            ConfigureTextBlock();
        }

        private TextBlock SetUpTime()
        {
            TextBlock temps = new TextBlock();
            temps.Text = "Temps écoulé";
            temps.FontSize = 36;
            temps.Name = "txtTemps";
            temps.VerticalAlignment = VerticalAlignment.Center;
            temps.HorizontalAlignment = HorizontalAlignment.Center;
            //temps.MouseDown += new MouseButtonEventHandler(txtTemps_MouseDown);
            Grid.SetColumnSpan(temps, 4);
            Grid.SetRow(temps, 4);
            grdMain.Children.Add(temps);
            return temps;
        }
        private void ConfigureGrid()
        {
            // colonnes
            ColumnDefinition[] colDef = new ColumnDefinition[4];
            for (int i = 0; i < 4; i++)
            {
                colDef[i] = new ColumnDefinition();
                grdMain.ColumnDefinitions.Add(colDef[i]);
            }

            // lignes
            RowDefinition[] rowDef = new RowDefinition[5];
            for (int i = 0; i < 4; i++)
            {
                rowDef[i] = new RowDefinition();
                grdMain.RowDefinitions.Add(rowDef[i]);
            }

            // hauteur de 50
            rowDef[4] = new RowDefinition { Height = new GridLength(50) };
            grdMain.RowDefinitions.Add(rowDef[4]);
        }
        private void ConfigureTextBlock()
        {
            TextBlock[,] TextBlockMat = new TextBlock[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    TextBlockMat[i, j] = new TextBlock();

                    Grid.SetColumn(TextBlockMat[i, j], j);
                    Grid.SetRow(TextBlockMat[i, j], i);
                    grdMain.Children.Add(TextBlockMat[i, j]);

                    TextBlockMat[i, j].VerticalAlignment = VerticalAlignment.Center;
                    TextBlockMat[i, j].HorizontalAlignment = HorizontalAlignment.Center;
                    TextBlockMat[i, j].Text = "?";
                    TextBlockMat[i, j].FontSize = 36;
                    TextBlockMat[i, j].MouseDown += new MouseButtonEventHandler(mous_MouseDown);
                }
            }
        }
        private void mous_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlockActif = sender as TextBlock;

            textBlockActif.Text = "X";
        }
    }
}
