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
            get { return (string)GetValue(MarkTypeProperty); }
            set { SetValue(MarkTypeProperty, value); }
        }
        public static readonly DependencyProperty MarkTypeProperty =
            DependencyProperty.Register("MarkType", typeof(string), typeof(MarkingItem), new FrameworkPropertyMetadata("",
                      FrameworkPropertyMetadataOptions.AffectsRender, 
                      new PropertyChangedCallback(OnUriChanged)));

        private static void OnUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MarkingItem).SetMarkText((string)e.NewValue);
        }

        
        #endregion PROPERTIES



        #region PUBLIC METHOS
        public void SetMarkText(string text)
        {
            markingTextBlock.Text = text;
        }
        #endregion PUBLIC METHOS


        #region EVENT HANDLERS

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            // Detect drag starting
            MarkingItem item = sender as MarkingItem;
            if (item != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(item, item.MarkType, DragDropEffects.Move);
            }
        }
        #endregion EVENT HANDLERS
    }
}
