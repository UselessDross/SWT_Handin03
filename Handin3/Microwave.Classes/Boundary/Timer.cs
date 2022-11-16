using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Timer : ITimer
    {
        public int TimeRemaining
        {
            get => _timeRemaining;
            set
            {
                _timeRemaining = Math.Max(0, value);
                TimerTick?.Invoke(this, EventArgs.Empty);

                if (_timeRemaining <= 0) Expire();
            }
        }
        private int _timeRemaining;

        public event EventHandler Expired;
        public event EventHandler TimerTick;

        private System.Timers.Timer timer;

        public Timer()
        {
            timer = new System.Timers.Timer();
            // Bind OnTimerEvent with an object of this, and set up the event
            timer.Elapsed += OnTimerEvent;
            timer.Interval = 1000; // 1 second intervals
            timer.AutoReset = true;  // Repeatable timer
        }


        public void Start(int time)
        {
            TimeRemaining = time;
            timer.Enabled = true;
        }

        public void AddTime(int seconds)
        {
            TimeRemaining += seconds;
        }
        public void SubtractTime(int seconds)
        {
            TimeRemaining -= seconds;
        }

        public void Stop()
        {
            timer.Enabled = false;
        }

        private void Expire()
        {
            timer.Enabled = false;
            Expired?.Invoke(this, System.EventArgs.Empty);
        }

        private void OnTimerEvent(object sender, System.Timers.ElapsedEventArgs args)
        {
            TimeRemaining -= 1;
        }

    }
}