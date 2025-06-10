// ========================================
// ViewModels/MainViewModel.cs - Logique métier
// ========================================
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Windows;

namespace Prism.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchText = "";
        private SearchResult _selectedResult;
        private ObservableCollection<SearchResult> _searchResults;
        private ObservableCollection<SearchResult> _allResults;

        public MainViewModel()
        {
            InitializeData();
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterResults();
            }
        }

        public SearchResult SelectedResult
        {
            get => _selectedResult;
            set
            {
                _selectedResult = value;
                OnPropertyChanged(nameof(SelectedResult));
            }
        }

        public ObservableCollection<SearchResult> SearchResults
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }

        public void SelectNext()
        {
            if (!SearchResults.Any()) return;

            int currentIndex = SearchResults.IndexOf(SelectedResult);
            int nextIndex = (currentIndex + 1) % SearchResults.Count;

            SelectedResult = SearchResults[nextIndex];
        }

        public void SelectPrevious()
        {
            if (!SearchResults.Any()) return;

            int currentIndex = SearchResults.IndexOf(SelectedResult);
            int prevIndex = currentIndex <= 0 ? SearchResults.Count - 1 : currentIndex - 1;

            SelectedResult = SearchResults[prevIndex];
        }

        public void ExecuteSelected()
        {
            if (SelectedResult == null) return;

            try
            {
                switch (SelectedResult.Title.ToLower())
                {
                    case "calculatrice":
                        System.Diagnostics.Process.Start("calc.exe");
                        break;
                    case "bloc-notes":
                        System.Diagnostics.Process.Start("notepad.exe");
                        break;
                    case "paint":
                        System.Diagnostics.Process.Start("mspaint.exe");
                        break;
                    case "explorateur de fichiers":
                        System.Diagnostics.Process.Start("explorer.exe");
                        break;
                    case "paramètres":
                        System.Diagnostics.Process.Start("ms-settings:");
                        break;
                    case "panneau de configuration":
                        System.Diagnostics.Process.Start("control.exe");
                        break;
                    default:
                        MessageBox.Show($"Fonction non implémentée pour : {SelectedResult.Title}",
                                      "Prism", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du lancement : {ex.Message}",
                              "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Déclencher l'événement pour fermer la fenêtre
            ApplicationExit?.Invoke();
        }

        public event Action ApplicationExit;

        private void InitializeData()
        {
            _allResults = new ObservableCollection<SearchResult>
            {
                new SearchResult { Title = "Calculatrice", Subtitle = "Application Windows", Type = "App" },
                new SearchResult { Title = "Bloc-notes", Subtitle = "Éditeur de texte", Type = "App" },
                new SearchResult { Title = "Paint", Subtitle = "Éditeur d'images", Type = "App" },
                new SearchResult { Title = "Chrome", Subtitle = "Navigateur web", Type = "App" },
                new SearchResult { Title = "Visual Studio", Subtitle = "IDE de développement", Type = "App" },
                new SearchResult { Title = "Explorateur de fichiers", Subtitle = "Gestionnaire de fichiers", Type = "App" },
                new SearchResult { Title = "Paramètres", Subtitle = "Configuration système", Type = "App" },
                new SearchResult { Title = "Panneau de configuration", Subtitle = "Configuration avancée", Type = "App" },
            };

            SearchResults = new ObservableCollection<SearchResult>(_allResults);

            if (SearchResults.Any())
            {
                SelectedResult = SearchResults.First();
            }
        }

        private void FilterResults()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                SearchResults = new ObservableCollection<SearchResult>(_allResults);
            }
            else
            {
                var filtered = _allResults
                    .Where(r => r.Title.ToLower().Contains(SearchText.ToLower()) ||
                               r.Subtitle.ToLower().Contains(SearchText.ToLower()))
                    .ToList();

                SearchResults = new ObservableCollection<SearchResult>(filtered);
            }

            if (SearchResults.Any())
            {
                SelectedResult = SearchResults.First();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}