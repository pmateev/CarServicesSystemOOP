using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CarServicesSystem.Interfaces;
using CarServicesSystem.Models;

namespace CarServicesSystem.Services
{
    public class AppointmentService : IService
    {
        private List<Appointment> appointments = new();
        private readonly SafeLoad<Appointment> fileManager = new SafeLoad<Appointment>("Data/appointments.json");

        public AppointmentService()
        {
            Load();
        }

        public void Save() => fileManager.Save(appointments);
        public void Load() => appointments = fileManager.Load();

        public void BookAppointment(string garageName, string clientPhone, string serviceName, DateTime dateTime, Car car)
        {
            if (IsSlotAvailable(garageName, dateTime))
            {
                var appointment = new Appointment(garageName, clientPhone, serviceName, dateTime, car);
                appointments.Add(appointment);
                Save();
                Console.WriteLine("Appointment booked successfully!");
            }
            else
            {
                Console.WriteLine("This slot is already taken at this garage.");
            }
        }

        public bool IsSlotAvailable(string garageName, DateTime dateTime)
        {
            return !appointments.Any(a => a.GarageName.Equals(garageName, StringComparison.OrdinalIgnoreCase)
                                       && a.DateTime == dateTime);
        }
    }
}
