using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerShop.Core.Models
{
    public class CreateOrUpdateWorkOrderModel
    {
        public Guid? Id { get; set; }

        [Required]
        public WorkOrderModel workOrder { get; set; }
    }
}
