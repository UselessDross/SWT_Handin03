using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Classes.Interfaces
{
    public interface ICookController
    {
        void StartCooking(int power, int time);
        void AddTime(int seconds);
        void SubtractTime(int seconds);
        void Stop();
    }
}