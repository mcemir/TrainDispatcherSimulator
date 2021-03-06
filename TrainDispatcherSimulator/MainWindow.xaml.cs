﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        #region INITIALIZATION
        public MainWindow()
        {
            InitializeComponent();
        }

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
        #endregion INITIALIZATION


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





        #region PRIVATE METHODS
        private void exportLog()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.SelectedPath + "\\log.txt";
                
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(path))
                {
                }

                using (StreamWriter sw = File.AppendText(path))
                {
                    List<Log> logs = Controller.Instance.logger.logs;
                    for (int i = logs.Count - 1; i >= 0; i--)
                    {
                        sw.WriteLine(logs[i].Timestamp + "\t" + logs[i].LogType.ToString() + "\t" + logs[i].Content);
                    }
                    
                }

                MessageBox.Show("The log has beeen sucessfully exported to the selected location.", "Export successful", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }
        #endregion PRIVATE METHODS






        #region EVENT HANDLERS
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainTableGrid.Width = e.NewSize.Width-20;
        }

        private void railwayNamesCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            Visibility railwayVisisbility = Visibility.Visible;
            if (!((bool)railwayNamesCheckBox.IsChecked))
                railwayVisisbility = Visibility.Collapsed;

            foreach (RailwayBase railway in Controller.Instance.Railways)
                railway.RailwayNameVisibility = railwayVisisbility;
        }

        private void reserveButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.Instance.ReserveSelected();
        }

        private void clearSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.Instance.ClearSelected();
        }

        private void resetRailwayButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.Instance.Reset();
        }

        private void alarmButtonChecked(object sender, RoutedEventArgs e)
        {
            string content = (string)((sender as ToggleButton).Content);
            Controller.Instance.Log("Alarm " + content + " activated", LogType.Info);
        }

        private void alarmButtonUnchecked(object sender, RoutedEventArgs e)
        {
            string content = (string)((sender as ToggleButton).Content);
            Controller.Instance.Log("Alarm " + content + " deactivated", LogType.Info);
        }

        private void exportLogButton_Click(object sender, RoutedEventArgs e)
        {
            exportLog();
        }

        #endregion EVENT HANDLERS

        

        

        


    }
}
