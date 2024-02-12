using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Manager_Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public DateTime DOB { get; set; } = DateTime.Now;
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        
        public Address? Address { get; set; } = new Address();
        public int DepartmentId { get; set; }

        [Required]
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; } = new Department();

        [Required]
        public DateTime JoiningDate { get; set; } = DateTime.Now;

        public DateTime CreatedOn { get; set; }
    }
}

