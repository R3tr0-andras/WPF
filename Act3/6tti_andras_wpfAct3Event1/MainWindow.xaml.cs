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

namespace _6tti_andras_wpfAct3Event1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Fonction pour calculer (elle s'exécute quand on clique)
        private void Calculer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Les valeurs 
                double A = double.Parse(txtA.Text);
                double B = double.Parse(txtB.Text);
                double C = double.Parse(txtC.Text);

                // Calcul
                double resultat = Delta(A, B, C);

                // Affichage du résultat
                MessageBox.Show($"Le Delta est : {resultat}");
            }
            // Gestion des erreurs
            catch (FormatException)
            { 
                MessageBox.Show("Veuillez entrer des nombres valides dans les champs A, B, et C.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // fonction pour faire le delta
        private double Delta(double A, double B, double C)
        {
            // b² . 4ac
            double resultat = Math.Pow(B, 2) * 4 * A * C;
            return resultat;
        }

        // Afficher le bouton "V"
        private void Calculer_MouseEnter(object sender, RoutedEventArgs e)
        {
            btnV.Visibility = Visibility.Visible;
        }

        // Masquer le bouton "V"
        private void Calculer_MouseLeave(object sender, RoutedEventArgs e)
        {
            btnV.Visibility = Visibility.Collapsed;
        }
    }
}
