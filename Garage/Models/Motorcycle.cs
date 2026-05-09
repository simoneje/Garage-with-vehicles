using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Models
{
    internal class Motorcycle : Vehicle
    {
        public int CylinderVolume { get; set; }
        public Motorcycle(string registrationNumber, string color, int amountWheels, int cylinderVolume)
                : base(registrationNumber, color, amountWheels)
        {
            CylinderVolume = cylinderVolume;
        }
        public override string ToString()
        {
            return base.ToString() +
                   $" | CC: {CylinderVolume,-4}";
        }
    }
}
