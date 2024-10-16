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
        public MainWindow()
        {
            InitializeComponent();

            // gestionnaire pour vérifier l'input dans la TextBox
            txtboxPersonnes.PreviewTextInput += new TextCompositionEventHandler(VerifInput);

            // Définit des dates non sélectionnables, toutes les dates avant aujourd'hui sont bloquées
            CalendarDateRange blackoutDays = new CalendarDateRange(new DateTime(0001, 01, 01), DateTime.Now.AddDays(-1));
            dateArrivee.BlackoutDates.Add(blackoutDays);

            // Définit la date actuelle comme date sélectionnée par défaut
            dateArrivee.SelectedDate = DateTime.Now;

            // gestionnaire pour détecter la fermeture du calendrier et mettre à jour les dates bloquées
            dateArrivee.CalendarClosed += new RoutedEventHandler(SelectDateChangeEvent);

            // Appliquer les dates bloquées
            dateSortie.BlackoutDates.Add(blackoutDays);

            // gestionnaire pour le bouton qui calcule et affiche la durée du séjour
            btnDuree.Click += new RoutedEventHandler(Duree_Click);
        }

        // Vérifier si l'input est entre 1 et 6
        public void VerifInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int value))
            {
                e.Handled = true;
            }
            if (txtboxPersonnes.Text.Length > 0)
            {
                e.Handled = true;
            }
            if (!(value >= 1 && value <= 6))
            {
                e.Handled = true;
            }
        }
        // Elle calcule la durée du séjour en semaines entre la date de sortie et la date d'arrivée
        public void Duree_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan semaines = dateSortie.SelectedDate.Value - dateArrivee.SelectedDate.Value;
            semaine.Text = (semaines.Days / 7).ToString();

        }

        // Eviter qu'on puisse choisir une date avant le jour d'arrivée dans le jour de sortie
        public void SelectDateChangeEvent(object sender, RoutedEventArgs e)
        {
            CalendarDateRange blackoutDays = new CalendarDateRange(new DateTime(0001, 01, 01), dateArrivee.SelectedDate.Value.Date);
            dateSortie.BlackoutDates.Clear();
            dateSortie.BlackoutDates.Add(blackoutDays);
        }
    }
}