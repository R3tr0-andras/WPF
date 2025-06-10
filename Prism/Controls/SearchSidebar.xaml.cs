using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.ViewModels;

namespace Prism.Controls
{
    public partial class SearchSidebar : UserControl
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public SearchSidebar()
        {
            InitializeComponent();
        }

        public void FocusSearchBox()
        {
            SearchTextBox?.Focus();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (ViewModel == null) return;

            switch (e.Key)
            {
                case Key.Down:
                    ViewModel.SelectNext();
                    ScrollSelectedIntoView();
                    e.Handled = true;
                    break;
                case Key.Up:
                    ViewModel.SelectPrevious();
                    ScrollSelectedIntoView();
                    e.Handled = true;
                    break;
                case Key.Enter:
                    ViewModel.ExecuteSelected();
                    e.Handled = true;
                    break;
                case Key.Escape:
                    CloseApplication();
                    e.Handled = true;
                    break;
            }
        }

        private void ResultsListBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (ViewModel == null) return;

            if (e.Delta > 0)
            {
                ViewModel.SelectPrevious();
            }
            else if (e.Delta < 0)
            {
                ViewModel.SelectNext();
            }

            ScrollSelectedIntoView();
            e.Handled = true;
        }

        private void ScrollSelectedIntoView()
        {
            if (ViewModel?.SelectedResult != null)
            {
                ResultsListBox.ScrollIntoView(ViewModel.SelectedResult);
                ResultsListBox.SelectedItem = ViewModel.SelectedResult;
            }
        }

        private void CloseApplication()
        {
            // Trouver la fenêtre parent et la fermer
            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Hide(); // ou Close()
        }
    }
}
