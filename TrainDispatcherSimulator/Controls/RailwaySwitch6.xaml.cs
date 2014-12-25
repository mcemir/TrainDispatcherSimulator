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
    /// Interaction logic for RailwaySwitch6.xaml
    /// </summary>
    public partial class RailwaySwitch6 : RailwaySwitchBase
    {
        public RailwaySwitch6()
        {
            InitializeComponent();
        }


        #region PUBLIC METHODS

        // Ovo nije dobro treba mijenjat
        public override RailwayBase GetRightRailway(RailwayBase referent = null)
        {
            if (State == RailwaySwitchState.Straight)
                return RightRailways[0];
            else
                return RightRailways[1];
        }

        // Ovo nije dobro treba mijenjat
        public override RailwayBase GetLeftRailway(RailwayBase referent = null)
        {
            if (State == RailwaySwitchState.Straight)
                return RightRailways[0];
            else
                return RightRailways[1];
        }
        #endregion PUBLIC METHODS
    }
}
