using System;
using System.Collections.Generic;
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
                if (vehicles[i] != null && vehicles[i].RegistrationNumber == regNumber)
                {
                    vehicles[i] = null;
                    return true;
                }
            }
            return false;
        }
    }
}
