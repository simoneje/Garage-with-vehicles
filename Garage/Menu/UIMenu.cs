using Garage.Core;
using Garage.Interfaces;
using Garage.Models;
using Garage.Models.Enums;
using Garage.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Menu
{
    //Console (Input/Output)
    internal class UIMenu
    {
        private IGarageHandler handler;
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
                ConsoleUtility.Message("Invalid size. Try again.", ConsoleColor.Red);

            }

            handler = new GarageHandler(size);
            if (size > 8)
            {
                handler.LoadTestVehicles();
            }



            bool running = true;

            while (running)
            {
                Console.WriteLine("\n1) List vehicles");
                Console.WriteLine("2) Order a vehicle");
                Console.WriteLine("3) Search the garage by registration number");
                Console.WriteLine("4) Search the garage by type values");
                Console.WriteLine("5) Sell a vehicle");
                Console.WriteLine("6) Find vehicle type by LINQ grouping");
                Console.WriteLine("0) Exit");

                string input = Console.ReadLine();

                switch(input)
                {
                    case "1":
                        handler.ListVehicles();
                        break;
                    case "2":
                        bool validOrder = OrderVehicle();
                        if (validOrder == true)
                        {
                            ConsoleUtility.Message("Order has been completed.", ConsoleColor.Green);
                            break;
                        }
                        else
                        {
                            ConsoleUtility.Message("Order was not successfully completed", ConsoleColor.Red);
                            break;
                        }
                    case "3":
                        Console.Write("Enter registration number: ");
                        string regNumber3 = Console.ReadLine();
                        var result3 = handler.SearchVehicleByRegNumber(regNumber3);
                        if (result3.success)
                            ConsoleUtility.Message(result3.message, ConsoleColor.Green);
                        else
                            ConsoleUtility.Message(result3.message, ConsoleColor.Red);

                        break;
                    case "4":
                        SearchType();
                        break;
                    case "5":
                        Console.Write("Enter registration number: ");
                        string regNumber5 = Console.ReadLine();
                        var result5 = handler.SellVehicle(regNumber5);
                        if (result5.success)
                        {
                            ConsoleUtility.Message(result5.message,ConsoleColor.Green);
                        }
                        else
                        {
                            ConsoleUtility.Message(result5.message, ConsoleColor.Red);
                        }

                        break;
                    case "6":
                        handler.VehicleStatistics();
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
            Console.WriteLine("\nWhat kind of vehicle would you like to order?");
            Console.WriteLine("1) Car\n2) Boat\n3) Airplane\n4) Bus\n5) Motorcycle");
            int.TryParse(Console.ReadLine(), out int vehicleType);
            if (vehicleType < 1 || vehicleType > 5)
            {
                ConsoleUtility.Message("Invalid operation.", ConsoleColor.Red);
                return false;
            }


            Console.Write("Registration number: ");
            string regNumber = Console.ReadLine();
            if (handler.RegistrationNumberExists(regNumber))
            {
                ConsoleUtility.Message("Registration number already exists.", ConsoleColor.Red);
                return false;
            }

            if (!handler.RegistrationNumberValidLength(regNumber))
            {
                ConsoleUtility.Message("Registration number is not a valid length of six characters.", ConsoleColor.Red);
                return false;
            }
                
            Console.Write("Color: ");
            string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color))
            {
                ConsoleUtility.Message("Color cannot be empty.", ConsoleColor.Red);

                return false;
            }

            Console.Write("Amount of wheels: ");
            if (!int.TryParse(Console.ReadLine(), out int wheels))
            {
                ConsoleUtility.Message("Wheels needs to be a number input", ConsoleColor.Red);
                return false;
            }


            Vehicle vehicle = null;
            bool success = false;
            switch(vehicleType)
            {
                case 1:
                    Console.WriteLine("Fuel type:");
                    Console.WriteLine("1) Petrol");
                    Console.WriteLine("2) Diesel");
                    Console.WriteLine("3) Electric");
                    Console.WriteLine("4) Hybrid");
                    if (!int.TryParse(Console.ReadLine(), out int fuelChoice))
                    { 
                        ConsoleUtility.Message("Invalid fuel type.", ConsoleColor.Red);
                        return false;
                    }
                    FuelType fuelType;

                    switch (fuelChoice)
                    {
                        case 1:
                            fuelType = FuelType.Petrol;
                            break;

                        case 2:
                            fuelType = FuelType.Diesel;
                            break;

                        case 3:
                            fuelType = FuelType.Electric;
                            break;

                        case 4:
                            fuelType = FuelType.Hybrid;
                            break;

                        default:
                            ConsoleUtility.Message("Invalid fuel type.", ConsoleColor.Red);
                            return false;
                    }

                    vehicle = new Car(regNumber, color, wheels, fuelType);
                    break;

                case 2:
                    Console.Write("Length: ");
                    success = double.TryParse(Console.ReadLine(), out double length);
                    if (!success)
                    {
                        ConsoleUtility.Message("Not a valid length parameter.", ConsoleColor.Red);
                        return false;
                    }

                    vehicle = new Boat(regNumber, color, wheels, length);
                    break;

                case 3:
                    Console.Write("Amount of engines: ");
                    success = int.TryParse(Console.ReadLine(), out int numberOfEngines);
                    if (!success)
                    {
                        ConsoleUtility.Message("Not a valid number of engines parameter.", ConsoleColor.Red);
                        return false;
                    }

                    vehicle = new Airplane(regNumber, color, wheels, numberOfEngines);
                    break;

                case 4:
                    Console.Write("Amount of seats: ");
                    success = int.TryParse(Console.ReadLine(), out int numberOfSeats);
                    if (!success)
                    {
                        ConsoleUtility.Message("Not a valid number of seats parameter.", ConsoleColor.Red);
                        return false;
                    }

                    vehicle = new Bus(regNumber, color, wheels, numberOfSeats);
                    break;

                case 5:
                    Console.Write("Cylinder volume: ");
                    success = int.TryParse(Console.ReadLine(), out int cylinderVol);
                    if (!success)
                    {
                        ConsoleUtility.Message("Not a valid cylinder volume.", ConsoleColor.Red);
                        return false;
                    }

                    vehicle = new Motorcycle(regNumber, color, wheels, cylinderVol);
                    break;

            }
            Console.WriteLine("\nVart vill du parkera ditt fordon?");
            handler.ListVehicles();
            if (!int.TryParse(Console.ReadLine(), out int parkingSpot))
            {
                ConsoleUtility.Message("Parking spot must be a number.", ConsoleColor.Red);
            }

            var result = handler.ParkVehicle(vehicle, parkingSpot);
            if (result.success)
            {
                ConsoleUtility.Message(result.message, ConsoleColor.Green);
                return true;
            }
            else
            {
                ConsoleUtility.Message(result.message, ConsoleColor.Red);
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
                if (!int.TryParse(wheels, out int result))
                {
                    ConsoleUtility.Message("Wheel input must be a number or ignored.", ConsoleColor.Red);
                    return;
                }

                parsedWheels = result;
            }

            handler.SearchVehicle(vehicleType, colorType, parsedWheels);

        }

    }
}
