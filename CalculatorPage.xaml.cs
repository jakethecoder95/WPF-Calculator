using System;
using System.Linq;
using System.Text;
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
        private string firstNumber { get; set; } = "0";
        private string secondNumber { get; set; } = "0";
        private string chosenOpperator { get; set; } = "0";
        private bool operatorWasChosen { get; set; } = false;
        private int step { get; set; } = 1; // 1 == choosing first val,  2 == choosing second val

        public CalculatorPage()
        {
            InitializeComponent();

            topDisplay.Text = "";
            bottomDisplay.Text = "0";

            equals.Click += Equals;
            delete.Click += Delete;
            clearAll.Click += ClearAll;
            clear.Click += Clear;
            negativePositiveToggle.Click += NegativePositiveToggle;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var buttonValue = (e.Source as Button).Content.ToString();

            if (isActionOperator(buttonValue))
                return;

            if (isCalcOperator(buttonValue))
            { 
                if (displayingResult)
                {
                    firstNumber = bottomDisplay.Text;
                    bottomDisplay.Text = "0";
                    secondNumber = "0";
                    displayingResult = false;
                }

                if (step == 1)
                {
                    chosenOpperator = buttonValue;
                    operatorWasChosen = true;

                    topDisplay.Text = firstNumber + chosenOpperator;
                    bottomDisplay.Text = "0";
                    step++;
                }
                else
                {
                    firstNumber = Calculate().ToString();
                    secondNumber = "0";

                    chosenOpperator = buttonValue;
                    operatorWasChosen = true;

                    topDisplay.Text = firstNumber + chosenOpperator;
                    bottomDisplay.Text = secondNumber;
                }

                return;
            }

            string newDisplayNumber;
            if (displayingResult)
            {
                newDisplayNumber = buttonValue;
                displayingResult = false;
            }
            else if (step == 1)
                newDisplayNumber = firstNumber = updateNumber(buttonValue);
            else
                newDisplayNumber = secondNumber = updateNumber(buttonValue);

            bottomDisplay.Text = newDisplayNumber;            
        }

        private float Calculate()
        {
            var first = float.Parse(firstNumber);
            var second = float.Parse(secondNumber);
            float answer;

            if (chosenOpperator == "+")
                answer = first + second;
            else if (chosenOpperator == "-")
                answer = first - second;
            else if (chosenOpperator == "×")
                answer = first * second;
            else
                answer = second != 0 ? first / second : 0;

            return answer;
        }

        private void Equals(object sender, RoutedEventArgs e)
        {
            float answer = Calculate();

            topDisplay.Text = firstNumber + chosenOpperator + secondNumber + "=";
            bottomDisplay.Text = answer.ToString();
            displayingResult = true;
            operatorWasChosen = false;
        }

        private string updateNumber(string numberUpdate)
        {
            if (bottomDisplay.Text == "0")
                return numberUpdate;

            return bottomDisplay.Text + numberUpdate;
        }

        private void NegativePositiveToggle(object sender, RoutedEventArgs e)
        {
            string activeNumber = step == 1 ? firstNumber : secondNumber;
            string newNumber = activeNumber[0] == '-' ? activeNumber.Substring(1) : "-" + activeNumber;

            bottomDisplay.Text = newNumber;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            string newValue = bottomDisplay.Text.Length > 1 ? bottomDisplay.Text.Substring(0, bottomDisplay.Text.Length - 1) : "0";

            if (step == 1)
                firstNumber = newValue;
            else
                secondNumber = newValue;

            displayingResult = false;
            bottomDisplay.Text = newValue;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            bottomDisplay.Text = "0";
            topDisplay.Text = "";

            if (step == 1)
                firstNumber = "0";
            else
                secondNumber = "0";

            displayingResult = false;
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            firstNumber = "0";
            secondNumber = "0";
            topDisplay.Text = "";
            bottomDisplay.Text = firstNumber;
            displayingResult = false;
            step = 1;
        }

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
