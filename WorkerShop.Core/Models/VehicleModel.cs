using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.Enums;

namespace WorkerShop.Core.Models
{
    public class VehicleModel
    {
        [Required, MaxLength(17)]
        public string VIN { get; set; }

        [Required]
        [RegularExpression("[0-9]{4}")]
        public int ManufacturingYear { get; set; }

        [RegularExpression("[a-z A-Z 0-9]*")]
        public string? LicensePlate { get; set; }

        [Required, Range(1, 9999)]
        public int EngineDisplacement { get; set; }

        [Required, Range(1, 9999)]
        public int Power { get; set; }

        [Required]
        public PowerEnum PowerEnum { get; set; }

        public VehicleModel(string vIN, int manufacturingYear, string? licensePlate, int engineDisplacement, int power, PowerEnum powerEnum)
        {
            VIN = vIN;
            ManufacturingYear = manufacturingYear;
            LicensePlate = licensePlate;
            EngineDisplacement = engineDisplacement;
            Power = power;
            PowerEnum   = powerEnum;
        }
    }
}
