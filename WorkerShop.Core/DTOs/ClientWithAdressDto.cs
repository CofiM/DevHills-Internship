﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.Enums;

namespace WorkerShop.Core.DTOs
{
    public class ClientWithAdressDto
    {
        [Required, MaxLength(13)]
        [RegularExpression("[0-9]{13}")]
        public string Id { get; set; }

        [Required, MaxLength(255)]
        public string FirstName { get; set; }

        [Required, MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        public SexEnum Sex { get; set; }

        [Required, MaxLength(255)]
        public string Address { get; set; }

        [Required, MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        public virtual List<VehicleDTO> Vehicles { get; set; }
    }
}
