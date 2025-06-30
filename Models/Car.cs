using System;

namespace CarServicesSystem.Models
{
    public class Car
    {
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Car(string licensePlate, string brand, string model, int year)
        {
            LicensePlate = licensePlate;
            Brand = brand;
            Model = model;
            Year = year;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"{Brand} {Model} ({Year}) - Reg. number: {LicensePlate}");
        }
    }
}
