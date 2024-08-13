using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerShop.Core.Models
{
    public class CompleteWorkOrderRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string WorkerId { get; set; }
        [Required]
        public string Notes { get; set; }
    }
}
