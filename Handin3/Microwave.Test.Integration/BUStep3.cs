using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class BUStep3
    {
        private IOutput output;

        private Timer timer;
        private Buzzer buzzer;
        private Display display;
        private PowerTube powerTube;
        private CookController cooker;

        private UserInterface ui;
        private Light light;

        private Button powerButton;
        private Button addTimeButton;
        private Button subtractTimeButton;
        private Button startCancelButton;

        private Door door;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();

            powerButton = new Button();
            addTimeButton = new Button();
            subtractTimeButton = new Button();
            startCancelButton = new Button();

            door = new Door();

            timer = new Timer();
            buzzer = new Buzzer();
            display = new Display(output);
            powerTube = new PowerTube(output);

            light = new Light(output);

            cooker = new CookController(timer, buzzer, display, powerTube);
            
            ui = new UserInterface(
                powerButton, addTimeButton, subtractTimeButton, startCancelButton,
                door,
                display, light, cooker);

            cooker.UI = ui;

        }

        #region Button_UserInterface

        #endregion

        #region UserInterface_Display

        [Test]
        public void Button_UserInterface_PowerButtonPressed()
        {
            powerButton.Press();

            // Should now be 50 W
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
        }

        [Test]
        public void Button_UserInterface_TimeButtonPressed()
        {
            powerButton.Press();
            addTimeButton.Press();

            // Should now show time 01:00
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void Button_UserInterface_StartCancelButtonPressed()
        {
            powerButton.Press();
            startCancelButton.Press();

            // Should cancel, and clear display
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }
        #endregion

        #region Door_UserInterface
        [Test]
        public void Door_UserInterface_DoorOpened()
        {
            door.Open();

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void Door_UserInterface_DoorClosed()
        {
            door.Open();
            door.Close();

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        #endregion

    }
}