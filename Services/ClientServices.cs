using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CarServicesSystem.Interfaces;
using CarServicesSystem.Models;

namespace CarServicesSystem.Services
{
    public class ClientService : IService
    {
        private List<Client> clients = new();
        private readonly SafeLoad<Client> fileManager = new SafeLoad<Client>("Data/clients.json");

        public ClientService()
        {
            Load();
        }

        public void Save() => fileManager.Save(clients);
        public void Load() => clients = fileManager.Load();

        public void RegisterClient(string fullName, string phone)
        {
            var client = new Client(fullName, phone);
            clients.Add(client);
            Save();
            Console.WriteLine("The client is registered succesfully!");
        }

        public Client FindClientByPhone(string phone)
        {
            return clients.Find(c => c.PhoneNumber == phone);
        }

        public void AddCarToClient(string phone, Car car)
        {
            var client = FindClientByPhone(phone);
            if (client != null)
            {
                client.AddCar(car);
                Save();
                Console.WriteLine("The car is added to the client.");
            }
            else
            {
                Console.WriteLine("The client is not found.");
            }
        }
    }
}
