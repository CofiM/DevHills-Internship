using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.Enums;

namespace WorkerShop.Core.DTOs
{
    public class WorkOrderPageDto
    {
        public string VIN { get; set; }
        public string? WorkerName { get; set; }
        public string? LicensePlate { get; set; }
        public CompleteEnum Status { get; set; }
        public DateTime Created { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
    }
}
