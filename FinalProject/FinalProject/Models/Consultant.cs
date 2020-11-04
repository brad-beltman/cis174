using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Consultant
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The consultant name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The consultant's role is required")]
        public string Role { get; set; }  // For now, just user or admin but may expand in the future
    }
}
