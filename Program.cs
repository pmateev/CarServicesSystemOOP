using System;
using System.Collections.Generic;
using CarServicesSystem.Models;
using CarServicesSystem.Services;

namespace CarServicesSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var garageService = new GarageService();
            var clientService = new ClientService();

            while (true)
            {
                Console.WriteLine("      -- MENU --      \n");
                Console.WriteLine("1. Register a garage");
                Console.WriteLine("2. Add service to garage");
                Console.WriteLine("3. Show garage info");
                Console.WriteLine("4. Register a client");
                Console.WriteLine("5. Add car to client");
                Console.WriteLine("6. Show client by phone number");
                Console.WriteLine("7. Book an appointment");
                Console.WriteLine("0. Exit\n");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Garage name: ");
                        string name = Console.ReadLine();
                        Console.Write("Address: ");
                        string address = Console.ReadLine();
                        Console.Write("Services (comma-separated): ");
                        string servicesStr = Console.ReadLine();
                        var services = new List<string>(servicesStr.Split(','));
                        garageService.RegisterGarage(name, address, services);
                        break;

                    case "2":
                        var garages = garageService.GetAllGarages();
                        if (garages.Count == 0)
                        {
                            Console.WriteLine("No garages registered.");
                            break;
                        }

                        Console.WriteLine("Available garages:");
                        for (int i = 0; i < garages.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {garages[i].Name}");
                        }

                        Console.Write("Select garage number: ");
                        int garageIndexForService = int.Parse(Console.ReadLine()) - 1;
                        var garageName = garages[garageIndexForService].Name;

                        Console.Write("New service to add: ");
                        string newService = Console.ReadLine();
                        garageService.AddServiceToGarage(garageName, newService);
                        break;


                    case "3":
                        var allGarages = garageService.GetAllGarages();
                        if (allGarages.Count == 0)
                        {
                            Console.WriteLine("No garages registered.");
                            break;
                        }

                        foreach (var g in allGarages)
                        {
                            g.DisplayInfo();
                            Console.WriteLine();
                        }
                        break;


                    case "4":
                        Console.Write("Full name: ");
                        string fullName = Console.ReadLine();
                        Console.Write("Phone number: ");
                        string phone = Console.ReadLine();
                        clientService.RegisterClient(fullName, phone);
                        break;

                    case "5":
                        Console.Write("Client phone number: ");
                        string clientPhone = Console.ReadLine();
                        Console.Write("Car license plate: ");
                        string plate = Console.ReadLine();
                        Console.Write("Brand: ");
                        string brand = Console.ReadLine();
                        Console.Write("Model: ");
                        string model = Console.ReadLine();
                        Console.Write("Year: ");
                        int year = int.Parse(Console.ReadLine());

                        var car = new Car(plate, brand, model, year);
                        clientService.AddCarToClient(clientPhone, car);
                        break;

                    case "6":
                        Console.Write("Phone number: ");
                        string searchPhone = Console.ReadLine();
                        var client = clientService.FindClientByPhone(searchPhone);
                        if (client != null)
                        {
                            client.DisplayInfo();
                        }
                        else
                        {
                            Console.WriteLine("Client not found.");
                        }
                        break;

                    case "7":
                        var garagesForBooking = garageService.GetAllGarages();
                        if (garagesForBooking.Count == 0)
                        {
                            Console.WriteLine("No garages registered.");
                            break;
                        }

                        Console.WriteLine("Available garages:");
                        for (int i = 0; i < garagesForBooking.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {garagesForBooking[i].Name}");
                        }

                        Console.Write("Select garage number: ");
                        int garageIndex = int.Parse(Console.ReadLine()) - 1;
                        var selectedGarage = garagesForBooking[garageIndex];

                        if (selectedGarage.ServicesOffered.Count == 0)
                        {
                            Console.WriteLine("This garage has no services.");
                            break;
                        }

                        Console.WriteLine("Available services:");
                        for (int i = 0; i < selectedGarage.ServicesOffered.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {selectedGarage.ServicesOffered[i]}");
                        }

                        Console.Write("Select service number: ");
                        int serviceIndex = int.Parse(Console.ReadLine()) - 1;
                        string selectedService = selectedGarage.ServicesOffered[serviceIndex];

                        Console.Write("Enter your phone number: ");
                        string clientPhoneInput = Console.ReadLine();

                        Console.Write("Enter appointment date (YYYY-MM-DD): ");
                        string dateStr = Console.ReadLine();
                        Console.Write("Enter hour (HH:MM): ");
                        string timeStr = Console.ReadLine();

                        if (DateTime.TryParse($"{dateStr} {timeStr}", out DateTime appointmentTime))
                        {
                            var appointmentService = new AppointmentService();
                            var clientBooking = clientService.FindClientByPhone(clientPhoneInput);
                            if (clientBooking == null)
                            {
                                Console.WriteLine("Client not found.");
                                break;
                            }

                            if (clientBooking.Cars.Count == 0)
                            {
                                Console.WriteLine("This client has no registered cars.");
                                break;
                            }

                            Console.WriteLine("Select a car to bring:");
                            for (int i = 0; i < clientBooking.Cars.Count; i++)
                            {
                                var c = clientBooking.Cars[i];
                                Console.WriteLine($"{i + 1}. {c.Brand} {c.Model} ({c.LicensePlate})");
                            }

                            Console.Write("Car number: ");
                            int carIndex = int.Parse(Console.ReadLine()) - 1;
                            var selectedCar = clientBooking.Cars[carIndex];

                            appointmentService.BookAppointment(selectedGarage.Name, clientPhoneInput, selectedService, appointmentTime, selectedCar);

                        }
                        else
                        {
                            Console.WriteLine("Invalid date or time format.");
                        }
                        break;

                    case "0":
                        Console.WriteLine("Exiting program.");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
