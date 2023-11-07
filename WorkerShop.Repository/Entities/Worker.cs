using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.Enums;

namespace WorkerShop.Repository.Entities
{
    public class Worker
    {
        [MaxLength(13)]
        [RegularExpression("[0-9]{13}")]
        public string Id { get; set; }


        [Required, MaxLength(255)]
        public string FirstName { get; set; }


        [Required, MaxLength(255)]
        public string LastName { get; set; }


        [Required, MaxLength(255)]
        public string MiddleName { get; set; }

        [Required]
        public SexEnum Sex { get; set; }

        [Required, MaxLength(255)]
        public string City { get; set; }

        [Required, MaxLength(255)]
        public string Street { get; set; }

        [Required,Range(1,999)]
        public int BuildingNumber { get; set; }

        [Range(1, 99)]
        public int? FloorNumber { get; set; }

        [Required, Range(1, 999)]
        public int ApartmentNumber { get; set; }

        [Required,Range(1,9999.99)]
        public double DayRate { get; set; }
        [Required]
        public DateTime Created { get; set;}
        [Required]
        public  bool IsActive { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public Worker(string id, string firstName, string lastName, string middleName, SexEnum sex, string city, string street, int buildingNumber, int? floorNumber, int apartmentNumber, double dayRate,   DateTimeOffset? deletedOn, DateTimeOffset? lastModifiedOn)
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
            Created = DateTime.UtcNow;
            IsActive = true;
            DeletedOn = deletedOn;
            LastModifiedOn = lastModifiedOn;
        }
        public Worker() { }
    }
}
