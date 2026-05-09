using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Models
{
    internal class Airplane : Vehicle
    {
        public int NumberOfEngines { get; set; }
        public Airplane(string registrationNumber, string color, int amountWheels, int numberOfEngines)
                : base(registrationNumber, color, amountWheels)
        {
            NumberOfEngines = numberOfEngines;
        }
        public override string ToString()
        {
            return base.ToString() +
                   $" | Engines: {NumberOfEngines,-2}";
        }
    }
}
