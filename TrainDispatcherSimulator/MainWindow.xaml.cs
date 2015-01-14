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
            Controller.Instance.showGraph();
        }

        private void trainButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.Instance.showRailway();
        }

        private void tableButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.Instance.showTableGrid();
        }
        private void logButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.Instance.showLogger();
        }
        #endregion VISIBILITY MANAGEMENT



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Controller.Instance.Railways = railwaysGrid.Children.Cast<UIElement>().Where(p => p is RailwayBase).Cast<RailwayBase>().ToList();
            Controller.Instance.LogDataGrid = logDataGrid;
            Controller.Instance.ScheduleDataGrid = scheduleDataGrid;

            Controller.Instance.InitSchedule();

            logDataGrid.ItemsSource = Controller.Instance.logger.logs;
            scheduleDataGrid.ItemsSource = Controller.Instance.ScheduleList;
            scheduleDataGrid.SelectedItem = scheduleDataGrid.Items[3];

        }




        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainTableGrid.Width = e.NewSize.Width-20;
        }



    }
}
