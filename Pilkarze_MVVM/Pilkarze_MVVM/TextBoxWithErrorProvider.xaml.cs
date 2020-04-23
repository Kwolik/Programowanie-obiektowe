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

namespace Pilkarze_MVVM
{
    /// <summary>
    /// Interaction logic for TextBoxWithErrorProvider.xaml
    /// </summary>
    public partial class TextBoxWithErrorProvider : UserControl
    {
        public static readonly RoutedEvent TextChangedEvent =
           EventManager.RegisterRoutedEvent("Text_Changed", RoutingStrategy.Bubble,
               typeof(RoutedEventHandler), typeof(TextBoxWithErrorProvider));

        public event RoutedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        void RaiseTextChanged()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(TextChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxWithErrorProvider),
                new FrameworkPropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb.Text == "")
                tb.ToolTip = "Puste pole!";
            else if (tb.Text == "Podaj imię" || tb.Text == "Podaj nazwisko")
                tb.ToolTip = "Uzupełnij pole!";
            else if (!tb.Text.All(Char.IsLetter))
                tb.ToolTip = "Niedozwolone znaki!";
            else
                tb.ToolTip = "Wszystko w porządku!";

            RaiseTextChanged();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            tb.BorderBrush = Brushes.Gray;

            if (tb.Text == "Podaj imię" || tb.Text == "Podaj nazwisko")
                tb.Text = "";

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb.Text == "")
                tb.BorderBrush = Brushes.Red;
        }


        public TextBoxWithErrorProvider()
        {
            InitializeComponent();
        }

    }
}
