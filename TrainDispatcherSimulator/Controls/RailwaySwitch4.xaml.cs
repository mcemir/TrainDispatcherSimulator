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


        public override RailwayBase GetLeftRailway()
        {
            if (State == RailwaySwitchState.Straight)
                return LeftRailways[0];
            else
                return LeftRailways[1];
        }


        public override bool Reserve(RailwayBase previousRailway, RailwayBase nextRailway, bool highlight = false)
        {
            if (base.Reserve(previousRailway, nextRailway, highlight))
            {
                if (previousRailway == null && nextRailway == null)
                    return true;

                int indexR = 0;
                int indexL = 0;
                bool found = false;

                if (RightRailways.Contains(previousRailway) && LeftRailways.Contains(nextRailway))
                {
                    indexL = LeftRailways.IndexOf(nextRailway);
                    found = true;
                }
                else if (RightRailways.Contains(nextRailway) && LeftRailways.Contains(previousRailway))
                {
                    indexL = LeftRailways.IndexOf(previousRailway);
                    found = true;
                }

                if (found)
                {
                    if (indexL == 0)
                        State = RailwaySwitchState.Straight;
                    else
                        State = RailwaySwitchState.Sverve;
                }
                else
                {
                    Reset();
                }

                return found;
            }

            return false;
        }

        #endregion PUBLIC METHODS
    }
}
