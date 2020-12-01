using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter the password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter the new password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please confirm the password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
