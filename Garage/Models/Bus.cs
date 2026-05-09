using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Models
{
    internal class Bus : Vehicle
    {
        public int NumberOfSeats { get; set; }
        public Bus(string registrationNumber, string color, int amountWheels, int numberOfSeats)
                : base(registrationNumber, color, amountWheels)
        {
            NumberOfSeats = numberOfSeats;
        }
        public override string ToString()
        {
            return base.ToString() +
                   $" | Seats: {NumberOfSeats,-3}";
        }
    }
}
