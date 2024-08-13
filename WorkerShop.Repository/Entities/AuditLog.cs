using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerShop.Repository.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        [Required]
        public string EntityName { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]  
        public DateTime TimeStamp { get; set; }

        [Required]
        public string Changes { get; set; } 
    }
}
