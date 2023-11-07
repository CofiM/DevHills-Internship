using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.Enums;

namespace WorkerShop.Core.Models
{
    public class ClientModel
    {
        [Required, MaxLength(13)]
        [RegularExpression("[0-9]{13}")]
        public string Id { get; set; }

        [Required, MaxLength(255)]
        public string FirstName { get; set; }

        [Required, MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        public SexEnum Sex { get; set; }

        [Required, MaxLength(255)]
        public string City { get; set; }

        [Required, MaxLength(255)]
        public string Street { get; set; }

        [Range(1, 999)]
        public int? BuildingNumber { get; set; }

        [Range(1, 99)]
        public int? FloorNumber { get; set; }

        [Range(1, 999)]
        public int? ApartmentNumber { get; set; }

        [Required, MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        public List<VehicleModel> Vehicles { get; set; }

        public ClientModel(string id, string firstName, string lastName, SexEnum sex, string city, string street, int? buildingNumber, int? floorNumber, int? apartmentNumber, string email, List<VehicleModel> vehicles)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
            FloorNumber = floorNumber;
            ApartmentNumber = apartmentNumber;
            Email = email;
            Vehicles = vehicles;
        }
    }
}
