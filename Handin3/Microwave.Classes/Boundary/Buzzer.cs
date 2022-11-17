using Microwave.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        public Task Play(params BuzzerTone[] tones) => throw new NotImplementedException();
    }
}
