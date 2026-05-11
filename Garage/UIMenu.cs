using Garage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garage
{
    internal class UIMenu
    {
        private Garage garage;
        public UIMenu()
        {

        }
        public void Start()
        {
            Console.WriteLine("Welcome, how big is your garage?");

            int size;

            while (true)
            {
                Console.Write("Input size: ");

                if (int.TryParse(Console.ReadLine(), out size) && size > 0)
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid size. Try again.");
                Console.ResetColor();
            }

            garage = new Garage(size);

            if (size > 8)
            {
                LoadTestVehicles();
            }

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n1) List vehicles");
                Console.WriteLine("2) Order a vehicle");
                Console.WriteLine("3) Search the garage by registration number");
                Console.WriteLine("4) Search the garage by type values");
                Console.WriteLine("5) Sell a car");
                Console.WriteLine("0) Exit");

                string input = Console.ReadLine();

                switch(input)
                {
                    case "1":
                        garage.ListVehicles();
                        break;
                    case "2":
                        bool validOrder = OrderVehicle();
                        if (validOrder == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Order has been completed");

                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Order was not successfully completed");

                            Console.ResetColor();
                            break;
                        }
                    case "3":
                        garage.SearchVehicleByRegNumber();
                        break;
                    case "4":
                        SearchType();
                        break;
                    case "5":
                        Console.Write("Enter registration number: ");
                        string regNumber = Console.ReadLine();
                        if (garage.SellVehicle(regNumber))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Vehicle has been sold");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Could not find any vehicle to sell");
                            Console.ResetColor();
                        }

                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Do not recognize that command..\n");
                        break;
                }
            }
        }
        public bool OrderVehicle()
        {
            bool running = true;
            
            Console.WriteLine("\nWhat kind of vehicle would you like to order?");
            Console.WriteLine("1) Car\n2) Boat\n3) Airplane\n4) Bus\n5) Motorcycle");
            int.TryParse(Console.ReadLine(), out int vehicleType);
            if (vehicleType < 1 || vehicleType > 5)
            {
                Console.WriteLine("Invalid option");
                return false;
            }


            Console.Write("Registration number: ");
            string regNumber = Console.ReadLine();

            Console.Write("Color: ");
            string color = Console.ReadLine();

            Console.Write("Amount of wheels: ");
            int wheels = int.Parse(Console.ReadLine());

            Vehicle vehicle = null;

            switch(vehicleType)
            {
                case 1:
                    Console.Write("Fuel type: ");
                    string fuelType = Console.ReadLine();

                    vehicle = new Car(regNumber, color, wheels, fuelType);
                    break;

                case 2:
                    Console.Write("Length: ");
                    double length = double.Parse(Console.ReadLine());

                    vehicle = new Boat(regNumber, color, wheels, length);
                    break;

                case 3:
                    Console.Write("Amount of engines: ");
                    int.TryParse(Console.ReadLine(), out int numberOfEngines);

                    vehicle = new Airplane(regNumber, color, wheels, numberOfEngines);
                    break;

                case 4:
                    Console.Write("Amount of seats: ");
                    int.TryParse(Console.ReadLine(), out int numberOfSeats);

                    vehicle = new Bus(regNumber, color, wheels, numberOfSeats);
                    break;

                case 5:
                    Console.Write("Cylinder volume: ");
                    int.TryParse(Console.ReadLine(), out int cylinderVol);

                    vehicle = new Motorcycle(regNumber, color, wheels, cylinderVol);
                    break;

            }
            Console.WriteLine("\nVart vill du parkera ditt fordon?");
            garage.ListVehicles();
            int.TryParse(Console.ReadLine(), out int parkingSpot);

            bool success = garage.ParkVehicle(vehicle, parkingSpot);
            if (success)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        public void SearchType()
        {
            Console.Write("Input type of vehicle(enter to ignore): ");
            string? vehicleType = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(vehicleType))
                vehicleType = null;
            Console.Write("Input color of vehicle(enter to ignore): ");
            string? colorType = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(colorType))
                colorType = null;
            Console.Write("Input amount of wheels of vehicle(enter to ignore): ");
            string? wheels = Console.ReadLine();

            int? parsedWheels = null;

            if (!string.IsNullOrWhiteSpace(wheels))
            {
                if (int.TryParse(wheels, out int result))
                {
                    parsedWheels = result;
                    garage.SearchVehicle(vehicleType, colorType, parsedWheels);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Invalid wheel input");

                    Console.ResetColor();
                    return;
                }
            }
            else
            {
                garage.SearchVehicle(vehicleType, colorType, null);
            }
            
        }
        private void LoadTestVehicles()
        {
            garage.ParkVehicle(new Car("ABC123", "Red", 4, "Diesel"), 0);
            garage.ParkVehicle(new Boat("SEA777", "White", 0, 12), 1);
            garage.ParkVehicle(new Bus("BUS999", "Blue", 6, 48), 2);
            garage.ParkVehicle(new Motorcycle("MOTO55", "Black", 2, 600), 3);
            garage.ParkVehicle(new Airplane("AIR101", "Silver", 8, 2), 4);
            garage.ParkVehicle(new Car("XYZ888", "Gray", 4, "Gasoline"), 8);
        }
    }
}
