using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.Enums;

namespace WorkerShop.Core.DTOs
{
    public class AllClientsDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 5;

        public OrderByEnum OrderBy { get; set; } = OrderByEnum.FirstName;
    }
}
