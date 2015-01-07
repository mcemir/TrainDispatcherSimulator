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
using TrainDispatcherSimulator.Controls;
using TrainDispatcherSimulator.Helpers;

namespace TrainDispatcherSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Controller.Instance.logger.Add(new Log("Test", LogType.Info, DateTime.Now));
            Controller.Instance.logger.Add(new Log("Medo je bio ovdje i izvrnuo stol!", LogType.Info, DateTime.Now));
            Controller.Instance.logger.Add(new Log("Meho je sve zezno!", LogType.Error, DateTime.Now));
            this.KeyUp += new KeyEventHandler(keyboardKey_Up);

        }

        private void keyboardKey_Up(object sender, KeyEventArgs e)
        {
            Controller.Instance.RegisterKeyPressed(e);
        }

        private void graphButton_Click(object sender, RoutedEventArgs e)
        {
            railwayGrid.Visibility = Visibility.Collapsed;
            graphGrid.Visibility = Visibility.Visible;
            logGrid.Visibility = Visibility.Collapsed;
        }

        private void trainButton_Click(object sender, RoutedEventArgs e)
        {
            railwayGrid.Visibility = Visibility.Visible;
            graphGrid.Visibility = Visibility.Collapsed;
            logGrid.Visibility = Visibility.Collapsed;

        }

        private void logButton_Click(object sender, RoutedEventArgs e)
        {

            logGrid.Visibility = Visibility.Visible;
            railwayGrid.Visibility = Visibility.Collapsed;
            graphGrid.Visibility = Visibility.Collapsed;
            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Controller.Instance.Railways = railwayGrid.Children.Cast<UIElement>().Where(p => p is RailwayBase).Cast<RailwayBase>().ToList();
            Controller.Instance.LogDataGrid = logDataGrid;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var gridData = sender as DataGrid;
            gridData.ItemsSource = Controller.Instance.logger.logs;
        }

    }
}
