using System;
using System.Collections.Generic;
using CarServicesSystem.Interfaces;

namespace CarServicesSystem.Models
{
    public class Client : Person, IDisplayable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Car> Cars { get; set; } = new();

        public Client(string fullName, string phoneNumber) : base(fullName, phoneNumber) { }

        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Client: {FullName}, Phone: {PhoneNumber}");
            Console.WriteLine("Cars:");
            foreach (var car in Cars)
                Console.WriteLine($" - {car.Brand} {car.Model} ({car.LicensePlate})");
        }
    }
}