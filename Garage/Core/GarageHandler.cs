using Garage.Interfaces;
using Garage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Core
{
    //Garagehandler hanterar felhantering på garagelogik, söklogik
    internal class GarageHandler : IGarageHandler
    {
        private Garage<Vehicle> garage;
        public GarageHandler(int size)
        {
            garage = new Garage<Vehicle>(size);
        }
        public void LoadTestVehicles()
        {
            garage.ParkVehicle(new Car("ABC123", "Red", 4, Models.Enums.FuelType.Diesel), 0);
            garage.ParkVehicle(new Boat("SEA777", "White", 0, 12), 1);
            garage.ParkVehicle(new Bus("BUS999", "Blue", 6, 48), 2);
            garage.ParkVehicle(new Motorcycle("MOTO55", "Black", 2, 600), 3);
            garage.ParkVehicle(new Airplane("AIR101", "Silver", 8, 2), 4);
            garage.ParkVehicle(new Car("XYZ888", "Gray", 4, Models.Enums.FuelType.Petrol), 8);
        }
        public (bool success, string message) ParkVehicle(Vehicle vehicle, int parkingSpot)
        {
            if (garage.FindVehicle(vehicle.RegistrationNumber) != null)
            {
                return (false, "Registration number already exists.");
            }
            if (!garage.ParkVehicle(vehicle, parkingSpot))
            {
                return (false, "Parking spot is occupied or invalid.");
            }
            return (true, "Vehicle parked successfully!");
        }
        public void ListVehicles()
        {
            string output = "";

            for (int i = 0; i < garage.Capacity; i++)
            {
                var vehicle = garage.GetVehicleAtSpot(i);

                output += $"Spot {i}: ";

                if (vehicle == null)
                    output += "Empty parking\n";
                else
                    output += vehicle + "\n";
            }

            Console.WriteLine("\n" + output);
        }
        //public bool SellVehicle(string regNumber)
        //{
        //    for (int i = 0; i < vehicles.Length; i++)
        //    {
        //        if (vehicles[i] != null && vehicles[i].RegistrationNumber.ToLower() == regNumber.ToLower())
        //        {
        //            vehicles[i] = null;
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        public (bool success, string message) SellVehicle(string regNumber)
        {
            if (string.IsNullOrWhiteSpace(regNumber))
                return (false, "Registration is not a valid string.");

            var success = garage.RemoveVehicle(regNumber);
            if (success)
                return (true, "Vehicle has been sold, congratulations!");

            return (false, "Vehicle could not be found among all vehicles");
        }
        public (bool success, string message) SearchVehicleByRegNumber(string regNumber)
        {
            if (string.IsNullOrWhiteSpace(regNumber))
                return (false, "Registration number is invalid.");

            if (regNumber.Length != 6)
                return (false, "Registration number needs to be a length of 6 characters.");

            var vehicle = garage.FindVehicle(regNumber);

            if (vehicle == null)
                return (false, "Vehicle was not found.");

            return (true, vehicle.ToString());
        }
        public bool RegistrationNumberExists(string regNumber)
        {
            if (string.IsNullOrWhiteSpace(regNumber))
                return false;

            return garage.FindVehicle(regNumber) != null;
        }
        //public void SearchVehicle(string? type, string? color, int? wheels)
        //{
        //    string outputString = "";
        //    Console.WriteLine();
        //    foreach (var vehicle in garage)
        //    {
        //        if (vehicle == null)
        //            continue;

        //        bool match = true;

        //        if (type != null && vehicle.GetType().Name.ToLower() != type.ToLower())
        //            match = false;

        //        if (color != null && vehicle.Color.ToLower() != color.ToLower())
        //            match = false;

        //        if (wheels != null && vehicle.AmountWheels != wheels)
        //            match = false;

        //        if (match)
        //            outputString += vehicle.ToString() + "\n";
        //    }
        //    if (outputString.Length > 0)
        //    {
        //        Console.WriteLine(outputString);
        //    }
        //    else
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("No such vehicle parked");
        //        Console.ResetColor();
        //    }
        //}
        public void SearchVehicle(string? type, string? color, int? wheels)
        {
            {
                var matches = garage.Where(vehicle =>
                    (string.IsNullOrWhiteSpace(type) ||
                     vehicle.GetType().Name.Equals(type, StringComparison.OrdinalIgnoreCase)) &&

                    (string.IsNullOrWhiteSpace(color) ||
                     vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase)) &&

                    (!wheels.HasValue ||
                     vehicle.AmountWheels == wheels.Value)
                );

                foreach (var vehicle in matches)
                {
                    Console.WriteLine(vehicle);
                }
            }
        }
        public bool RegistrationNumberValidLength(string regNumber)
        {
            if (string.IsNullOrWhiteSpace(regNumber))
                return false;

            if (regNumber.Length != 6)
                return false;

            return true;
        }

        public void VehicleStatistics()
        {
            var groupedVehicles =
                garage.GroupBy(v => v.GetType().Name);

            foreach (var group in groupedVehicles)
            {
                Console.WriteLine(
                    $"{group.Key}: {group.Count()}");
            }
        }
    }
}
