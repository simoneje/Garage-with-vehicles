using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace Garage
{
    internal class Garage
    {
        private Vehicle[] vehicles;
        public Garage(int capacity)
        {
            vehicles = new Vehicle[capacity];
        }
        public Vehicle? FindVehicle(string regNumber)
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] != null && vehicles[i].RegistrationNumber == regNumber)
                {
                    return vehicles[i];
                }
            }
            return null;
        }
        public bool ParkVehicle(Vehicle vehicle, int parkingSpot)
        {
            if (parkingSpot < 0 || parkingSpot >= vehicles.Length)
            {
                return false;
            }
            else if (vehicles[parkingSpot] != null)
            {
                return false;
            }
            else if (FindVehicle(vehicle.RegistrationNumber) != null)
            {
                return false;
            }
            else
            {
                vehicles[parkingSpot] = vehicle;
                return true;
            }
        }
        public void ListVehicles()
        {
            string output = "";
            for (int i = 0; i < vehicles.Length; i++)
            {
                output += $"Spot {i}: ";
                if (vehicles[i] == null)
                {
                    output += "Empty parking\n";
                }
                else
                {
                    output += vehicles[i].ToString()+"\n";
                }
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
            if (regNumber != null && regNumber.Length > 0 )
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
