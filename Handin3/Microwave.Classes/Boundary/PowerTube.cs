using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        public PowerTube(IOutput output, int PowerTubePower = 700)
        {
            myOutput = output;
            ConfigPower(PowerTubePower);
        }

        public void TurnOn(int power)
        {
            if (Power_ < 1)
            {
                throw new Exception("PowerTube not configured. Possible solution: call configPower()");
            }


            if (power < 1 || Power_ < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 700 (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }



        private int Power_ = 0;
        public void ConfigPower(int power)
        {
            if (power < 1) throw new ArgumentOutOfRangeException("configuring the power tube must be above 1, power value not set to > 1");
            Power_ = power;
        }
    }
}