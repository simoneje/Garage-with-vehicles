using Garage.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace Garage.Core
{
    internal class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        private T?[] vehicles;
        public int Capacity => vehicles.Length;
        public Garage(int capacity)
        {
            vehicles = new T?[capacity];
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

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T? vehicle in vehicles)
            {
                if (vehicle != null)
                {
                    yield return vehicle;
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool ParkVehicle(T vehicle, int parkingSpot)
        {
            if (vehicle == null)    //Fordonet måste innehålla ett objekt för att kunna parkeras
                return false;

            else if (parkingSpot < 0 || parkingSpot >= vehicles.Length) //Är parkeringsplatsen en laglig plats?
                return false;

            else if (vehicles[parkingSpot] != null) //Är parkeringsplatsen tagen?
                return false;

            else //Parkera fordon!
            {
                vehicles[parkingSpot] = vehicle;
                return true;
            }
        }
        public T? GetVehicleAtSpot(int spot)
        {
            if (spot < 0 || spot >= Capacity)
            {
                return null;
            }
                
            return vehicles[spot];
        }

    }
}
