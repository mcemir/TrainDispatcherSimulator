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
using System.Windows.Threading;

namespace TrainDispatcherSimulator.Controls
{
    /// <summary>
    /// Interaction logic for RailwayPrivola.xaml
    /// </summary>
    public partial class RailwayPrivolaIn : RailwayBase
    {

        public RailwayPrivolaIn()
        {
            InitializeComponent();


            // TEST 
            GenerateTrain(5,0);
        }




        #region PUBLIC METHODS

        // Metoda koja će generisati voz na prvoj šini za onoliko vremena koliko je zadato sa offset-om
        public void GenerateTrain(int minutes, int seconds)
        {
            startTimer(minutes, seconds);
        }
        #endregion PUBLIC METHODS





        #region PRIVATE METHODS

        private void generateTrain()
        {

        }

        #endregion PRIVATE METHODS




        #region DISPATCHER TIMER
        // Timer koji služi za animaciju sata na GUI-u
        private DispatcherTimer timer;
        private int minutes, seconds;

        private void startTimer(int minutes, int seconds) 
        {
            this.minutes = minutes;
            this.seconds = seconds;

            timer = new DispatcherTimer(DispatcherPriority.Render); // Set priority to render
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);           // Tick every second
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (minutes <= 0 && seconds <= 0)    // If offset is in the past
            {
                timerTextBlock.Text = "";
                timer.Stop();
                generateTrain();
            }
            else
            {
                seconds--;
                if (seconds < 0)
                {
                    seconds = 59;
                    minutes--;
                }
                string secStr = seconds < 10 ? "0" + seconds : "" + seconds;
                timerTextBlock.Text = minutes + ":" + secStr;
            }
        }
        

        #endregion DISPATCHER TIMER

    }
}
