using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerShop.Core.DTOs
{
    public class WorkOrderPage
    {
        public int TotalRows { get; set; }
        public List<WorkOrderPageDto> WorkOrders { get; set; }
    }
}
