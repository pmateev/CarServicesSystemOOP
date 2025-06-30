using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CarServicesSystem.Interfaces;
using CarServicesSystem.Models;

namespace CarServicesSystem.Services
{
    public class GarageService : IService
    {
        private List<Garage> garages = new();
        private readonly SafeLoad<Garage> fileManager = new SafeLoad<Garage>("Data/garages.json");

        public GarageService()
        {
            Load();
        }

        public void Save() => fileManager.Save(garages);
        public void Load() => garages = fileManager.Load();

        public void RegisterGarage(string name, string address, List<string> services)
        {
            var garage = new Garage(name, address, services);
            garages.Add(garage);
            Save();
            Console.WriteLine("The garage is registered successfully.");
        }

        public List<Garage> GetAllGarages() => garages;

        public Garage GetGarageByName(string name)
        {
            return garages.FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void AddServiceToGarage(string garageName, string newService)
        {
            var garage = GetGarageByName(garageName);
            if (garage != null && !garage.ServicesOffered.Contains(newService))
            {
                garage.ServicesOffered.Add(newService);
                Save();
                Console.WriteLine("Service added to the garage.");
            }
            else
            {
                Console.WriteLine("This service already exists or the garage doesn't exist.");
            }
        }
    }
}
