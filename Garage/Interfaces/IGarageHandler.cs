using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Interfaces
{
    internal interface IGarageHandler
    {
        void LoadTestVehicles();

        (bool success, string message) ParkVehicle(Vehicle vehicle, int parkingSpot);
        (bool success, string message) SellVehicle(string regNumber);
        (bool success, string message) SearchVehicleByRegNumber(string regNumber);
        bool RegistrationNumberExists(string regNumber);
        bool RegistrationNumberValidLength(string regNumber);

        void ListVehicles();
        void SearchVehicle(string? type, string? color, int? wheels);
        void VehicleStatistics();
    }
}
