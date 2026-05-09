using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Models
{
    internal class Car : Vehicle
    {
        public string FuelType { get; set; }
        public Car(string registrationNumber, string color, int amountWheels, string fuelType)
                : base(registrationNumber, color, amountWheels)
        {
            FuelType = fuelType;
        }
        public override string ToString()
        {
            return base.ToString() +
                   $" | Fuel: {FuelType,-10}";
        }
    }
}
