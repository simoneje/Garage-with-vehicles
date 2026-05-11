using System;
using System.Collections.Generic;
using System.Text;

namespace Garage
{
    internal class Vehicle
    {
        public string RegistrationNumber { get; }
        public string Color { get; set; }
        public int AmountWheels { get; set; }

        public Vehicle(string registrationNumber, string color, int amountOfWheels)
        {
            RegistrationNumber = registrationNumber;
            Color = color;
            AmountWheels = amountOfWheels;
        }
        public override string ToString()
        {
            return $"{GetType().Name,-12} | " +
                   $"Reg: {RegistrationNumber,-8} | " +
                   $"Color: {Color,-8} | " +
                   $"Wheels: {AmountWheels,-2}";
        }
        
    }
}
