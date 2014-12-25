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
    /// Interaction logic for RailwaySwitch4.xaml
    /// </summary>
    public partial class RailwaySwitch4 : RailwaySwitchBase
    {
        public RailwaySwitch4()
        {
            InitializeComponent();
        }





        #region PUBLIC METHODS

        public override RailwayBase GetLeftRailway(RailwayBase referent = null)
        {
            if (State == RailwaySwitchState.Straight)
                return LeftRailways[0];
            else
                return LeftRailways[1];
        }


        #endregion PUBLIC METHODS
    }
}
