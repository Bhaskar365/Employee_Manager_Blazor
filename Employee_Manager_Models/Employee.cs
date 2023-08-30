using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Employee_Manager_Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = String.Empty;

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime JoiningDate { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
