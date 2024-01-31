using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Manager_Models.CustomModels
{
    public class LoginModel
    {
        public string? UserKey { get; set; }
        public string? Name { get; set; }
        
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }   
    }
}
