using System;
using CarServicesSystem.Interfaces;

namespace CarServicesSystem.Models
{
    public class Appointment : IDisplayable
    {
        public string GarageName { get; set; }
        public string ClientPhone { get; set; }
        public string ServiceName { get; set; }
        public DateTime DateTime { get; set; }
        public Car CarDetails { get; set; }

        public Appointment() { }

        public Appointment(string garageName, string clientPhone, string serviceName, DateTime dateTime, Car car)
        {
            GarageName = garageName;
            ClientPhone = clientPhone;
            ServiceName = serviceName;
            DateTime = dateTime;
            CarDetails = car;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"{ClientPhone} booked {ServiceName} at {GarageName} on {DateTime}");
            Console.WriteLine($"Car: {CarDetails.Brand} {CarDetails.Model} ({CarDetails.LicensePlate})");
        }
    }
}