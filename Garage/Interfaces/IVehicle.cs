using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Interfaces
{
    internal interface IVehicle
    {
        string RegistrationNumber { get; }
        string Color { get; set; }
        int AmountWheels { get; set; }
    }
}
