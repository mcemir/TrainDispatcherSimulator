﻿using System;
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

        private void graphButton_Click(object sender, RoutedEventArgs e)
        {
            railwayGrid.Visibility = Visibility.Collapsed;
            graphGrid.Visibility = Visibility.Visible;
        }

        private void trainButton_Click(object sender, RoutedEventArgs e)
        {
            railwayGrid.Visibility = Visibility.Visible;
            graphGrid.Visibility = Visibility.Collapsed;

        }

        private void logButton_Click(object sender, RoutedEventArgs e)
        {
            railwayGrid.Visibility = Visibility.Visible;
            graphGrid.Visibility = Visibility.Collapsed;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Controller.Instance.Railways = railwayGrid.Children.Cast<UIElement>().Where(p => p is RailwayBase).Cast<RailwayBase>().ToList();
        }

    }
}
