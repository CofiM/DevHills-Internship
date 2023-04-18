using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.Enums;

namespace WorkerShop.Core.DTOs
{
    public class WorkerWithFullNameDto
    {
        public WorkerWithFullNameDto(string id, string firstName, string lastName, string middleName, SexEnum sex, string city, string street, int buildingNumber, int? floorNumber, int apartmentNumber, double dayRate, bool isActive)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            FullName = firstName + " ("+ middleName +") " + lastName;
            Sex = sex;
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
            FloorNumber = floorNumber;
            ApartmentNumber = apartmentNumber;
            DayRate = dayRate;
            IsActive = isActive;
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FullName { get; set; }

        public SexEnum Sex { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int BuildingNumber { get; set; }

        public int? FloorNumber { get; set; }

        public int ApartmentNumber { get; set; }

        public double DayRate { get; set; }

        public bool IsActive { get; set; }
    }
}
