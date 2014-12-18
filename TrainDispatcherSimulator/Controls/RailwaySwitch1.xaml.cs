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

namespace TrainDispatcherSimulator.Controls
{
    /// <summary>
    /// Interaction logic for RailwaySwitch1.xaml
    /// </summary>
    public partial class RailwaySwitch1 : RailwaySwitchBase
    {
        public RailwaySwitch1()
        {
            InitializeComponent();
            this.StateChanged += RailwaySwitch1_StateChanged;
        }

        void RailwaySwitch1_StateChanged(object sender, EventArgs e)
        {
            if (State == RailwaySwitchState.Straight)
            {
                switchStraight.Visibility = Visibility.Visible;
                switchSverve.Visibility = Visibility.Collapsed;
            }
            else
            {
                switchStraight.Visibility = Visibility.Collapsed;
                switchSverve.Visibility = Visibility.Visible;
            }
        }
    }
}
