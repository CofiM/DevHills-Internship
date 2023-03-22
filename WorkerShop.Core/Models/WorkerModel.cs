using WorkerShop.Core.Enums;

namespace WorkerShop.API.Models
{
    public class WorkerModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public SexEnum Sex { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int BuildingNumber { get; set; }

        public int? FloorNumber { get; set; }

        public int ApartmentNumber { get; set; }

        public double DayRate { get; set; }

        public DateTime Created { get; set; }

        public WorkerModel(string id, string firstName, string lastName, string middleName, SexEnum sex, string city, string street, int buildingNumber, int? floorNumber, int apartmentNumber, double dayRate, DateTime created)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Sex = sex;
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
            FloorNumber = floorNumber;
            ApartmentNumber = apartmentNumber;
            DayRate = dayRate;
            Created = created;
        }
    }
}
