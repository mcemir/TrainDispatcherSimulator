using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace TrainDispatcherSimulator.Helpers
{
    public class KeyboardInput
    {
        public KeyboardInput()
        {

        }
        public KeyboardEvents parseKeyboardInput(KeyEventArgs e)
        {
            Controller.Instance.Railways[13].Platform.Title = e.Key.ToString();
            //RailwayBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 200));
            return KeyboardEvents.Clear;
        }

    }
}
