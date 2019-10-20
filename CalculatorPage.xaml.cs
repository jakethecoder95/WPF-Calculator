using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for CalculatorPage.xaml
    /// </summary>
    public partial class CalculatorPage : Page
    {
        private bool displayingResult { get; set; }

        public CalculatorPage()
        {
            InitializeComponent();

            display.Text = "0";
            equals.Click += Calculate;
            delete.Click += Delete;
            clear.Click += Clear;
            clear2.Click += Clear;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var buttonValue = (e.Source as Button).Content.ToString();

            if (isActionOperator(buttonValue))
                return;

            if ((displayingResult || display.Text == "0") && !isCalcOperator(buttonValue))
            {
                display.Text = buttonValue;
                displayingResult = false;
            }
            else
                display.Text += buttonValue;
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            string[] values = display.Text.Split('+', '-', '×', '÷');

            var firstNumber = float.Parse(values[0]);
            var secondNumber = float.Parse(values[1]);
            float answer;

            if (display.Text.Contains('+'))
                answer = firstNumber + secondNumber;
            else if (display.Text.Contains('-'))
                answer = firstNumber - secondNumber;
            else if (display.Text.Contains('×'))
                answer = firstNumber * secondNumber;
            else if (display.Text.Contains('÷') && secondNumber != 0)
                answer = firstNumber / secondNumber;
            else
            {
                display.Text = "Error";
                return;
            }

            display.Text = answer.ToString();
            displayingResult = true;
        }

        private void Delete(object sender, RoutedEventArgs e) =>
            display.Text = display.Text.Length > 1 ? display.Text.Substring(0, display.Text.Length - 1) : "0";

        private void Clear(object sender, RoutedEventArgs e) => display.Text = "0";

        private bool isActionOperator(string value)
        {
            switch (value)
            {
                case "CE":
                    return true;
                case "C":
                    return true;
                case "Del":
                    return true;
                case "=":
                    return true;
                case "-/+":
                    return true;
                default:
                    return false;
            }
        }

        private bool isCalcOperator(string value)
        {
            switch (value)
            {
                case "+":
                    return true;
                case "-":
                    return true;
                case "×":
                    return true;
                case "÷":
                    return true;
                default:
                    return false;
            }
        }
    }
}
