using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

namespace _6tti_andras_wpfAct3Bis_LesChalets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox nbPer;
        public MainWindow()
        {
            InitializeComponent();
            DatabaseGeneratedAttribute
        }

        //encoder un nombre entier entre 1 et 6

        //encoder des dates valides
        //voir le nombre de semaine comptabilisées

    }
}
