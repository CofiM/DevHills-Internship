using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerShop.Core.DTOs
{
    public class VehicleDTO
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

        public ClientDTO Client { get; set; }
    }
}
