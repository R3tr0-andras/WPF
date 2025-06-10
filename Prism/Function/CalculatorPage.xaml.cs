using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Prism.Function
{
    public partial class CalculatorPage : UserControl, INotifyPropertyChanged
    {
        private string _displayValue = "0";
        private double _firstNumber = 0;
        private double _secondNumber = 0;
        private string _operation = "";
        private bool _isNewNumber = true;

        public CalculatorPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string DisplayValue
        {
            get => _displayValue;
            set
            {
                _displayValue = value;
                OnPropertyChanged(nameof(DisplayValue));
            }
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string number = button.Content.ToString();

            if (_isNewNumber)
            {
                DisplayValue = number;
                _isNewNumber = false;
            }
            else
            {
                if (DisplayValue == "0")
                    DisplayValue = number;
                else
                    DisplayValue += number;
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string op = button.Tag.ToString();

            if (!_isNewNumber)
            {
                if (!string.IsNullOrEmpty(_operation))
                {
                    Calculate();
                }
                else
                {
                    _firstNumber = double.Parse(DisplayValue);
                }
            }

            _operation = op;
            _isNewNumber = true;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
            _operation = "";
            _isNewNumber = true;
        }

        private void Calculate()
        {
            if (string.IsNullOrEmpty(_operation)) return;

            try
            {
                _secondNumber = double.Parse(DisplayValue);
                double result = 0;

                switch (_operation)
                {
                    case "+":
                        result = _firstNumber + _secondNumber;
                        break;
                    case "-":
                        result = _firstNumber - _secondNumber;
                        break;
                    case "*":
                        result = _firstNumber * _secondNumber;
                        break;
                    case "/":
                        if (_secondNumber != 0)
                            result = _firstNumber / _secondNumber;
                        else
                        {
                            DisplayValue = "Erreur";
                            return;
                        }
                        break;
                }

                DisplayValue = result.ToString();
                _firstNumber = result;
            }
            catch
            {
                DisplayValue = "Erreur";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            DisplayValue = "0";
            _firstNumber = 0;
            _secondNumber = 0;
            _operation = "";
            _isNewNumber = true;
        }

        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (DisplayValue != "0")
            {
                if (DisplayValue.StartsWith("-"))
                    DisplayValue = DisplayValue.Substring(1);
                else
                    DisplayValue = "-" + DisplayValue;
            }
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            double value = double.Parse(DisplayValue);
            DisplayValue = (value / 100).ToString();
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewNumber)
            {
                DisplayValue = "0,";
                _isNewNumber = false;
            }
            else if (!DisplayValue.Contains(","))
            {
                DisplayValue += ",";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}