using a2_s3673712_s3719368.Area.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Areas.Admin.Models
{
    public class LoginDto
    {
        [Required, StringLength(8)]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }

        [Required]
        public int CustomerID { get; set; }
        public virtual CustomerDto Customer { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }

        public bool Lock { get; set; }
        public DateTime LockDate { get; set; }
        public int attempt { get; set; }
    }
}
