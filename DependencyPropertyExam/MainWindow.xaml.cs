using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1),
                                                        DispatcherPriority.Normal,
                                                        delegate
                                                        {
                                                            int newvalue = 0;
                                                            if (Counter == int.MaxValue)
                                                            {
                                                                newvalue = 0;
                                                            }
                                                            else
                                                            {
                                                                newvalue = Counter + 1;
                                                            }
                                                            SetValue(CounterProperty, newvalue);

                                                        }, Dispatcher);
        }

        public int Counter
        {
            get { return (int)GetValue(CounterProperty); }
            set { SetValue(CounterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Counter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CounterProperty =
            DependencyProperty.Register("Counter", typeof(int), typeof(MainWindow), new PropertyMetadata(1));

        public string MyFruit
        {
            get { return (string)GetValue(MyFruitProperty); }
            set { SetValue(MyFruitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyFruit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyFruitProperty =
            DependencyProperty.Register("MyFruit", typeof(string), typeof(MainWindow),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnMyPropertyChanged)));

        public static void OnMyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow win = d as MainWindow;

            win.Title = (e.OldValue == null) ? "No selected fruit" : $"Previous selected file {e.OldValue.ToString()}";
            win.uiTb_Fruit.Text = e.NewValue.ToString();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
            MyFruit = item.Content.ToString();
        }
    }
}
