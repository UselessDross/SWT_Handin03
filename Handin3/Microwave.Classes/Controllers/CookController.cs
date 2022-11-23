using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Controllers
{
    public class CookController : ICookController
    {
        // Since there is a 2-way association, this cannot be set until the UI object has been created
        // It also demonstrates property dependency injection
        public IUserInterface UI { set; private get; }

        private bool isCooking = false;

        private IDisplay myDisplay;
        private IPowerTube myPowerTube;
        private ITimer myTimer;
        private IBuzzer myBuzzer;

        public CookController(
            ITimer timer,
            IBuzzer buzzer,
            IDisplay display,
            IPowerTube powerTube,
            IUserInterface ui) : this(timer, buzzer, display, powerTube)
        {
            UI = ui;
        }

        public CookController(
            ITimer timer,
            IBuzzer buzzer,
            IDisplay display,
            IPowerTube powerTube)
        {
            myTimer = timer;
            myBuzzer = buzzer;
            myDisplay = display;
            myPowerTube = powerTube;

            timer.Expired += new EventHandler(OnTimerExpired);
            timer.TimeChanged += new EventHandler(OnTimeChanged);
        }

        public void StartCooking(int power, int time)
        {
            myPowerTube.TurnOn(power);
            myTimer.Start(time);
            isCooking = true;
        }

        public void AddTime(int seconds) => myTimer.AddTime(seconds);
        public void SubtractTime(int seconds) => myTimer.SubtractTime(seconds);

        public void Stop()
        {
            isCooking = false;
            myPowerTube.TurnOff();
            myTimer.Stop();
        }

        public void OnTimerExpired(object sender, EventArgs e)
        {
            if (isCooking)
            {
                isCooking = false;
                myPowerTube.TurnOff();
                myBuzzer.Play(new BuzzerTone[]
                {
                    new BuzzerTone(800, 0.2), new BuzzerTone(0, 0.8),
                    new BuzzerTone(800, 0.2), new BuzzerTone(0, 0.8),
                    new BuzzerTone(800, 0.2), new BuzzerTone(0, 0.8),
                });
                UI.CookingIsDone();
            }
        }

        public void OnTimeChanged(object sender, EventArgs e)
        {
            if (isCooking)
            {
                int remaining = myTimer.TimeRemaining;
                myDisplay.ShowTime(remaining / 60, remaining % 60);
            }
        }
    }
}