using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.Enums;

namespace WorkerShop.Core.Models
{
    public class SearchRequest
    {
        public string? LicencePlate { get; set; }
        public string? VIN { get; set; }
        public string? WorkerId { get; set; }
        [Required]
        public bool Unassigned { get; set; }
        [Required]
        public CompleteEnum Status { get; set; }
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int Size { get; set; }
    }
}
