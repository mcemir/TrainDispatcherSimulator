using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace TrainDispatcherSimulator.Helpers
{
    public class AudioSignal
    {
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
        public void AsteriskSound()
        {
                SystemSounds.Asterisk.Play();
                System.Media.SystemSounds.Question.Play();
        }

        public void BeepSound()
        {
                SystemSounds.Beep.Play();
        }

        public void ExclamationSound()
        {
                SystemSounds.Exclamation.Play();
        }

        public void HandSound()
        {
                SystemSounds.Hand.Play();
        }

        public void QuestionSound()
        {
                SystemSounds.Question.Play();
        }


    }
}
