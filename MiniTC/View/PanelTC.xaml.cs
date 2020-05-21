using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MiniTC
{
    /// <summary>
    /// Interaction logic for PanelTC.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {

        public PanelTC()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DyskiProperty =
            DependencyProperty.Register(nameof(Dyski), typeof(string[]),typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ListaProperty =
            DependencyProperty.Register(nameof(Lista), typeof(List<string>), typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty WoluminProperty =
            DependencyProperty.Register(nameof(Wolumin), typeof(string), typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PlikProperty =
            DependencyProperty.Register(nameof(Plik), typeof(string), typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SciezkaProperty =
            DependencyProperty.Register(nameof(Sciezka), typeof(string), typeof(PanelTC),
                new FrameworkPropertyMetadata(null));


        public string[] Dyski
        {
            get { return (string[])GetValue(DyskiProperty); }
            set { SetValue(DyskiProperty, value); }
        }

        public string Wolumin
        {
            get { return (string)GetValue(WoluminProperty); }
            set { SetValue(WoluminProperty, value); }
        }

        public List<string> Lista
        {
            get { return (List<string>)GetValue(ListaProperty); }
            set { SetValue(ListaProperty, value); }
        }

        public string Plik
        {
            get { return (string)GetValue(WoluminProperty); }
            set { SetValue(WoluminProperty, value); }
        }

        public string Sciezka
        {
            get { return (string)GetValue(SciezkaProperty); }
            set { SetValue(SciezkaProperty, value); }
        }

        //Rodzaj panelu
        public static readonly RoutedEvent PanelEvent =
            EventManager.RegisterRoutedEvent(nameof(Panel),
                         RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                         typeof(PanelTC));

        public event RoutedEventHandler Panel
        {
            add { AddHandler(PanelEvent, value); }
            remove { RemoveHandler(PanelEvent, value); }
        }

        //podwójne wciśniecie myszki
        public static readonly RoutedEvent DoubleClickedEvent =
            EventManager.RegisterRoutedEvent(nameof(DoubleClicked),
                         RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                         typeof(PanelTC));

        public event RoutedEventHandler DoubleClicked
        {
            add { AddHandler(DoubleClickedEvent, value); }
            remove { RemoveHandler(DoubleClickedEvent, value); }
        }

        private void listOfFiles_GotFocus(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(PanelEvent));
        }
        private void listOfFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(DoubleClickedEvent));
        }
    }
}
