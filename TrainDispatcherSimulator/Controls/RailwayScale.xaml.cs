using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MarkingItem.xaml
    /// </summary>
    public partial class RailwayScale : UserControl
    {

        public RailwayScale()
        {
            InitializeComponent();
        }


        #region PROPERTIES
        public string MeasueredWeight
        {
            get { return railwayScaleBlock.Text; }
            set { railwayScaleBlock.Text = value; }
        }
        #endregion PROPERTIES

    }
}
