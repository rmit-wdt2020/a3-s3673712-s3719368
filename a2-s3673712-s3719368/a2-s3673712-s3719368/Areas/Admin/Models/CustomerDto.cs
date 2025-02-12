﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Areas.Admin.Models
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(11)]
        public string TFN { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(4), RegularExpression(@"^[0-9]{4}$",
         ErrorMessage = "Wrong postcode format.")]
        public string PostCode { get; set; }

        [RegularExpression("VIC|NSW|QLD|TAS|WA|SA", ErrorMessage = "Wrong state format.")]
        [MaxLength(3)]
        public string State { get; set; }

        [Required, RegularExpression(@"^[61][0-9]{9}$",
         ErrorMessage = "Wrong phone format.")]
        public string Phone { get; set; }

        public virtual List<AccountDto> Accounts { get; set; }
    }
}
