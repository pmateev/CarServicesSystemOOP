using System;
using System.Collections.Generic;
using CarServicesSystem.Interfaces;

namespace CarServicesSystem.Models
{
    public class Garage : IDisplayable
    {
        public string Name { get; private set; }
        public string Address { get; set; }
        public List<string> ServicesOffered { get; set; } = new();

        public Garage() { }

        public Garage(string name, string address, List<string> services)
        {
            Name = name;
            Address = address;
            ServicesOffered = services;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Garage: {Name}, Address: {Address}");
            Console.WriteLine("Services:");
            foreach (var s in ServicesOffered)
                Console.WriteLine($" - {s}");
        }
    }
}