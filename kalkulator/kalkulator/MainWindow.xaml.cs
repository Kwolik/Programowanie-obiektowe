using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace kalkulator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal result = 0;  //wynik
        char operation = '+';        //operator

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonClick_numbers(object sender, RoutedEventArgs e) // Cyfry
        {
            Button button = (Button)sender;
            string action = label_action.Content.ToString();
            string main = label_main.Text.ToString();

            if (main.Contains("=")) // Reset
                buttonClick_Clear(sender, e);

            if (action == "0")
                label_action.Content = button.Content.ToString();

            if (action != "0")
                label_action.Content += button.Content.ToString();

            if (label_score.Content.ToString() == "Dzielenie przez zero!")
                label_score.Content = "";

            if (action == "-" && button.Content.ToString() == "0")
                label_action.Content = "0";
        }

        private void buttonClick_Clear(object sender, RoutedEventArgs e) // Czyszczenie wszystkich elementów
        {
            result = 0;
            label_main.Text = "";
            label_action.Content = "";
            label_score.Content = "";
        }

        private void buttonClick_Opp(object sender, RoutedEventArgs e) // +/-
        {
            string action = label_action.Content.ToString();

            if (action.ToCharArray().ElementAt(0) != '-' && action != "0")
                label_action.Content = "-" + action;

            if (action != "" && action.ToCharArray().ElementAt(0) == '-')
                label_action.Content = action.Remove(0, 1);
        }

        private void buttonClick_Comma(object sender, RoutedEventArgs e) // przecinek
        {
            string action = label_action.Content.ToString();
            string main = label_main.Text.ToString();
            bool sign = action.Contains(',');

            if (action == "")
                label_action.Content = "0,";

            if (action != "" && sign == false)
                label_action.Content += ",";
        }

        private void Operations(decimal action, char operation) // Działania
        {
            if (operation == '+')
                result += action;

            if (operation == '-')
                result -= action;

            if (operation == '×')
                result *= action;

            if (operation == '÷' && action != 0)
                result /= action;

            if (operation == '÷' && action == 0)
                label_score.Content = "Dzielenie przez zero!";
        }

        private void buttonClick_Operation(object sender, RoutedEventArgs e) // + - * /
        {
            Button b = (Button)sender;
            operation = Convert.ToChar(b.Content);
            string action = label_action.Content.ToString();
            string main = label_main.Text.ToString();
            string[] a = label_main.Text.ToString().Split(' ');
            char operation2;

            if (action != "" && action.Last() != ',' || action == "" && main != "")
            {
                label_action.Content = "";
                label_main.Text += action + " " + operation + " ";

                if (main.Contains("=") || main.Contains("-") || main.Contains("+") || main.Contains("÷") || main.Contains("×"))
                {
                    string temp = result.ToString();
                    buttonClick_Clear(sender, e);
                    label_main.Text = temp + " " + b.Content + " ";
                    result = Convert.ToDecimal(temp);
                }

                else
                {
                    if (main != "" && a.Length > 4)
                    {
                        operation2 = Convert.ToChar(a[a.Length - 4]);
                        Operations(Convert.ToDecimal(action), operation2);
                    }
                }

                if (action != "" && main == "")
                    result = Convert.ToDecimal(action);

                if (label_score.Content.ToString() != "Dzielenie przez zero!")
                    label_score.Content = result;
                else
                    Zero();

                if (result.ToString() == "-0")
                    result = 0;
            }
        }

        private void buttonClick_Back(object sender, RoutedEventArgs e) // Cofanie
        {
            string action = label_action.Content.ToString();

            if (action != "")
                label_action.Content = action.Remove(action.Length - 1, 1);
        }

        private void buttonClick_Equals(object sender, RoutedEventArgs e) // =
        {
            string main = label_main.Text.ToString();
            string action = label_action.Content.ToString();
            char operation2;
            string result_string;

            if (action != "" && main.Contains("=")) // Jeśli jest wynik, po wciśnieciu = wykonuje się ostatnie działanie
            {
                result_string = result.ToString();
                Operations(Convert.ToDecimal(action), operation);
                label_main.Text = result_string + " " + operation + " " + action + " = " + result;
                label_score.Content = result;
            }

            else
            {
                if(main != "" && action !="" && (main.Contains("-") || main.Contains("+") || main.Contains("÷") || main.Contains("×")))
                {
                    operation2 = Convert.ToChar(main.ElementAt(main.Length - 2));
                    decimal act = Convert.ToDecimal(label_action.Content);
                    if (operation2 == '-' || operation2 == '+' || operation2 == '×' || operation2 == '÷')
                    {
                        main = main.Remove(main.Length - 2, 1);
                        Operations(act, operation2);
                    }
                    label_main.Text = main + operation2 + " " + action + " = " + result;
                }
                else
                {
                    if (label_score.Content.ToString() != "Dzielenie przez zero!" && action != "")
                        result = Convert.ToDecimal(action);
                }

                if(main != "" && action == "")
                {
                    string tmp = result.ToString();
                    string[] str = main.Split(' ');
                    string m = Convert.ToString(str[str.Length - 3]);
                    operation2 = operation;
                    main = main.Remove(2);
                    Operations(Convert.ToDecimal(m), operation2);
                    label_main.Text = tmp + " " + operation2 + " " + m + " = " + result;
                }

                if (label_score.Content.ToString() != "Dzielenie przez zero!")
                    label_score.Content = result;
                else
                    Zero();
            }
        }
        private void Zero() // dzielenie przez zero
        {
            label_action.Content = "";
            label_main.Text = "";
            label_score.Content = "Dzielenie przez zero!"; 
        }
    }
}