using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerShop.Repository.Entities
{
    public class WorkOrder
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

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
        public DateTime Created { get; set; }
        public string? WorkNotes { get; set; }   

        public bool IsActive { get; set; }  
        public DateTimeOffset? CompletedAt { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }

        public Vehicle Vehicle { get; set; }    
        
        public Worker? Worker { get; set; }

    }
}
