using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class RegisterModel 
    {
     
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [EmailAddress]
        [MinLength(5)]
        [MaxLength(32)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [MinLength(5)]
        [MaxLength(32)]
        public string ConfirmPassword { get; set; }

    }
}
