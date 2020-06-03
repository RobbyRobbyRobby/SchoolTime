using System;
using System.Threading;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Displays.Lcd;
using Meadow.Foundation.Sensors.Buttons;
using Meadow.Foundation.Sensors;
using System.Collections.Generic;
using Meadow.Hardware;

namespace SchoolTime
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {
        CharacterDisplay display;
        PushButton pushButton;

        List<string> messages = new List<string> {"Morning", "School", "Break", "School", "Lunch", "School", "Free Time", "Quiet Time", "Family Time", "Bed Time"  };
        int messageIndex = 0;

        public MeadowApp()
        {
            Initialize();
        }

        void Initialize()
        {
            SetupDisplay();
            pushButton = new PushButton(Device,
             Device.Pins.D12);
         
            //pushButton.PressStarted += PushButtonPressStarted;
            pushButton.PressEnded += PushButtonPressEnded;
        }

        private void PushButtonPressEnded(object sender, EventArgs e)
        {
            messageIndex++;

            Console.WriteLine(messageIndex);

            if (messageIndex >= messages.Count)
            {
                messageIndex = 0;
            }

            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            Console.WriteLine(messages[messageIndex]);
            display.WriteLine(string.Empty, 0);
            display.WriteLine(messages[messageIndex], 1); 
        }

        public void SetupDisplay()
        {
            Console.WriteLine("Setup Display called");

            display = new CharacterDisplay(
                Device,
                pinRS: Device.Pins.D05,
                pinE: Device.Pins.D07,
                pinD4: Device.Pins.D08,
                pinD5: Device.Pins.D09,
                pinD6: Device.Pins.D10,
                pinD7: Device.Pins.D11,
                rows: 4, columns: 20    // Adjust dimensions to fit your display
                );

            display.WriteLine("Hello!", 0);
            display.WriteLine("Press to cycle.", 1);
            display.WriteLine("Martain School.", 3);
            //display.WriteLine("Press the Orange button for 'Get Ready'.", 2);
            //display.WriteLine("Press the Blue button for 'School Time'.", 3);
            //display.WriteLine("Press the Green button for 'Break Time'.", 4);
            //display.WriteLine("Press the White button for 'School Finished'.", 5);

            Console.WriteLine("Display initial message set.");
        }

    }
}
