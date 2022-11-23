using System;
using System.IO;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class CookControllerTest
    {
        private CookController uut;

        private IUserInterface ui;
        private ITimer timer;
        private IBuzzer buzzer;
        private IDisplay display;
        private IPowerTube powerTube;

        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            timer = Substitute.For<ITimer>();
            buzzer = Substitute.For<IBuzzer>();
            display = Substitute.For<IDisplay>();
            powerTube = Substitute.For<IPowerTube>();

            uut = new CookController(timer, buzzer, display, powerTube, ui);
        }


        //<NEW TEST>
        [Test]
        public void Cooking_TimerExpired_BuzzerPlays()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            buzzer.Received(1);
        }
        //</NEW TEST>


        //<NEW TEST>
        [TestCase(11)]
        [TestCase(13)]
        [TestCase(23)]
        public void StartCooking_SubtractTime_TimerStarted(int startTime)
        {
            uut.StartCooking(50, startTime);

            uut.SubtractTime(10);
            timer.Received().SubtractTime(10);
        }
        //</NEW TEST>

        //<NEW TEST>
        [TestCase(11)]
        [TestCase(13)]
        [TestCase(23)]
        public void StartCooking_AddTime_TimerStarted(int startTime)
        {
            uut.StartCooking(50, startTime);

            uut.AddTime(10);
           timer.Received().AddTime(10);
        }
        //</NEW TEST>


        [Test]
        public void StartCooking_ValidParameters_TimerStarted()
        {
            uut.StartCooking(50, 60);

            timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            uut.StartCooking(50, 60);

            powerTube.Received().TurnOn(50);
        }

        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(115);
            timer.TimeChanged += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 55);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            powerTube.Received().TurnOff();
        }

        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            ui.Received().CookingIsDone();
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            powerTube.Received().TurnOff();
        }

    }
}