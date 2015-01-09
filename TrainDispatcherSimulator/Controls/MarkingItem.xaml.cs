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
    public partial class MarkingItem : UserControl
    {

        public MarkingItem()
        {
            InitializeComponent();
        }


        #region PROPERTIES
        public string MarkType
        {
            get { return markingBlock.Text; }
            set { markingBlock.Text = value; }
        }
        #endregion PROPERTIES

    }
}
