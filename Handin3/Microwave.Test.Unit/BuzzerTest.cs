using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;

namespace Microwave.Test.Unit
{

    [TestFixture]
    public class BuzzerTest
    {
        private Buzzer uut;

        [SetUp]
        public void Setup()
        {
            uut = new Buzzer();
        }

        [TestCase(60, 1)]
        [TestCase(180, 2)]
        [TestCase(240, 3)]

        public void Buzzer_writesToConsol_IsCorrect(long frequncy, double time)
        {
            
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
   
            BuzzerTone buzz = new BuzzerTone(frequncy,time);

            uut.Play(buzz);

            var output = stringWriter.ToString();
            Assert.That(output,Is.EqualTo($"BZZ: Plays {frequncy}Hz tone for {time} seconds\r\n"));

        }
    }
}
