namespace CarServicesSystem.Models
{
    public class Person
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public Person() { }

        public Person(string fullName, string phone)
        {
            FullName = fullName;
            PhoneNumber = phone;
        }
    }
}