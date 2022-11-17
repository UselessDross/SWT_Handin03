using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Interfaces
{
    /// <summary> A tone that can be played. </summary>
    public struct BuzzerTone
    {
        /// <summary> The frequency of the tone, or 0 if it should be silent. </summary>
        public double Frequency;
        /// <summary> The duration of the tone in seconds. </summary>
        public double Time;
    }

    public interface IBuzzer
    {
        /// <param name="tones"> The tones to play, one after the other. </param>
        /// <returns> A task that finishes when all tones have been played. </returns>
        Task Play(params BuzzerTone[] tones);
    }
}
