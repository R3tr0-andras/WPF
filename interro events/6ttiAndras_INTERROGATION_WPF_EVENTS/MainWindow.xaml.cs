using System.Reflection;
using System;
using System.Windows;
using System.Windows.Controls;
using I_WPF_CalcBin;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace _6ttiAndras_INTERROGATION_WPF_EVENTS
{
    /// <summary>
    /// De vous à moi ça ne fonctionne pas et je n'y arriverais pas, je vais donc allez sur le projet...
    /// je me ratraperais sur la prochaine plutot que de rester 1h de plus à ne pas savoir pourquoi ça ne marche pas.
    /// bonne correction pour les autres, et si je suis le dernier bah bravo vous avez enfin finit ça mérite une tarte aux pommes.
    /// 👍👍👍👍👍👍👍👍👍👍👍👍👍
    /// </summary>

    public partial class MainWindow : Window
    {
        public MethodesDuProjet projet = new MethodesDuProjet();
        public MainWindow()
        {
            txtNombre1.PreviewTextInput += new TextCompositionEventHandler(VerifInput);
            txtNombre2.PreviewTextInput += new TextCompositionEventHandler(VerifInput);
            btnCalculer.Click += new RoutedEventHandler(Calculer());
            btnReset.Click += new RoutedEventHandler(Reset());
            InitializeComponent();
            RadioButton optAddition;
            RadioButton optSoustraction;

    }
        // Vérifier si l'input est entre 1 et 7
        public void VerifInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int value))
            {
                e.Handled = true;
            }
            if (txtNombre1.Text.Length > 0)
            {
                e.Handled = true;
            }
            if (!(value <= 7))
            {
                e.Handled = true;
            }
        }

        private bool Verify(string textNombre) 
        {
            bool bol = true;
            if (textNombre.Length <= 7)
            {
                for (int i = 0; i < textNombre.Length; i++)
                {
                    if (textNombre[i] != '0' && textNombre[i] == '1')
                    {
                        // Caractère invalide
                        MessageBox.Show("Caractère invalide merci de ne mettre que des 1 et des 0");
                        bol = false;
                    }
                }
            } else
            {
                //trop de caractère
                MessageBox.Show("C'est beaucoup trop, merci de ne pas dépacer la limite de 7");
                bol = false;
            }
            return bol;
        }

        private void Calculer(object sender, RoutedEvent e)
        {
            

        }
    }
}