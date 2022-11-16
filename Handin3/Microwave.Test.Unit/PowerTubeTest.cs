using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class PowerTubeTest
    {
        private PowerTube uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            uut = new PowerTube(output);
        }

        [TestCase(-10)]
        [TestCase(0)]
        public void Constructor_IllegalPowerTubePower_ThrowsException(int maxPower)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new PowerTube(output, maxPower));
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(800)]
        public void ConfigPower_LegalPower_ThrowsNoException(int maxPower)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.ConfigPower(maxPower));
        }
        [TestCase(-10)]
        [TestCase(0)]
        public void ConfigPower_IllegalPower_ThrowsException(int maxPower)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.ConfigPower(maxPower));
        }

        [TestCase(1, 10)]
        [TestCase(200, 200)]
        [TestCase(800, 1000)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power, int maxPower)
        {
            uut.ConfigPower(maxPower);
            uut.TurnOn(power);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(-5, 10)]
        [TestCase(-1, 10)]
        [TestCase(0, 10)]
        [TestCase(11, 10)]
        [TestCase(701, 700)]
        public void TurnOn_WasOffOutOfRangePower_ThrowsException(int power, int maxPower)
        {
            uut.ConfigPower(maxPower);
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut.TurnOn(50);
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut.TurnOff();
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            uut.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => uut.TurnOn(60));
        }
    }
}