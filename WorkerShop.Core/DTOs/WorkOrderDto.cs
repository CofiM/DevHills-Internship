using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerShop.Core.DTOs
{
    public class WorkOrderDto
    {
        [Required, MaxLength(17)]
        public string VIN { get; set; }

        [RegularExpression("[0-9]{13}")]
        public string AssignedWorkerId { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Milleage { get; set; }

        [Required, Range(0, 100)]
        public float FuelLevel { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
