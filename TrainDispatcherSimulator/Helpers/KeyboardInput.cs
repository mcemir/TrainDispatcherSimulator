using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TrainDispatcherSimulator.Helpers
{
    public class KeyboardInput
    {
        private KeyboardEvents key;
        public KeyboardInput()
        {

        }

        public void  captureKeyboardInput(KeyEventArgs e){
            key = parseKeyboardInput(e);

        }
        /*
        private void performAction(KeyboardEvents k)
        {
            switch (k)
            {
                case KeyboardEvents.Clear:
                    break;
                case KeyboardEvents.Reserve:
                    break;
                case KeyboardEvents.ShowRailway:
                    break;
                case KeyboardEvents.ShowGraph:
                    break;
                case KeyboardEvents.ShowLog:
                    break;
            }
        }
         */
         
        private KeyboardEvents parseKeyboardInput(KeyEventArgs e)
        {
            string pressedKey = e.Key.ToString();
            Controller.Instance.Railways[13].Platform.Title = pressedKey;
            switch (pressedKey)
            {
                case "R":
                    Controller.Instance.Railways[13].Platform.Title = "Reserved";
                    break;
                case "C":
                    PathReservation.Instance.clearPath();
                    Controller.Instance.Railways[13].Platform.Title = "Cleared";
                    break;
                case "D1":
                    showRailway();
                    break;
                case "D2":
                    showGraph();
                    break;
                case "D3":
                    showLog();
                    break;
            }
            //Zanemariti ovaj return;
            return KeyboardEvents.Clear;
        }
           
        public void showRailway(){
            //railwayGrid.Visibility = Visibility.Visible;
            //graphGrid.Visibility = Visibility.Collapsed;
        }
        public void showGraph(){
            //railwayGrid.Visibility = Visibility.Collapsed;
            //graphGrid.Visibility = Visibility.Visible;
        }
        public void showLog(){
            //railwayGrid.Visibility = Visibility.Visible;
            //graphGrid.Visibility = Visibility.Collapsed;
        }
    }
}
