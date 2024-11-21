using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TextBlock dyna = new TextBlock();
        public TextBlock info = new TextBlock();

        public TextBox box = new TextBox();

        public Button btn1 = new Button();
        public Button btn2 = new Button();
        public Button btn3 = new Button();

        StackPanel stkPan = new StackPanel();
        ComboBox cboBox = new ComboBox();

        public MainWindow()
        {
            InitializeComponent();
            PrepareAll();
        }
        public void PrepareAll()
        {
            // Prépare la grille
            PrepareGrid();

            // met les infos et dyna
            dyna = PrepareTextBlock(dyna, 1);
            info = PrepareTextBlock(info, 2);

            // Met les bouttons
            SetUpButton(btn1, 1);
            SetUpButton(btn2, 2);
            SetUpButton(btn3, 3);

            // Met la textBox
            SetUpTextBox(box);

            // Met le stackPannel
            StckPanPrepare();

            // Met la comboBox
            PrepareComboBox(cboBox);
        }
        public void PrepareGrid()
        {
            //Cols
            ColumnDefinition coldef1 = new ColumnDefinition();
            ColumnDefinition coldef2 = new ColumnDefinition();
            ColumnDefinition coldef3 = new ColumnDefinition();
            grdMain.ColumnDefinitions.Add(coldef1);
            grdMain.ColumnDefinitions.Add(coldef2);
            grdMain.ColumnDefinitions.Add(coldef3);

            //Rows
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            RowDefinition rowDef3 = new RowDefinition();
            grdMain.RowDefinitions.Add(rowDef1);
            grdMain.RowDefinitions.Add(rowDef2);
            grdMain.RowDefinitions.Add(rowDef3);
        }
        public TextBlock PrepareTextBlock(TextBlock local, byte Id)
        {
            local.Height = 80;
            local.Background = Brushes.Yellow;
            local.FontWeight = FontWeights.Bold;

            if (Id == 1) // Mettre en rouge le texte + changer le span
            {
                local.Text = "TextBlock créé dynamiquement";
                local.Foreground = Brushes.Red;
                local.FontSize = 24;
                Grid.SetColumn(local, 0);
                Grid.SetRow(local, 0);
                Grid.SetColumnSpan(local, 3);
                grdMain.Children.Add(local);
            }
            else if (Id == 2) // Mettre le texte en noire + changer le span
            {
                local.Text = "Infos :";
                local.Foreground = Brushes.Black;
                local.FontSize = 20;
                stkPan.Children.Add(local);
            }

            return local;
        }
        public Button SetUpButton(Button btn, byte btnId)
        {
            btn.Content = btnId;
            btn.Height = 300;
            btn.Width = 300;
            btn.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetColumn(btn, btnId - 1);
            Grid.SetRow(btn, 1);
            grdMain.Children.Add(btn);

            return btn;
        }
        public TextBox SetUpTextBox(TextBox text)
        {
            text.Text = "J'attend vos infos";

            //Grid
            Grid.SetColumn(text, 0);
            Grid.SetRow(text, 3);
            Grid.SetColumnSpan(text, 2);
            stkPan.Children.Add(text);

            return text;
        }
        public void StckPanPrepare()
        {
            stkPan.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(stkPan, 0);
            Grid.SetRow(stkPan, 3);
            Grid.SetColumnSpan(stkPan, 2);
            grdMain.Children.Add(stkPan);
        }
        public ComboBox PrepareComboBox(ComboBox comboBoxLocal)
        {
            //Attribut
            comboBoxLocal.Height = 150;
            comboBoxLocal.Width = 150;

            //Item
            comboBoxLocal.Items.Add("1");
            comboBoxLocal.Items.Add("2");

            //Grid
            Grid.SetColumn(comboBoxLocal, 2);
            Grid.SetRow(comboBoxLocal, 2);
            grdMain.Children.Add(comboBoxLocal);

            return comboBoxLocal;
        }
    }
}