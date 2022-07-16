using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4)]
        public string Password { get; set; }
        
        [Required]
        public int Role { get; set; }
    }
}