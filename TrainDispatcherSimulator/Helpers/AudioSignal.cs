using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TrainDispatcherSimulator.Helpers
{
    public class AudioSignal
    {
        SoundPlayer mplayer;
        private static AudioSignal instance;
        public static AudioSignal Instance
        {
            get
            {
                if (instance == null)
                    instance = new AudioSignal();

                return instance;
            }
        }

        private AudioSignal()
        {
            mplayer = new SoundPlayer();
        }
        public void RailwaySwitchToogle()
        {
           
            mplayer.Stream = Properties.Resources.RailwaySwitchToggle2;
            mplayer.Play();
        }

        public void RailwayMouseUp()
        {

            mplayer.Stream = Properties.Resources.RailwayMouseUp;
            mplayer.Play();
        }

        public void RailwayScaleActivated()
        {

            mplayer.Stream = Properties.Resources.RailwayScaleActivated;
            mplayer.Play();
        }

    }
}
