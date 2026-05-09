using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Models
{
    internal class Boat : Vehicle
    {
        public double Length { get; set; }
        public Boat(string registrationNumber, string color, int amountWheels, double length)
                : base(registrationNumber, color, amountWheels)
        {
            Length = length;
        }
        public override string ToString()
        {
            return base.ToString() +
                   $" | Length: {Length}m";
        }
    }
}
