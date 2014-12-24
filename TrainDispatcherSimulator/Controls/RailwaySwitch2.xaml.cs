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
    /// Interaction logic for RailwaySwitch2.xaml
    /// </summary>
    public partial class RailwaySwitch2 : RailwaySwitchBase
    {
        public RailwaySwitch2()
        {
            InitializeComponent();
        }





        #region PUBLIC METHODS

        public override RailwayBase GetRightRailway()
        {
            if (State == RailwaySwitchState.Straight)
                return RightRailways[1];
            else
                return RightRailways[0];
        }


        #endregion PUBLIC METHODS
    }
}
