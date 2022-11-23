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
        public Buzzer() { }

        public async Task Play(params BuzzerTone[] tones)
        {
            foreach (var tone in tones)
            {
                if (tone.Frequency > 0)
                    Console.WriteLine($"BZZ: Plays {tone.Frequency}Hz tone for {tone.Time} seconds");

                await Task.Delay((int)(tone.Time * 1000));
            }

            return;
        }
    }
}
