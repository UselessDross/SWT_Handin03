using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button addTimeButton = new Button();
            Button subtractTimeButton = new Button();
            ConsoleKeyInfo inputKey;

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            //<Configurab powerTube value>
            powerTube.ConfigPower(800);
            //</Configurab powerTube value>
            /**
             * NOTE:
             * the assignment 4.1.2 states:
             * "you must make changes to the design, such that this value is configurable to other values [...]
             * from the main function, WHEN setting up and connecting all the modules BEFORE the system starts running."
             * 
             * please state if this has been done incrractly*/

            CookController cooker = new CookController(timer, buzzer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, addTimeButton, subtractTimeButton, startCancelButton, door, display, light, cooker);
            cooker.UI = ui;

            //Simulate simple operation

            powerButton.Press();
            addTimeButton.Press();

            System.Console.WriteLine();
            System.Console.WriteLine("When you press enter, the timer will start");
            System.Console.WriteLine("When you press up or down, 1 minute will be added/subtracted from the timer");
            while ((inputKey = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {

                switch (inputKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        addTimeButton.Press();
                        break;
                    case ConsoleKey.DownArrow:
                        subtractTimeButton.Press();
                        break;
                    default: break;
                }
            }

            startCancelButton.Press();

            System.Console.WriteLine();
            System.Console.WriteLine("When you press enter, the program will stop");
            System.Console.WriteLine("When you press up or down, 10 seconds will be added/subtracted from the timer");
            while ((inputKey = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                
                switch (inputKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        addTimeButton.Press();
                        break;
                    case ConsoleKey.DownArrow:
                        subtractTimeButton.Press();
                        break;
                    default: break;
                }
            }
        }
    }
}
