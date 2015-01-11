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
        }



        #region VISIBILITY MANAGEMENT
        private void graphButton_Click(object sender, RoutedEventArgs e)
        {
            collapseAll();
            graphGrid.Visibility = Visibility.Visible;
        }

        private void trainButton_Click(object sender, RoutedEventArgs e)
        {
            collapseAll();
            mainRailwayGrid.Visibility = Visibility.Visible;
        }

        private void tableButton_Click(object sender, RoutedEventArgs e)
        {
            collapseAll();
            mainTableGrid.Visibility = Visibility.Visible;
        }
        private void logButton_Click(object sender, RoutedEventArgs e)
        {
            collapseAll();
            logGrid.Visibility = Visibility.Visible;
        }

        private void collapseAll()
        {
            logGrid.Visibility = Visibility.Collapsed;
            mainRailwayGrid.Visibility = Visibility.Collapsed;
            graphGrid.Visibility = Visibility.Collapsed;
            mainTableGrid.Visibility = Visibility.Collapsed;
        }
        #endregion VISIBILITY MANAGEMENT



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Controller.Instance.Railways = railwaysGrid.Children.Cast<UIElement>().Where(p => p is RailwayBase).Cast<RailwayBase>().ToList();
            Controller.Instance.LogDataGrid = logDataGrid;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var gridData = sender as DataGrid;
            gridData.ItemsSource = Controller.Instance.logger.logs;
        }



    }
}
