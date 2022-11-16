using System.Threading;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class TimerTest
    {
        private Timer uut;

        [SetUp]
        public void Setup()
        {
            uut = new Timer();
        }

        //<NEW TEST>
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        public void Stop_AddTimer_addedsTime(int startTime)
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(startTime);
            uut.Stop();

            uut.AddTime(1);
            

            // the remaning time should be above startTime
            Assert.That(uut.TimeRemaining,Is.GreaterThan(startTime));
        }
        //</NEW TEST>


        //<NEW TEST>
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        public void Start_AddTimer_addedsTime(int startTime)
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(startTime);
         
            uut.AddTime(1);
            uut.AddTime(1);


            // the remaning time should be above startTime
            Assert.That(uut.TimeRemaining, Is.GreaterThan(startTime));
        }
        //</NEW TEST>


        //<NEW TEST>
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(11)]
        public void Stop_subtract_subtractTime(int startTime)
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(startTime);
            uut.Stop();

            uut.SubtractTime(10);

            // the remaning time should be above startTime
            Assert.That(uut.TimeRemaining, Is.LessThan(startTime-1));
        }
        //</NEW TEST>

        //<NEW TEST>
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(11)]
        public void Start_subtract_subtractTime(int startTime)
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(startTime);


            uut.SubtractTime(10);

            // the remaning time should be above startTime
            Assert.That(uut.TimeRemaining, Is.LessThan(startTime - 1));
        }
        //</NEW TEST>


        [Test]
        public void Start_TimerTick_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimeChanged += (sender, args) => pause.Set();
            uut.Start(2);

            // wait for a tick, but no longer
            Assert.That(pause.WaitOne(1100));
        }

        [Test]
        public void Start_TimerTick_LongEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimeChanged += (sender, args) => pause.Set();
            uut.Start(2);

            // wait shorter than a tick, shouldn't come
            Assert.That(!pause.WaitOne(900));
        }

        [Test]
        public void Start_TimerExpires_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.Start(2);

            // wait for expiration, but not much longer, should come
            Assert.That(pause.WaitOne(2100));
        }

        [Test]
        public void Start_TimerExpires_LongEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.Start(2);

            // wait shorter than expiration, shouldn't come
            Assert.That(!pause.WaitOne(1900));
        }

        [Test]
        public void Start_TimerTick_CorrectNumber()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int notifications = 0;

            uut.Expired += (sender, args) => pause.Set();
            uut.TimeChanged += (sender, args) => notifications++;

            uut.Start(2);

            // wait longer than expiration
            Assert.That(pause.WaitOne(2100));

            Assert.That(notifications, Is.EqualTo(2));
        }

        [Test]
        public void Stop_NotStarted_NoThrow()
        {
            Assert.That( () => uut.Stop(), Throws.Nothing);
        }

        [Test]
        public void Stop_Started_NoTickTriggered()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimeChanged += (sender, args) => pause.Set();

            uut.Start(2000);
            uut.Stop();

            Assert.That(!pause.WaitOne(1100));
        }

        [Test]
        public void Stop_Started_NoExpiredTriggered()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();

            uut.Start(2000);
            uut.Stop();

            Assert.That(!pause.WaitOne(2100));
        }

        [Test]
        public void Stop_StartedOneTick_NoExpiredTriggered()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int notifications = 0;

            uut.Expired += (sender, args) => pause.Set();
            uut.TimeChanged += (sender, args) => uut.Stop();

            uut.Start(2000);

            Assert.That(!pause.WaitOne(2100));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Tick_Started_TimeRemainingCorrect(int ticks)
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int ticksGone = 0;
            uut.TimeChanged += (sender, args) =>
            {
                ticksGone++;
                if (ticksGone >= ticks)
                    pause.Set();
            };
            uut.Start(5);

            // wait for ticks, only a little longer
            pause.WaitOne(ticks * 1000 + 100);

            Assert.That(uut.TimeRemaining, Is.EqualTo(5-ticks*1));
        }




    }
}