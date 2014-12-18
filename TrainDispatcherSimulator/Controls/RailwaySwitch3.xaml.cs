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

namespace TrainDispatcherSimulator.Controls
{
    /// <summary>
    /// Interaction logic for RailwaySwitch3.xaml
    /// </summary>
    public partial class RailwaySwitch3 : RailwaySwitchBase
    {
        public RailwaySwitch3()
        {
            InitializeComponent();
            this.StateChanged += RailwaySwitch3_StateChanged;
        }

        void RailwaySwitch3_StateChanged(object sender, EventArgs e)
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
