using Garage.Interfaces;
using Garage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Core
{
    //Garagehandler hanterar felhantering på garagelogik, söklogik
    internal class GarageHandler
    {
        private Garage<Vehicle> garage;
        public GarageHandler(int size)
        {

        }
        public void LoadTestVehicles()
        {
            garage.ParkVehicle(new Car("ABC123", "Red", 4, "Diesel"), 0);
            garage.ParkVehicle(new Boat("SEA777", "White", 0, 12), 1);
            garage.ParkVehicle(new Bus("BUS999", "Blue", 6, 48), 2);
            garage.ParkVehicle(new Motorcycle("MOTO55", "Black", 2, 600), 3);
            garage.ParkVehicle(new Airplane("AIR101", "Silver", 8, 2), 4);
            garage.ParkVehicle(new Car("XYZ888", "Gray", 4, "Gasoline"), 8);
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
        public bool SellVehicle(string regNumber)
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] != null && vehicles[i].RegistrationNumber.ToLower() == regNumber.ToLower())
                {
                    vehicles[i] = null;
                    return true;
                }
            }
            return false;
        }
        public void SearchVehicleByRegNumber()
        {
            Console.Write("\nEnter a registration number: ");
            string regNumber = Console.ReadLine();
            int counter = 0;
            Console.WriteLine("Searching...");
            Thread.Sleep(1500);
            if (regNumber != null && regNumber.Length > 0)
            {
                foreach (var vehicle in vehicles)
                {

                    if (vehicle != null && regNumber.ToUpper() == vehicle.RegistrationNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{vehicle.GetType().Name} found on parking spot [ {counter} ].");
                        Console.ResetColor();

                        Console.WriteLine(vehicle.ToString());
                        return;
                    }

                    counter++;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Registration number was not found in the garage");
            Console.ResetColor();
        }
        public void SearchVehicle(string? type, string? color, int? wheels)
        {
            string outputString = "";
            Console.WriteLine();
            foreach (var vehicle in vehicles)
            {
                if (vehicle == null)
                    continue;

                bool match = true;

                if (type != null && vehicle.GetType().Name.ToLower() != type.ToLower())
                    match = false;

                if (color != null && vehicle.Color.ToLower() != color.ToLower())
                    match = false;

                if (wheels != null && vehicle.AmountWheels != wheels)
                    match = false;

                if (match)
                    outputString += vehicle.ToString() + "\n";
            }
            if (outputString.Length > 0)
            {
                Console.WriteLine(outputString);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No such vehicle parked");
                Console.ResetColor();
            }
        }
    }
}
