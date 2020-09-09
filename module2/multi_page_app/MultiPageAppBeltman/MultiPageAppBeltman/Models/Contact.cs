using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultiPageAppBeltman.Models
{
    public class Contact
    {
        public int ContactID { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public string Notes { get; set; }
    }
}
