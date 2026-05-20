using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Interfaces
{
    internal interface IGarage<T> : IEnumerable<T> where T : Vehicle
    {
        int Capacity { get; }

        bool ParkVehicle(T Vehicle, int parkingSpot);
        bool RemoveVehicle(string registrationNumber);
        T? FindVehicle(string registrationNumber);
        T? GetVehicleAtSpot(int spot);
    }
}
