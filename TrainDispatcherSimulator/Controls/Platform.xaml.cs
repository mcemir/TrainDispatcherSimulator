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
    /// Interaction logic for Platform.xaml
    /// </summary>
    public partial class Platform : UserControl
    {

        #region PROPERTIES

        public string Title
        {
            get { return titleTextBlock.Text; }
            set { titleTextBlock.Text = value; } 
        }

        


        #endregion PROPERTIES

        public Platform()
        {
            InitializeComponent();
        }
    }
}
