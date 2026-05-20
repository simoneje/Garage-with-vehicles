using Garage.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace Garage.Core
{
    internal class Garage<T> : IGarage<T> where T : Vehicle
    {
        private T?[] vehicles;
        public int Capacity => vehicles.Length;

        public Garage(int capacity)
        {
            vehicles = new T?[capacity];
        }

        public T? FindVehicle(string regNumber)
        {
            return this.FirstOrDefault(v => v.RegistrationNumber.Equals(regNumber, StringComparison.OrdinalIgnoreCase));
        }
        public bool RemoveVehicle(string regNumber)
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] != null &&
                    vehicles[i]!.RegistrationNumber.Equals(regNumber, StringComparison.OrdinalIgnoreCase))
                {
                    vehicles[i] = null;
                    return true;
                }
            }

            return false;
        }
        public bool ParkVehicle(T vehicle, int parkingSpot)
        {
            if (vehicle == null)    //Fordonet måste innehålla ett objekt för att kunna parkeras
                return false;

            if (parkingSpot < 0 || parkingSpot >= vehicles.Length) //Är parkeringsplatsen en laglig plats?
                return false;

            if (vehicles[parkingSpot] != null) //Är parkeringsplatsen tagen?
                return false;

            //Parkera fordon!
            vehicles[parkingSpot] = vehicle;
            return true;
        }
        public T? GetVehicleAtSpot(int spot)
        {
            if (spot < 0 || spot >= Capacity)
            {
                return null;
            }
                
            return vehicles[spot];
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
    }
}
